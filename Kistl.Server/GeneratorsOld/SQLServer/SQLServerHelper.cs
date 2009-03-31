using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server;
using Kistl.Server.Generators.Extensions;
using Kistl.Server.GeneratorsOld.Helper;

namespace Kistl.Server.GeneratorsOld.SQLServer
{
    internal static class SQLServerHelper
    {
        public static bool CheckColumnExists(ObjectClass classToCheck, Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            SqlCommand cmd = new SqlCommand("select dbo.fn_ColumnExists(@t, @c)", db, tx);
            cmd.Parameters.AddWithValue("@t", GeneratorHelper.GetDatabaseTableName(classToCheck));
            if (p is ObjectReferenceProperty)
            {
                cmd.Parameters.AddWithValue("@c", p.PropertyName.CalcForeignKeyColumnName(parentPropertyName));
            }
            else
            {
                cmd.Parameters.AddWithValue("@c", p.PropertyName.CalcColumnName(parentPropertyName));
            }

            return (bool)cmd.ExecuteScalar();
        }

        public static bool CheckColumnExists(string table, string propertyName, SqlConnection db, SqlTransaction tx)
        {
            SqlCommand cmd = new SqlCommand("select dbo.fn_ColumnExists(@t, @c)", db, tx);
            cmd.Parameters.AddWithValue("@t", table);
            cmd.Parameters.AddWithValue("@c", propertyName);
            return (bool)cmd.ExecuteScalar();
        }


        public static bool CheckListPositionColumnExists(ObjectClass classToCheck, string parentPropertyName, Property p, SqlConnection db, SqlTransaction tx)
        {
            SqlCommand cmd = new SqlCommand("select dbo.fn_ColumnExists(@t, @c)", db, tx);
            cmd.Parameters.AddWithValue("@t", GeneratorHelper.GetDatabaseTableName(classToCheck));
            cmd.Parameters.AddWithValue("@c", p.PropertyName.CalcListPositionColumnName(parentPropertyName));
            return (bool)cmd.ExecuteScalar();
        }
        public static bool CheckListPositionColumnExists(Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            SqlCommand cmd = new SqlCommand("select dbo.fn_ColumnExists(@t, @c)", db, tx);
            cmd.Parameters.AddWithValue("@t", GeneratorHelper.GetDatabaseTableName(p));
            cmd.Parameters.AddWithValue("@c", p.PropertyName.CalcListPositionColumnName(parentPropertyName));
            return (bool)cmd.ExecuteScalar();
        }

        public static void CreateColumn(ObjectClass objClass, Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("alter table [{0}] add ", GeneratorHelper.GetDatabaseTableName(objClass));
            sb.Append(SQLServerHelper.GetColumnStmt(p, parentPropertyName));

            SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
            cmd.ExecuteNonQuery();
        }

        public static void CreateColumn(string tblName, string colName, string dataType, bool nullable, SqlConnection db, SqlTransaction tx)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("alter table [{0}] add [{1}] {2} {3}", tblName, colName, dataType, nullable ? "NULL" : "NOT NULL");

            SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
            cmd.ExecuteNonQuery();
        }

        public static void AlterColumn(ObjectClass objClass, Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("alter table [{0}] alter column ", GeneratorHelper.GetDatabaseTableName(objClass));
            sb.Append(SQLServerHelper.GetColumnStmt(p, parentPropertyName));

            SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
            cmd.ExecuteNonQuery();
        }

        public static void AlterColumn(Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("alter table [{0}] alter column ", GeneratorHelper.GetDatabaseTableName(p));
            sb.Append(SQLServerHelper.GetColumnStmt(p, parentPropertyName));

            SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
            cmd.ExecuteNonQuery();
        }

        public static void AlterColumn(string tblName, string colName, string dataType, bool nullable, SqlConnection db, SqlTransaction tx)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("alter table [{0}] alter column [{1}] {2} {3}", tblName, colName, dataType, nullable ? "NULL" : "NOT NULL");

