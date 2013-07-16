using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace 代码生成器
{
    public class TypeUtil
    {
        public static string TypeToVB(Type type)
        {
            switch (type.ToString())
            {
                case "System.Int64": return "Long";
                case "System.Byte[]": return "Byte()";
                case "System.Boolean": return "Boolean?";
                case "System.String": return "String";
                case "System.DateTime": return "DateTime?";
                case "System.Decimal": return "Decimal?";
                case "System.Double": return "Double?";
                case "System.Int32": return "Integer?";
                case "System.Single": return "Single?";
                case "System.Int16": return "Short?";
                case "System.Byte": return "Byte?";
                case "System.Guid": return "Guid?";
                case "System.TimeSpan": return "TimeSpan?";
                default:
                    throw new NotSupportedException("不支持的数据类型:" + type.ToString());
            }
        }

        public static string TypeToCS(Type type)
        {
            switch (type.ToString())
            {
                case "System.Int64": return "long";
                case "System.Byte[]": return "byte[]";
                case "System.Boolean": return "bool?";
                case "System.String": return "string";
                case "System.DateTime": return "DateTime?";
                case "System.Decimal": return "decimal?";
                case "System.Double": return "double?";
                case "System.Int32": return "int?";
                case "System.Single": return "float?";
                case "System.Int16": return "short?";
                case "System.Byte": return "byte?";
                case "System.Guid": return "guid?";
                case "System.TimeSpan": return "TimeSpan?";
                default:
                    throw new NotSupportedException("不支持的数据类型:" + type.ToString());
            }
        }

        public static string SqlTypeToVB(string type)
        {
            switch (type)
            {
                case "bigint": return "Long?";
                case "binary": return "Byte()";
                case "bit": return "Boolean?";
                case "char": return "String";
                case "date": return "DateTime?";
                case "datetime": return "DateTime?";
                case "decimal": return "Decimal?";
                case "float": return "Double?";
                case "image": return "Byte()";
                case "int": return "Integer?";
                case "money": return "Decimal?";
                case "nchar": return "String";
                case "ntext": return "String";
                case "numeric": return "Decimal?";
                case "nvarchar": return "String";
                case "real": return "Single?";
                case "smalldatetime": return "DateTime?";
                case "smallint": return "Short?";
                case "smallmoney": return "Decimal?";
                case "text": return "String";
                case "tinyint": return "Byte?";
                case "uniqueidentifier": return "Guid?";
                case "varchar": return "String";
                case "datetimeoffset": return "DateTimeOffset";
                case "xml": return "String";
                case "varbinary": return "Byte()";
                case "datetime2": return "DateTime";
                case "timestamp": return "Byte()";
                case "time": return "TimeSpan";
                case "sysname": return "String";
                default:
                    throw new NotSupportedException("不支持的数据类型:" + type);
            }
        }

        public static string SqlTypeToCS(string type)
        {
            switch (type)
            {
                case "bigint": return "long?";
                case "binary": return "byte[]";
                case "bit": return "bool?";
                case "char": return "string";
                case "date": return "DateTime?";
                case "datetime": return "DateTime?";
                case "decimal": return "decimal?";
                case "float": return "double?";
                case "image": return "byte[]";
                case "int": return "int?";
                case "money": return "decimal?";
                case "nchar": return "string";
                case "ntext": return "string";
                case "numeric": return "decimal?";
                case "nvarchar": return "string";
                case "real": return "float?";
                case "smalldatetime": return "DateTime?";
                case "smallint": return "short?";
                case "smallmoney": return "decimal?";
                case "text": return "string";
                case "tinyint": return "byte?";
                case "uniqueidentifier": return "Guid?";
                case "varchar": return "string";
                case "datetimeoffset": return "DateTimeOffset";
                case "xml": return "string";
                case "varbinary": return "byte[]";
                case "datetime2": return "DateTime";
                case "timestamp": return "byte[]";
                case "time": return "TimeSpan";
                case "sysname": return "string";
                default:
                    throw new NotSupportedException("不支持的数据类型:" + type);
            }
        }

    }
}
