using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 代码生成器
{
    public class Cryptography
    {
        //功能：字符串加密
        //参数：待加密串
        //返回：加密变换后的结果
        public static string EnCode(string inStr)
        {
            string StrBuff = null;
            int IntLen = 0;
            int IntCode = 0;
            int IntCode1 = 0;
            int IntCode2 = 0;
            int IntCode3 = 0;
            int i = 0;

            IntLen = inStr.Trim().Length;

            IntCode1 = IntLen % 3;
            IntCode2 = IntLen % 9;
            IntCode3 = IntLen % 5;
            IntCode = IntCode1 + IntCode3;

            for (i = 1; i <= IntLen; i++)
            {
                StrBuff = StrBuff + Convert.ToChar(Convert.ToInt16(inStr.Substring(IntLen - i, 1).ToCharArray()[0]) - IntCode);
                if (IntCode == IntCode1 + IntCode3)
                {
                    IntCode = IntCode2 + IntCode3;
                }
                else
                {
                    IntCode = IntCode1 + IntCode3;
                }
            }

            return StrBuff + new string(' ', inStr.Length - IntLen);

        }

        //功能：字符串解密
        //参数：待反加密串
        //返回：反加密变换后的结果
        public static string DeCode(string inStr)
        {
            string StrBuff = null;
            int IntLen = 0;
            int IntCode = 0;
            int IntCode1 = 0;
            int IntCode2 = 0;
            int IntCode3 = 0;
            int i = 0;

            StrBuff = "";

            IntLen = inStr.Trim().Length;

            IntCode1 = IntLen % 3;
            IntCode2 = IntLen % 9;
            IntCode3 = IntLen % 5;

            if (IntLen % 2 == 0)
            {
                IntCode = IntCode2 + IntCode3;
            }
            else
            {
                IntCode = IntCode1 + IntCode3;
            }


            for (i = 1; i <= IntLen; i++)
            {
                StrBuff = StrBuff + Convert.ToChar(Convert.ToInt16(inStr.Substring(IntLen - i, 1).ToCharArray()[0]) + IntCode);

                if (IntCode == IntCode1 + IntCode3)
                {
                    IntCode = IntCode2 + IntCode3;
                }
                else
                {
                    IntCode = IntCode1 + IntCode3;
                }
            }

            return StrBuff + new string(' ', inStr.Length - IntLen);
        }
    }
}