            SqlCommand cmd = new SqlCommand(sb.ToString(), db, tx);
            cmd.ExecuteNonQuery();
        }

        public static void CreateListPositionColumn(ObjectClass objClass, Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            SQLServerHelper.CreateColumn(
                GeneratorHelper.GetDatabaseTableName(objClass),
                p.PropertyName.CalcListPositionColumnName(parentPropertyName), 
                "int", true, db, tx);
        }
        public static void CreateListPositionColumn(Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            SQLServerHelper.CreateColumn(
                GeneratorHelper.GetDatabaseTableName(p),
                p.PropertyName.CalcListPositionColumnName(parentPropertyName),
                "int", true, db, tx);
        }

        public static void AlterListPositionColumn(ObjectClass objClass, Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            SQLServerHelper.AlterColumn(
                GeneratorHelper.GetDatabaseTableName(objClass),
                p.PropertyName.CalcListPositionColumnName(parentPropertyName),
                "int", true, db, tx);
        }

        public static void AlterListPositionColumn(Property p, string parentPropertyName, SqlConnection db, SqlTransaction tx)
        {
            SQLServerHelper.AlterColumn(
                GeneratorHelper.GetDatabaseTableName(p),
                p.PropertyName.CalcListPositionColumnName(parentPropertyName),
                "int", true, db, tx);
        }

        /// <summary>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetColumnStmt(Property p, string parentPropertyName)
        {
            StringBuilder sb = new StringBuilder();

            if (p is ObjectReferenceProperty)
            {
                sb.AppendFormat(" [{0}] ", p.PropertyName.CalcForeignKeyColumnName(parentPropertyName));
            }
            else
            {
                sb.AppendFormat(" [{0}] ", p.PropertyName.CalcColumnName(parentPropertyName));
            }

            sb.Append(SQLServerHelper.GetDBType(p));

            if (p is StringProperty)
            {
                sb.AppendFormat(" ({0})", ((StringProperty)p).Length);
            }

            if (p.IsList) sb.AppendFormat(" NOT NULL ");
            else sb.AppendFormat(p.IsNullable ? " NULL " : " NOT NULL ");

            return sb.ToString();
        }

        public static bool CheckTableExists(ObjectClass objClass, SqlConnection db, SqlTransaction tx)
        {
            SqlCommand cmd = new SqlCommand("select dbo.fn_TableExists(@t)", db, tx);
            cmd.Parameters.AddWithValue("@t", GeneratorHelper.GetDatabaseTableName(objClass));
            return (bool)cmd.ExecuteScalar();
        }

        public static bool CheckTableExists(Property prop, SqlConnection db, SqlTransaction tx)
        {
            SqlCommand cmd = new SqlCommand("select dbo.fn_TableExists(@t)", db, tx);
            cmd.Parameters.AddWithValue("@t", GeneratorHelper.GetDatabaseTableName(prop));
            return (bool)cmd.ExecuteScalar();
        }

        #region CreateFKConstraint
        public static void CreateFKConstraint(ObjectClass parent, Property child, string fk_column, SqlConnection db, SqlTransaction tx)
        {
            string fk = GeneratorHelper.GetAssociationName(parent.GetTypeMoniker(), GeneratorHelper.GetAssociationChildType(child), fk_column);
            SqlCommand cmd = new SqlCommand("select dbo.fn_FKConstraintExists(@fk)", db, tx);
            cmd.Parameters.AddWithValue("@fk", fk);

            if (!((bool)cmd.ExecuteScalar()))
            {
                cmd = new SqlCommand(string.Format(@"ALTER TABLE [{0}]  WITH CHECK 
                    ADD CONSTRAINT [{1}] FOREIGN KEY([{2}])
                    REFERENCES [{3}] ([ID])",
                       GeneratorHelper.GetDatabaseTableName(child),
                       fk,
                       fk_column,
                       GeneratorHelper.GetDatabaseTableName(parent)), db, tx); ;
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(string.Format(@"ALTER TABLE [{0}] CHECK CONSTRAINT [{1}]",
                       GeneratorHelper.GetDatabaseTableName(child),
                       fk), db, tx); ;
                cmd.ExecuteNonQuery();
            }
        }

        public static void CreateFKConstraint(ObjectClass parent, ObjectClass child, string fk_column, SqlConnection db, SqlTransaction tx)
        {
            string fk = GeneratorHelper.GetAssociationName(parent, child, fk_column);
            SqlCommand cmd = new SqlCommand("select dbo.fn_FKConstraintExists(@fk)", db, tx);
            cmd.Parameters.AddWithValue("@fk", fk);

            if (!((bool)cmd.ExecuteScalar()))
            {
                cmd = new SqlCommand(string.Format(@"ALTER TABLE [{0}]  WITH CHECK 
                    ADD CONSTRAINT [{1}] FOREIGN KEY([{2}])
                    REFERENCES [{3}] ([ID])",
                       GeneratorHelper.GetDatabaseTableName(child),
                       fk,
                       fk_column,
                       GeneratorHelper.GetDatabaseTableName(parent)), db, tx); ;
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(string.Format(@"ALTER TABLE [{0}] CHECK CONSTRAINT [{1}]",
                       GeneratorHelper.GetDatabaseTableName(child),
                       fk), db, tx); ;
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region GetDBType
        /// <summary>
        /// Returns the coresponding database type of the given CLR Type
        /// </summary>
        /// <param name="clrType">CLR Type as string</param>
        /// <returns>Databasetype</returns>
        public static string GetDBType(Property p)
        {
            if (p is ObjectReferenceProperty)
            {
                return "int";
            }
            else if (p is EnumerationProperty)
            {
                return "int";
            }
            else
            {
                string clrType = p.GetPropertyTypeString();
                // Try to get the CLRType
                Type t = Type.GetType(clrType, false, false);

                if (t == null) throw new DBTypeNotFoundException(clrType);

                // Resolve...
                if (t == typeof(int))
                    return "int";
                if (t == typeof(string))
                    return "nvarchar";
                if (t == typeof(double))
                    return "float";
                if (t == typeof(bool))
                    return "bit";
                if (t == typeof(DateTime))
                    return "datetime";

                throw new DBTypeNotFoundException(clrType);
            }
        }
        #endregion
    }
}
