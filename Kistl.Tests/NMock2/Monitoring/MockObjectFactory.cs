using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using NMock2.Internal;

namespace NMock2.Monitoring
{
    /// <summary>
    /// Summary description for MockObjectFactory.
    /// </summary>
    internal class MockObjectFactory
    {
        private static readonly Hashtable createdTypes = new Hashtable();
        private readonly ModuleBuilder moduleBuilder;

        private class TypeId
        {
            private readonly Type[] types;

            public TypeId(params Type[] types)
            {
                this.types = types;
            }

            private bool ContainsSameTypesAs(TypeId other)
            {
                if (other.types.Length !=
                    types.Length)
                {
                    return false;
                }
                for (int num1 = 0; num1 < types.Length; num1++)
                {
                    if (Array.IndexOf(other.types, types[num1]) < 0)
                    {
                        return false;
                    }
                }
                return true;
            }

            public override bool Equals(object obj)
            {
                return ((obj is TypeId) && ContainsSameTypesAs((TypeId)obj));
            }

            public override int GetHashCode()
            {
                int num1 = 0;
                foreach (Type type1 in types)
                {
                    num1 ^= type1.GetHashCode();
                }
                return num1;
            }
        }

        public MockObjectFactory(string name)
        {
            AssemblyName name1 = new AssemblyName();
            name1.Name = name;
            moduleBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    name1, AssemblyBuilderAccess.Run).DefineDynamicModule(name);
        }

        private static bool AllTypes(Type type, object criteria)
        {
            return true;
        }

        private static void BuildAllInterfaceMethods(
            Type mockedType, TypeBuilder typeBuilder)
        {
            Type[] typeArray1 = mockedType.FindInterfaces(new TypeFilter(AllTypes), null);
            foreach (Type type1 in typeArray1)
            {
                BuildInterfaceMethods(typeBuilder, type1);
            }
            BuildInterfaceMethods(typeBuilder, mockedType);
        }

        private static void BuildConstructor(TypeBuilder typeBuilder)
        {
            Type[] typeArray1 =
                new Type[] { typeof(Mockery), typeof(Type), typeof(string) };

            ILGenerator generator1 =
                typeBuilder.DefineConstructor(
                    MethodAttributes.Public, CallingConventions.HasThis, typeArray1).
                    GetILGenerator();

            ConstructorInfo info1 =
             typeof(MockObject).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, null, typeArray1, null);

            generator1.Emit(OpCodes.Ldarg_0);
            generator1.Emit(OpCodes.Ldarg_1);
            generator1.Emit(OpCodes.Ldarg_2);
            generator1.Emit(OpCodes.Ldarg_3);
            generator1.Emit(OpCodes.Call, info1);
            generator1.Emit(OpCodes.Ret);
        }

        private static void BuildInterfaceMethods(
            TypeBuilder typeBuilder, Type mockedType)
        {
            typeBuilder.AddInterfaceImplementation(mockedType);
            MethodInfo[] infoArray1 = mockedType.GetMethods();
            foreach (MethodInfo info1 in infoArray1)
            {
                GenerateMethodBody(typeBuilder, info1);
            }
        }

        public MockObject CreateMockObject(
            Mockery mockery, Type mockedType, string name)
        {
            return
                (Activator.CreateInstance(
                    GetMockedType(
                        Id(new Type[] { mockedType, typeof(IMockObject) }), mockedType),
                    new object[] { mockery, mockedType, name })
                 as MockObject);
        }

        private static int failedTypes = 0;
        private Type GetMockedType(TypeId id1, Type mockedType)
        {
            Type type1;
            if (createdTypes.ContainsKey(id1))
            {
                type1 = (Type)createdTypes[id1];
            }
            else
            {
                try
                {
                    createdTypes[id1] =
                        type1 = CreateType("MockObjectType" + (createdTypes.Count + failedTypes + 1), mockedType);
                }
                catch (Exception tle)
                {
                    failedTypes += 1;
                    System.Console.Out.WriteLine(tle.ToString());
                    throw new ArgumentException("Unable to create Type for this interface", tle);
                }
            }
            return type1;
        }

        private Type CreateType(string typeName, Type mockedType)
        {
            TypeBuilder builder1 =
                moduleBuilder.DefineType(
                    typeName,
                    TypeAttributes.Public,
                    typeof(MockObject),
                    new Type[] { mockedType });
            BuildConstructor(builder1);
            BuildAllInterfaceMethods(mockedType, builder1);
            return builder1.CreateType();
        }

        private static void EmitReferenceMethodBody(ILGenerator gen)
        {
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Ret);
        }

        private static void EmitValueMethodBody(MethodInfo method, ILGenerator gen)
        {
            gen.DeclareLocal(method.ReturnType);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Ret);
        }

        private static void GenerateMethodBody(
            TypeBuilder typeBuilder, MethodInfo method)
        {
            ILGenerator generator1 = PrepareMethodGenerator(typeBuilder, method);
            generator1.Emit(OpCodes.Ldarg_0);

            if (method.ReturnType == null)
            {
                generator1.Emit(OpCodes.Ret);
            }
            else if (method.ReturnType.IsValueType)
            {
                EmitValueMethodBody(method, generator1);
            }
            else
            {
                EmitReferenceMethodBody(generator1);
            }
        }

        private static TypeId Id(params Type[] types)
        {
            return new TypeId(types);
        }

        private static ILGenerator PrepareMethodGenerator(
            TypeBuilder typeBuilder, MethodInfo method)
        {
            ParameterInfo[] infoArray1 = method.GetParameters();

            Type[] typeArray1 = new Type[infoArray1.Length];
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                typeArray1[num1] = infoArray1[num1].ParameterType;
            }
            MethodBuilder builder1 =
                typeBuilder.DefineMethod(
                    method.Name,
                    MethodAttributes.Virtual | MethodAttributes.Public,
                    method.CallingConvention,
                    method.ReturnType,
                    typeArray1);
#if NET20
                if (method.IsGenericMethod)
                {
                    // there surely must be a better way to do this. 
                    // LINQ for example:
                    // (from info in method.GetGenericArguments() select info.Name).ToArray<string>()
                    Type[] genArgs = method.GetGenericArguments();
                    string[] genericNames = new string[genArgs.Length];
                    for (int num2 = 0; num2 < genArgs.Length; num2++)
                    {
                        genericNames[num2] = genArgs[num2].Name;
                    }
                    // end bad hack

                    if (genericNames.Length > 0)
                    {
                        GenericTypeParameterBuilder[] typeParameters = builder1.DefineGenericParameters(genericNames);
                    }
                }
#endif
            builder1.InitLocals = true;
            typeBuilder.DefineMethodOverride(builder1, method);
            return builder1.GetILGenerator();
        }
    }
}