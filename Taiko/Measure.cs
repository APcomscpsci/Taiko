using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Taiko
{

    public class Measure
    {
        private List<Note> notes;

        private bool isGogo;

        private float BPM;

        public Measure(String line, bool isGog, float mBPM)
        {
            ToNoteArray(Expand(line));
            isGogo = isGog;
            BPM = mBPM;

        }

        public Measure()
        {

        }

        public float getBPM()
        {
            return BPM;
        }

        public static String Expand(String line) //please make sure only measure data is going in, not extraneous things, e.g. timesigchange, gogotime change
        {
            line = line.Substring(0, line.Length - 1);
            if (line.Length == 0) line = "000000000000000000000000000000000000000000000000";
            else
            {
                String zero = "0"; String one = "1"; String two = "2"; String three = "3"; String four = "4"; String five = "5"; String six = "6"; String seven = "7"; String eight = "8";
                int additionalZeroes = (48 / line.Length); //only works if 48th placement, honestly this whole method is fucked but efficiency is later
                line = line.Replace("0", zero.PadRight(additionalZeroes, '0'));
                line = line.Replace("1", one.PadRight(additionalZeroes, '0'));
                line = line.Replace("2", two.PadRight(additionalZeroes, '0'));
                line = line.Replace("3", three.PadRight(additionalZeroes, '0'));
                line = line.Replace("4", four.PadRight(additionalZeroes, '0'));
                line = line.Replace("5", five.PadRight(additionalZeroes, '0'));
                line = line.Replace("6", six.PadRight(additionalZeroes, '0'));
                line = line.Replace("7", seven.PadRight(additionalZeroes, '0'));
                line = line.Replace("8", eight.PadRight(additionalZeroes, '0'));
            }

            return line;
        }

        public void ToNoteArray(String line)
        {
            List<Note> notes = new List<Note>();
            foreach (char c in line)
            {
                
                notes.Add(new Note(Int32.Parse(c + "")));
            }
            this.notes = notes;
        }


        public List<Note> getNotes()
        {
            return notes;
        }

        public bool getIsGogo()
        {
            return isGogo;
        }

        public override String ToString()
        {
            String s = "";

            foreach(Note n in notes)
            {
                s += n;
            }

            return s;
        }
    }
}
