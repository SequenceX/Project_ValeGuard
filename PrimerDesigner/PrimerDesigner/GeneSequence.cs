using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrimerDesigner
{
    class GeneSequence
    {
        /*
        Planed:
        Int GS Primer:
           DONE Check der Min&Max Länge für Genestrand 150?-1000
           Design in Mitte der Seq
           Try Start of Primer GG/CC/GC/CG
           Unic Base Check
           Primer erweitern bis Tm passt

            Primer Quality berechnen  Start, Unic...
        */



        //Konstruktor
        public GeneSequence(string DNASequence)
        {
            this.sequence = DNASequence.ToUpper();
            this.länge = sequence.Length;
            if (!this.CheckForDnaChar())
            {
                throw new Exception("None DNA Char found!");
            }          
        }
        //Getter / Setter
        private string sequence;
        public string Sequence
        {
            get
            { return sequence; }
            set
            {
                sequence = value.ToUpper();
                länge = sequence.Length;
                if (!this.CheckForDnaChar())
                {
                    throw new Exception("None DNA Char found!");
                }
            }
        }
        private int länge;
        public int Länge
        {
            get
            { return länge; }
        }
        //Methoden
        public bool CheckForDnaChar()
        {
            //Return true if onyl DNA Chars are used
            int gcount = sequence.ToUpper().Count(f => f == 'G');
            int ccount = sequence.ToUpper().Count(f => f == 'C');
            int acount = sequence.ToUpper().Count(f => f == 'A');
            int tcount = sequence.ToUpper().Count(f => f == 'T');
            bool check;
            if (gcount + ccount + acount + tcount == länge)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }
        public string[] GetGenestrandSeqPrimer(int distanceBetweenPrimer = 90, int minSeqLänge = 120, int maxSeqLänge = 1000, int searchStartArea = 20) //Min und Max Länge für Primer mit vorgeschlagener, überschreibbarer anzeige
        {
            string[] strReturn = new string[2] { "", "" };
            double targetTM = 58.00D;
            //Check der Min&Max Länge für Genestrand
            if (länge < minSeqLänge || länge > maxSeqLänge)
            {
                throw new Exception("Sequenzlänge liegt auserhalb des zulässingen Bereichs."+ minSeqLänge + " - "+ maxSeqLänge);
            }
            if (distanceBetweenPrimer >(minSeqLänge + searchStartArea + 80))
            {
                throw new Exception("Der Abstand der Primer ist zu groß gewählt so das keine Primer mehr ersellt werden können.");
            }
            if ((länge/2) < ((distanceBetweenPrimer/2) + searchStartArea + 40))
            {
                throw new Exception("Der Abstand der Primer oder die Search Area ist zu groß gewählt so das keine Primer mehr ersellt werden können.");
            }
            int midOfSeq=Länge/2;
            //Search for F Primer
            string firstSixFBases;
            string fPrimerStartPair;
            int fPrimerstartPos;
            int firstFPrimerstartPos;
            firstFPrimerstartPos = midOfSeq - (distanceBetweenPrimer / 2);
            fPrimerstartPos = firstFPrimerstartPos;
            fPrimerStartPair = sequence.Substring(fPrimerstartPos,2);
            //Loop for search Primer
            int fUnicCounter = 0;
            bool fUnic = false;
            while (fUnic==false)
            {
                //Search for 2x high Temp Start
                bool fHighTempStart = false;
                bool fMidTempStart = false;
                for (int i = 0; i < searchStartArea; i++)
                {
                    if (fPrimerStartPair == "GG" || fPrimerStartPair == "GC" || fPrimerStartPair == "CG" || fPrimerStartPair == "CC")
                    {
                        // Console.WriteLine(fPrimerStartPair);
                        fHighTempStart = true;
                        break; // i = searchStartArea;
                    }
                    else
                    {
                        fPrimerstartPos -= 1;
                        fPrimerStartPair = sequence.Substring(fPrimerstartPos, 2);
                        //Console.WriteLine(fPrimerStartPair);
                    }
                }
                //Search for 1x high Temp Start
                if (fHighTempStart==false)
                {
                    fPrimerstartPos = firstFPrimerstartPos;
                    fPrimerStartPair = sequence.Substring(fPrimerstartPos, 1);
                    for (int i = 0; i < searchStartArea; i++)
                    {

                        if (fPrimerStartPair == "G" || fPrimerStartPair == "C" )
                        {
                            // Console.WriteLine(fPrimerStartPair);
                            fMidTempStart = true;
                            break; // i = searchStartArea;
                        }
                        else
                        {
                            fPrimerstartPos -= 1;
                            fPrimerStartPair = sequence.Substring(fPrimerstartPos, 1);
                            //Console.WriteLine(fPrimerStartPair);
                        }
                     }
                 }
                if (fMidTempStart == true)
                {
                    fPrimerstartPos -= 1;
                    fPrimerStartPair = sequence.Substring(fPrimerstartPos, 2);
                }

                if (fHighTempStart == false && fMidTempStart == false)
                {
                    fPrimerstartPos = firstFPrimerstartPos;
                    fPrimerStartPair = sequence.Substring(fPrimerstartPos, 2);
                }
                //Unic Check
                firstSixFBases = sequence.Substring(fPrimerstartPos - 4, 2 + 4);
                var matches = Regex.Matches(sequence, firstSixFBases);
                if (matches.Count==1)
                {
                    fUnic = true;
                }
                firstFPrimerstartPos--;
                fUnicCounter++;
                if (fUnicCounter==searchStartArea)
                {
                    throw new Exception("Es konnte kein Unic Primer innerhalb der suchkreterien gefunden werden.");
                }

            }
            Primer fPrimer = new Primer(sequence.Substring(fPrimerstartPos - 4, 2 + 4), 50, 50);
            Primer nextFPrimer = new Primer(sequence.Substring(fPrimerstartPos - 4-1, 2 + 4+1), 50, 50);
            int nextPrimerCounter = 0;
            //Primer verlägern bis Abweichung zu TM minimal
            while (Math.Abs(targetTM - fPrimer.GetNearestNeighbourTemp()) > Math.Abs(targetTM - nextFPrimer.GetNearestNeighbourTemp()))
            {
                
                fPrimer.Sequence = sequence.Substring(fPrimerstartPos - 4 - nextPrimerCounter, 2 + 4 + nextPrimerCounter);
                nextFPrimer.Sequence = sequence.Substring(fPrimerstartPos - 4 - nextPrimerCounter - 1, 2 + 4 + nextPrimerCounter + 1);
                nextPrimerCounter++;
            }
            strReturn[0] = fPrimer.Sequence;


            //REV PRIMER 











            //strReturn[1] = "PCRR";
            return strReturn;
        }



     }
 }

