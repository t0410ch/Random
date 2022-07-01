using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace 二元推导检测方法
{
    class Program
    {
        //动态调用c++的函数库
        [DllImport("DemoCsharpImportCplus.dll",
            EntryPoint = "Erfc1",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl
            )]
            extern static double Erfc(double s);

        static void Main(string[] args)
        {
            Console.WriteLine("请输入你想产生多大长度的01序列：");
            string Long = Console.ReadLine();
            try
            {
                int zp = int.Parse(Long);
                string pz = randomset(zp);//产生随机序列
                Console.WriteLine("请输入你想按位异或多少次：");
                string ci = Console.ReadLine();
                int q = int.Parse(ci);
                string s1 = yihuo(pz);//第一次按位异或
                for (int i = 0; i < q-1; i++)
                {
                    s1 = yihuo(s1);
                }
                Console.WriteLine(save(s1, zp, q));
                if (save(s1, zp, q) > 0.01)
                {
                    Console.WriteLine("通过！");
                }
                else Console.WriteLine("未通过！");
                
            }
            catch (Exception)
            {

                Console.WriteLine("ERROR!");
            }

        }
        //产随机序列
        private static string randomset(int i)
        {
            Random r = new Random();
            string str = "";
            for (int j = 0; j < i; j++)
            {
                str += (r.Next(0, 2)).ToString();
            }
            return str;
        }
        //按位异或
        private static string yihuo(string s)
        {
            string str = "";

            for (int i = 0; i < s.Length-1; i++)
            {
                str += s[i] ^ s[i + 1];
            }
            return str;
        }
        //求值
        private static double save(string s,int n,int k )
        {
            int[] zhi1 = new int[s.Length];
            int sz = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                {
                    zhi1[i] = -1;
                    sz += zhi1[i];
                }
                else
                {
                    zhi1[i] = 1;
                    sz += zhi1[i];
                }
            }
            if (sz < 0)
            {
                sz = -sz;
            }
            double v;
            v = (sz) / Math.Pow((n - k), 0.5);
            if (v<0)
            {
                v = -v;
            }
            return Erfc(v/ Math.Pow(2, 0.5));

        }
    }

}
