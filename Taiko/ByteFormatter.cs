using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiko
{
    class ByteFormatter //this is the class that formatts all the bytes into the String it will become. This is where the magic happens
    {
        public static String buildHexString(Chart song)
        {
            string s = "34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42 34 33 C8 41 67 26 96 42 22 E2 D8 42";
            s += "00 04 39 00";//null
            s += "10 27 00 00";//total soul gauge size 1B4-1B7
            s += "40 1F 00 00";//total lifepoints (Oni, change for other difficulties) 1B8-1BB
            s += "20 00 00 00";//Each greats lifebar points 1BC-1BF
            s += "10 00 00 00";//Each greats lifebar points 1C0-1C3
            s += "BF FF FF FF";//1C4-1C7 Each BAD(不可)'s earned Tamasii value (Hex). (It's a negative number. Negative number in Hex should be FFFFFFxx.)
            s += "00 00 01 00 00 00 01 00 00 00 01 00 14 00 00 00 0A 00 00 00 00 00 00 00 01 00 00 00 14 00 00 00 0A 00 00 00 01 00 00 00 1E 00 00 00 1E 00 00 00 00 00 00 00";
            s += "E8 DA 37 00"; //1FC-1FF It used to be filled with Max Score value, but it's unused in current platforms.

           

            s += intToHexString(song.getMeasures().Count()) + "00 00";   // Total amount of Bars (Hex).200-203

            s += "56 37 40 00"; //noone knows what dis does 204-207

            for (int i = 0; i < song.getMeasures().Count; i++)
            {
                s += measureWriter(song, i);
            }

            s = s.Replace(" ", "");
            s = s.Replace("-", "");
            return s;
        }

        public static String measureWriter(Chart song, int measurePos)
        {
            List<Measure> measures = song.getMeasures();
            Measure currentMeasure = measures[measurePos];
            String s = "";

            

            s += floatToHexString(currentMeasure.getBPM()); //adds measures BPM
            s += floatToHexString(calculateMeasureOffset(song, measurePos, currentMeasure)); //adds measure offset
            if (currentMeasure.getIsGogo()) //adds Gogotime status
            {
                s += "01";
            }
            else
            {
                s += "00";
            }

            if (true) //barline status, always true for now
            {
                s += "01";
            }
            s += "6C6C"; //Dummy Data?
            s += "FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF"; //usually for split path

            String rM = "";//reduced measure in order to count how many notes are in a measure
            rM = currentMeasure.ToString().Replace("0", ""); rM = rM.Replace("8", "");
            int notesCount = rM.Count();

            s += "F4 FC 12 00";

            s += intToHexString(notesCount) + " 00 00"; //adds notecount of measure


            s += floatToHexString(currentMeasure.getHs());//hiSpeed

            String noteHex = "";
            for (int i = 0; i < currentMeasure.getNotes().Count; i++)
            {
                Note note = currentMeasure.getNotes()[i];

                

                switch (note.getType())
                {
                    case 1: //don
                        noteHex += "01 00 00 00" + floatToHexString(calculateNoteOffset(currentMeasure, i)) + "00 00 00 00 00 00 00 00";
                        noteHex += intToHexString(song.getScoreInit())+ intToHexString(4*song.getScoreDiff()); //two ints that represent how much score each notes worth, and how much it increases per combo
                        noteHex += "00 00 00 00";
                        break;
                    case 2: //kat
                        noteHex += "04 00 00 00" + floatToHexString(calculateNoteOffset(currentMeasure, i)) + "00 00 00 00 00 00 00 00";
                        noteHex += intToHexString(song.getScoreInit()) + intToHexString(4 * song.getScoreDiff()); //two ints that represent how much score each notes worth, and how much it increases per combo
                        noteHex += "00 00 00 00";
                        break;
                    case 3: //large don
                        noteHex += "07 00 00 00" + floatToHexString(calculateNoteOffset(currentMeasure, i)) + "00 00 00 00 00 00 00 00";
                        noteHex += intToHexString(song.getScoreInit()) + intToHexString(4 * song.getScoreDiff()); //two ints that represent how much score each notes worth, and how much it increases per combo
                        noteHex += "00 00 00 00";
                        break;
                    case 4: //large kat
                        noteHex += "08 00 00 00" + floatToHexString(calculateNoteOffset(currentMeasure, i)) + "00 00 00 00 00 00 00 00";
                        noteHex += intToHexString(song.getScoreInit()) + intToHexString(4 * song.getScoreDiff()); //two ints that represent how much score each notes worth, and how much it increases per combo
                        noteHex += "00 00 00 00";
                        break;
                    case 5: //drumroll
                        noteHex += "06 00 00 00" + floatToHexString(calculateNoteOffset(currentMeasure, i)) + "00 00 00 00 00 00 00 00";
                        noteHex += "CC 01 C2 01"; //two ints that represent how much score each notes worth, and how much it increases per combo
                        noteHex += floatToHexString(calculateNoteOffset(currentMeasure, note.getBalloonLength()));
                        noteHex += "00 00 00 00 00 00 00 00";
                        break;
                    case 6: //large drumroll
                        noteHex += "09 00 00 00" + floatToHexString(calculateNoteOffset(currentMeasure, i)) + "00 00 00 00 00 00 00 00";
                        noteHex += "CC 01 C2 01"; //two ints that represent how much score each notes worth, and how much it increases per combo
                        noteHex += floatToHexString(calculateNoteOffset(currentMeasure, note.getBalloonLength()));
                        noteHex += "00 00 00 00 00 00 00 00";
                        break;
                    case 7: //ballooon
                        noteHex += "0A 00 00 00" + floatToHexString(calculateNoteOffset(currentMeasure, i)) + "00 00 00 00 00 00 00 00";
                        noteHex += intToHexString(note.getBalloonSize()) + "00 00"; //balloon size
                        
                        noteHex += floatToHexString(calculateNoteOffset(currentMeasure, note.getBalloonLength()));

                        break;
                }

            }
            
            s += noteHex;

            s += "00 00 00 00 00 00 80 3F 00 00 00 00 00 00 80 3F";

            return s;
        }





        public static float calculateMeasureOffset(Chart song, int measurePos, Measure measure)  //returns the offset of a measure in float
        {
            return song.getOffset() + measurePos * (240000 / measure.getBPM());
        }

        public static  float calculateNoteOffset(Measure measure, int notePos)  //returns the offset of a note in a measure
        {
            return  (((float)notePos)/48) * (240000 / measure.getBPM());
        }

        public static String floatToHexString(float f) //converts a float number to a string of hex
        {
            float vIn = f;
            byte[] vOut = BitConverter.GetBytes(vIn);


            string hex = BitConverter.ToString(vOut);

            return hex;

        }


        public static string intToHexString(int i) //convert int to little endian hex string
        {
            string r = i.ToString("X");

            String flippedByte = r.PadLeft(4, '0');
            flippedByte = r.PadLeft(4, '0').Substring(2) + r.PadLeft(4, '0').Substring(0, 2);

            return flippedByte;


        }



        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
