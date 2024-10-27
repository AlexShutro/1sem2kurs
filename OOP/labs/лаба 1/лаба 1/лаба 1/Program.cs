using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //a

            Console.Write("Int: ");
            int i = 2344;
            i = Convert.ToInt32(Console.ReadLine());
            Console.Write("Uint: ");
            uint ui = Convert.ToUInt32(Console.ReadLine());
            Console.Write("Short: ");
            short s = Convert.ToInt16(Console.ReadLine());
            Console.Write("Ushort: ");
            ushort us = Convert.ToUInt16(Console.ReadLine());
            Console.Write("Long: ");
            long l = Convert.ToInt64(Console.ReadLine());
            Console.Write("Ulong: ");
            ulong ul = Convert.ToUInt64(Console.ReadLine());
            Console.Write("Bool: ");
            bool bl = Convert.ToBoolean(Console.ReadLine());
            Console.Write("Byte: ");
            byte b = Convert.ToByte(Console.ReadLine());
            Console.Write("Sbyte: ");
            sbyte sb = Convert.ToSByte(Console.ReadLine());
            Console.Write("Double: ");
            double d = Convert.ToDouble(Console.ReadLine());
            Console.Write("String: ");
            string str = Convert.ToString(Console.ReadLine());
            Console.Write("Char: ");
            char ch = Convert.ToChar(Console.ReadLine());

            Console.WriteLine($"\nInt: {i}\nUint: {ui}\nShort: {s}\nUshort: {us}\n");
            Console.WriteLine($"Long: {l}\nUlong: {ul}\nBool: {bl}\nDouble: {d}\n");
            Console.WriteLine($"Byte: {b}\nSbyte: {sb}\nString: {str}\nChar: {ch}\n");

            //b

            //неявное
            int numi = 217450097;
            long lnNum = numi;

            short nums = 34;
            int innum = nums;

            byte numb = 2;
            short shnum = numb;

            uint numui = 234343344;
            ulong ulnum = numui;

            double dnum = numi;

            //явное
            double numd = 1312.34;
            int x = (int)numd;

            long numl = 23433;
            x = (int)numl;

            nums = (short)numl;

            float numfl = (float)numd;

            decimal numdec = (decimal)numd;

            //c

            object o = i;
            i = (int)o;

            o = ui;
            ui = (uint)o;

            o = s;
            s = (short)o;

            o = us;
            us = (ushort)o;

            o = l;
            l = (long)o;

            o = ul;
            ul = (ulong)o;

            o = bl;
            bl = (bool)o;

            o = d;
            d = (double)o;

            o = b;
            b = (byte)o;

            o = sb;
            sb = (sbyte)o;

            o = str;
            str = (string)o;

            o = ch;
            ch = (char)o;

            //d

            var qwe = 7;
            qwe++;
            Console.WriteLine(qwe);

            var str_v = "hi";
            str_v = str;
            Console.WriteLine(str_v);

            dynamic dyn = 12;
            dyn = "rrf";
            Console.WriteLine(dyn);

            //e

            int? number = null;

            if (number.HasValue)
            {
                Console.WriteLine(number);
            }
            else
            {
                Console.WriteLine("параметр равен null");
            }

            //f

            var varf = 5;
            // varf = 434.434;

        }
    }
}

