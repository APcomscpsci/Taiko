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
            while (true) {

                string val;

                
                Console.WriteLine("Enter file path of tja, no quotes at the end or beginning pls");
                val = Console.ReadLine();

                string val2;
                Console.WriteLine("Enter file path of output file");
                val2 = Console.ReadLine();

                val = val.Replace("\\", "\\\\");
                val2 = val2.Replace("\\", "\\\\");

                Console.WriteLine(val);
                Console.WriteLine(val2);

                Chart song = Parser.Parse(val); //inputs

                byte[] b = ByteWriter.StringToByteArray(ByteFormatter.buildHexString(song));


                ByteWriter.ByteArrayToFile(val2, b); //outputs




                Console.WriteLine("I think it ran correctly? Press enter to continue");
                Console.ReadLine();

            }

        }
    }

}
