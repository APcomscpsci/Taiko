using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taiko
{
    class Parser
    {
        public static Chart Parse(String fileName)
        {

            Chart song = new Chart();

            ArrayList unprocessedLines = new ArrayList();

            String[] lines = File.ReadAllLines(fileName);


            foreach (String line in lines)
            { //parses the strings for song properties

                if (line.Length>=6&&line.Substring(0, 6).Equals("TITLE:"))
                {
                    song.setTitle(line.Substring(6));
                }
                else if (line.Length >= 4 && line.Substring(0, 4).Equals("BPM:"))
                {
                    song.setBPM(Double.Parse(line.Substring(4)));
                }
                else if (line.Length >= 7 && line.Substring(0, 7).Equals("OFFSET:"))
                {
                    song.setOffset(Double.Parse(line.Substring(7)));
                }
                else if (line.Length >= 10 && line.Substring(0, 10).Equals("SCOREINIT:"))
                {
                    song.setScoreInit(Double.Parse(line.Substring(10)));
                }
                else if (line.Length >= 10 && line.Substring(0, 10).Equals("SCOREDIFF:"))
                {
                    song.setScoreDiff(Double.Parse(line.Substring(10)));
                } else if (line.Length >= 8 && line.Substring(0, 8).Equals("BALLOON:"))
                {
                   
                    String[] s = (line.Substring(8).Split(','));
                    int[] b = new int[s.Length];
                    for(int i = 0; i < s.Length; i++)
                    {

                        b[i] = Int32.Parse(s[i]);
                    }

                    song.setBalloons(b);
                }

            }

            bool isMeasures = false;
            bool readGogo = false;

            for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Equals("#END")) break;


                else if (isMeasures == true)
                {
                    

                    if (lines[i].Equals("")) ;

                    else if (lines[i].Equals("#GOGOSTART"))
                    {
                        readGogo = true;
                    }

                    else if (lines[i].Equals("#GOGOEND"))
                    {
                        readGogo = false;
                    }




                    else if (lines[i].Substring(lines[i].Length - 1).Equals(","))
                    {
                        song.addMeasure(new Measure(lines[i], readGogo, song.getBPM()));
                    }



                }

                else if (lines[i].Equals("#START"))
                {
                    isMeasures = true;
                }
            }

            balloonProcessing(song);

            return song;

            
            
        }
        

        public static void balloonProcessing(Chart chart)//run this after chart is finished loading in
        {
            List<Measure> measures = chart.getMeasures();
            int[] balloons = chart.getBalloons();

            int queuedBalloonSize = 0;

            for(int i=0;i<measures.Count;i++) //loops through the measure List to find balloons
            {

                List<Note> currentMeasure = measures[i].getNotes();
                
                for(int j = 0; j < currentMeasure.Count; j++) //loops through the measure to find balloon
                {

                    if (currentMeasure[j].getType() == 5)
                    {

                        int balloonLength = 0;

                        for (int d = i; d < measures.Count; d++) //loops through the measure List to find balloon end
                        {
                            List<Note> currentlySearchingMeasure = measures[d].getNotes();
                            for (int k = 0; k < currentMeasure.Count; k++) //loops through the measure to find balloon end
                            {
                                if (d == i && k==0)//if first measure make k=j
                                { 
                                    k = j;
                                }


                                if (currentlySearchingMeasure[k].getType() == 8)
                                {
                                    d = measures.Count();
                                    break;
                                }
                                else
                                {
                                    balloonLength++;
                                }

                            }

                        }

                        currentMeasure[j].setBalloonLength(balloonLength);

                    }

                    else if (currentMeasure[j].getType() == 6)
                    {

                        int balloonLength = 0;

                        for (int d = i; d < measures.Count; d++) //loops through the measure List to find balloon end
                        {
                            List<Note> currentlySearchingMeasure = measures[d].getNotes();
                            for (int k = 0; k < currentMeasure.Count; k++) //loops through the measure to find balloon end
                            {
                                if (d == i && k == 0)//if first measure make k=j
                                {
                                    k = j;
                                }


                                if (currentlySearchingMeasure[k].getType() == 8)
                                {
                                    d = measures.Count();
                                    break;
                                }
                                else
                                {
                                    balloonLength++;
                                }

                            }

                        }

                        currentMeasure[j].setBalloonLength(balloonLength);

                    }

                    else if (currentMeasure[j].getType() == 7)
                    {

                        int balloonLength = 0;

                        for (int d = i; d < measures.Count; d++) //loops through the measure List to find balloon end
                        {
                            List<Note> currentlySearchingMeasure = measures[d].getNotes();
                            for (int k = 0; k < currentMeasure.Count; k++) //loops through the measure to find balloon end
                            {
                                if (d == i && k == 0)//if first measure make k=j
                                {
                                    k = j;
                                }


                                if (currentlySearchingMeasure[k].getType() == 8)
                                {
                                    d = measures.Count();
                                    break;
                                }
                                else
                                {
                                    balloonLength++;
                                }

                            }

                        }

                        currentMeasure[j].setBalloonLength(balloonLength);
                        currentMeasure[j].setBalloonSize(chart.getBalloons()[queuedBalloonSize]);
                        queuedBalloonSize++;
                    }

                }
                
            }

        }

    }
}
