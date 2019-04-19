using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiko
{
    class TaikoRunner
    {
        static void Main(string[] args)
        {

            Chart song = Parser.Parse("C:\\Users\\Sehun Joo\\Desktop\\tja3ds\\freed\\fd.tja");

            byte[] b = ByteWriter.StringToByteArray(ByteFormatter.buildHexString(song));

            for (int i = 0; i < b.Length; i++)
            {
                
            }
            
            

            ByteWriter.ByteArrayToFile("C:\\Users\\Sehun Joo\\Desktop\\tja3ds\\freed\\out.txt", b);





            Console.ReadLine();

            

        }
    }

}
