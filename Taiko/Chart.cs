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
        private float scoreInit;
        private float scoreDiff;

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

        public void setScoreInit(double s)
        {
            scoreInit = (float)s;
        }

        public void setScoreDiff(double s)
        {
            scoreDiff = (float)s;
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
