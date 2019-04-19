using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiko
{


    public class Note
    {
        private int type;
        private int balloonLength;
        private int balloonSize;


        public Note(int s)
        {
            type = s;
            balloonLength = 0;
            balloonSize = 0;
        }

        public int getType()
        {
            return type;
        }

        public void setBalloonLength(int i)
        {
            balloonLength = i;
        }

        public void setBalloonSize(int i)
        {
            balloonSize = i;
        }

        public int getBalloonSize()
        {
            return balloonSize;
        }

        public int getBalloonLength()
        {
            return balloonLength;
        }


        public override String ToString()
        {

            return type+"";
        }

   

        

        /*	0=empty
            1=don
            2=kat
            3=large don
            4=large kat
            5=where the drumroll starts
            6=where the large drumroll starts
            7=where the balloon note starts
            8=where the drumroll/balloon ends  */
    }
}
