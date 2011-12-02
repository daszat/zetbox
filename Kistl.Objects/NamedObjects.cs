
namespace Kistl
{
    using System;
    using Kistl.API.Utils;

    public static class NamedObjects
    {
        public static class Base
        {
            public static class Classes
            {
                public static class Kistl
                {
                    public static class App
                    {
                        public static class Base
                        {
                            public static TypedGuid<global::Kistl.App.Base.ObjectClass> ObjectClass
                            {
                                get { return new TypedGuid<global::Kistl.App.Base.ObjectClass>("20888dfc-1fbc-47c8-9f3c-c6a30a5c0048"); }
                            }
                            public static TypedGuid<global::Kistl.App.Base.ObjectClass> ObjectReferenceParameter
                            {
                                get { return new TypedGuid<global::Kistl.App.Base.ObjectClass>("3fb8bf11-cab6-478f-b9b8-3f6d70a70d37"); }
                            }
                        }
                    }
                }
            }
        }
    }
}
