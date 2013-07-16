using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 代码生成器
{
    public class SqlObject
    {
        public string Name { get; set; }
        public ObjectType Type { get; set; }
        public List<SqlObject> Childs { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum ObjectType
    {
        Folder,
        Table,
        View,
        StoreProcedure,
        SQL
    }
}
