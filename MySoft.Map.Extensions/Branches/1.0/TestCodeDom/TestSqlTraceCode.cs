using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.CodeDom;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;

namespace TestCodeDom
{
    [Serializable]
    public partial class TestSqlTrace : BaseEntity
    {
		[DataColumn(IsNullable = true)]
		public string TextData { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] BinaryData { get; set; }
		[DataColumn(IsNullable = true)]
		public long? TransactionID { get; set; }
		[DataColumn(IsNullable = true)]
		public string NTUserName { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? StartTime { get; set; }
		[DataColumn(IsNullable = true)]
		public string SqlText { get; set; }
		[DataColumn(PrimaryKey = true, Identity = true)]
		public int Id { get; set; }
		public int DatabaseID { get; set; }
    }
    public  class TestSqlTraceCode
    {
        public static void Run()
        {
            string connectionString = (from s in System.IO.File.ReadAllLines(@"c:\TestCodeDom.connectionString.txt")
                                       where s.StartsWith(";") == false
                                       select s
                                    ).First();

            Console.WriteLine("Current ConnectionStirng: " + connectionString);
            Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);
            try{
                TestCodeGenerator();
                //RunTestCase();
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

        private static void RunTestCase()
        {
            TestSqlTraceCode tester = new TestSqlTraceCode();
            //插入特殊字符
            tester.Insert();
            
        }
        private void Insert()
        {
            Console.WriteLine("\r\n----------Insert---------------------------");
            TestSqlTrace t1 = new TestSqlTrace
            {
                TextData = "asdf'sdf!#@",
                BinaryData=Encoding.Default.GetBytes("FFFE900201004D006900630072006F0073006F00660074002000530051004C00200053006500720076006500720000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000A00A00F030000005A0048002D00440042000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000C00"),
                DatabaseID=1,
                TransactionID=123123
            };
            //ShowAndExecuteCPQuery(_Tool.AutoGenerateCode.TestSqlTraceCodeTemplate.Insert(t1));
        }
        static void ShowAndExecuteCPQuery(CPQuery query)
        {
            SqlCommand command = query.GetCommand();

            Console.WriteLine("CommandText: " + command.CommandText);
            foreach (SqlParameter parameter in command.Parameters)
                Console.WriteLine("{0}: {1}", parameter.ParameterName, GetParameterValue(parameter.Value));
            int result = query.ExecuteNonQuery();
            Console.WriteLine("ExecuteNonQuery Result: " + result);
            if (result != 1)
                Console.WriteLine("#################### ExecuteNonQuery 结果不为零，请检查。#############################");
          
        }
        static string GetParameterValue(object val)
        {
            if (DBNull.Value.Equals(val))
                return "DBNull";

            if (val == null)
                return "null";

            if (val.GetType() == typeof(byte[]))
                return "0x" + BitConverter.ToString((byte[])val).Replace("-", "");
            else
                return val.ToString();

        }
        private static void TestCodeGenerator()
        {
            CodeGenerator generator = new CodeGenerator(typeof(TestSqlTrace));
            string classname = generator.GetCode("TestSqlTraceCodeTemplate");
            classname = CodeGenerator.GetCodeHeader() + classname + "}";
            System.IO.File.WriteAllText("..\\TestSqlTraceCodeTemplate.cs", classname, Encoding.UTF8);
        }
    }
}
