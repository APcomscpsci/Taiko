using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiko
{


    public class Chart
    {

        private String title;

        private float BPM;
        private float offset;  
        private int scoreInit;
        private int scoreDiff;

        private int[] balloons;
        private List<Measure> measures; //an arraylist of measures


        public Chart()
        {
            measures = new List<Measure>();
        }

        public void setTitle(String s)
        {
            title = s;
        }

        public void setBPM(double b)
        {
            BPM = (float)b;
        }

        public float getBPM()
        {
            return BPM;
        }

        public void setOffset(double o)
        {
            offset = (float)o;
        }

        public float getOffset()
        {
            return offset;
        }

        public void setScoreInit(int s)
        {
            scoreInit = s;
        }

        public int getScoreInit()
        {
            return scoreInit;
        }
        public void setScoreDiff(int s)
        {
            scoreDiff = s;
        }

        public int getScoreDiff()
        {
            return scoreDiff;
        }

        public void setBalloons(int[] b)
        {
            balloons = b;
        }

        public void addMeasure(Measure m)
        {
            measures.Add(m);
        }

        public List<Measure> getMeasures()
        {
            return measures;
        }

        public int[] getBalloons()
        {
            return balloons;
        }

        public String getBalloonsToString()
        {
            String n = "";
            foreach(int i in balloons)
            {
                n += " "+ i;
            }

            return n;
        }

        public override string ToString()
        {
            String s = "";

            foreach(Measure m in measures)
            {
                foreach(Note n in m.getNotes())
                {
                    s += n.ToString();
                }
                s += '\n';
            }
            return s;
        }
    }
}
