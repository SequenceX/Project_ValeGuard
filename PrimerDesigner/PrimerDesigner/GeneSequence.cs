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
// Diese Klasse benötigt Klasse Primer zur berechnung der TM.
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
        public string[] GetGenestrandSeqPrimer(int distanceBetweenPrimer = 90, int minSeqLänge = 120, int maxSeqLänge = 1000, int searchStartArea = 20)
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
            int fPrimerStartPos;
            int firstFPrimerstartPos;
            firstFPrimerstartPos = midOfSeq - (distanceBetweenPrimer / 2);
            fPrimerStartPos = firstFPrimerstartPos;
            fPrimerStartPair = sequence.Substring(fPrimerStartPos,2);
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
                        fHighTempStart = true;
                        break; 
                    }
                    else
                    {
                        fPrimerStartPos -= 1;
                        fPrimerStartPair = sequence.Substring(fPrimerStartPos, 2);
                    }
                }
                //Search for 1x high Temp Start
                if (fHighTempStart==false)
                {
                    fPrimerStartPos = firstFPrimerstartPos;
                    fPrimerStartPair = sequence.Substring(fPrimerStartPos, 1);
                    for (int i = 0; i < searchStartArea; i++)
                    {
                        if (fPrimerStartPair == "G" || fPrimerStartPair == "C" )
                        {
                            // Console.WriteLine(fPrimerStartPair);
                            fMidTempStart = true;
                            break;
                        }
                        else
                        {
                            fPrimerStartPos -= 1;
                            fPrimerStartPair = sequence.Substring(fPrimerStartPos, 1);
                        }
                     }
                 }
                //Add Second Base on MidTemp Start
                if (fMidTempStart == true)
                {
                    fPrimerStartPos -= 1;
                    fPrimerStartPair = sequence.Substring(fPrimerStartPos, 2);
                }
                //Low Temp Star
                if (fHighTempStart == false && fMidTempStart == false)
                {
                    fPrimerStartPos = firstFPrimerstartPos;
                    fPrimerStartPair = sequence.Substring(fPrimerStartPos, 2);
                }
                //Unic Check
                firstSixFBases = sequence.Substring(fPrimerStartPos - 4, 2 + 4);
                var matchesF = Regex.Matches(sequence, firstSixFBases);
                if (matchesF.Count==1)
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
            //Create f Primers
            Primer fPrimer = new Primer(sequence.Substring(fPrimerStartPos - 4, 2 + 4), 50, 50);
            Primer nextFPrimer = new Primer(sequence.Substring(fPrimerStartPos - 4-1, 2 + 4+1), 50, 50);
            int nextPrimerCounter = 0;
            //Primer verlägern bis Abweichung zu TM minimal
            while (Math.Abs(targetTM - fPrimer.GetNearestNeighbourTemp()) > Math.Abs(targetTM - nextFPrimer.GetNearestNeighbourTemp()))
            {  
                fPrimer.Sequence = sequence.Substring(fPrimerStartPos - 4 - nextPrimerCounter, 2 + 4 + nextPrimerCounter);
                nextFPrimer.Sequence = sequence.Substring(fPrimerStartPos - 4 - nextPrimerCounter - 1, 2 + 4 + nextPrimerCounter + 1);
                nextPrimerCounter++;
            }
            //Return F Primer
            strReturn[0] = fPrimer.Sequence;
                //REV PRIMER 
                string firstSixRBases;
                string rPrimerStartPair;
                int rPrimerStartPos;
                int firstRPrimerstartPos;
                firstRPrimerstartPos = fPrimerStartPos + distanceBetweenPrimer + 2;
                rPrimerStartPos = firstRPrimerstartPos;
                rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);
            //Loop for search Primer
            int rUnicCounter = 0;
            bool rUnic = false;
            while (rUnic == false)
            {
                bool rHighTempStart = false;
                bool rMidTempStart = false;
                //Search for 2x high Temp Start
                for (int i = 0; i < searchStartArea; i++)
                {
                    if (rPrimerStartPair == "GG" || rPrimerStartPair == "GC" || rPrimerStartPair == "CG" || rPrimerStartPair == "CC")
                    {
                        rHighTempStart = true;
                        break; 
                    }
                    else
                    {
                        rPrimerStartPos += 1;
                        rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);            
                    }
                }
                //Search for 1x high Temp Start
                if (rHighTempStart == false)
                {
                    rPrimerStartPos = firstRPrimerstartPos;
                    rPrimerStartPair = sequence.Substring(rPrimerStartPos, 1);
                    for (int i = 0; i < searchStartArea; i++)
                    {
                        if (rPrimerStartPair == "G" || rPrimerStartPair == "C")
                        {
                            rMidTempStart = true;
                            break;
                        }
                        else
                        {
                            rPrimerStartPos += 1;
                            rPrimerStartPair = sequence.Substring(rPrimerStartPos, 1);
                        }
                    }
                }
                //Add Second Base on MidTemp Start
                if (rMidTempStart == true)
                {
                    rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);
                }
                //Low Temp Star
                if (rHighTempStart == false && rMidTempStart == false)
                {
                    rPrimerStartPos = firstRPrimerstartPos;
                    rPrimerStartPair = sequence.Substring(rPrimerStartPos, 2);
                }
                //Unic Check
                firstSixRBases = sequence.Substring(rPrimerStartPos, 2 + 4);
                var matchesR = Regex.Matches(sequence, firstSixRBases);
                if (matchesR.Count == 1)
                {
                    rUnic = true;
                }
                firstRPrimerstartPos--;
                rUnicCounter++;
                if (rUnicCounter == searchStartArea)
                {
                    throw new Exception("Es konnte kein Unic Primer innerhalb der suchkreterien gefunden werden.");
                }
            }
            //Create r Primers
            Primer rPrimer = new Primer(sequence.Substring(rPrimerStartPos, 2 + 4), 50, 50);
            Primer nextRPrimer = new Primer(sequence.Substring(rPrimerStartPos, 2 + 4 + 1), 50, 50);
            int nextRPrimerCounter = 0;
            //Primer verlägern bis Abweichung zu TM minimal
            while (Math.Abs(targetTM - rPrimer.GetNearestNeighbourTemp()) > Math.Abs(targetTM - nextRPrimer.GetNearestNeighbourTemp()))
            {
                rPrimer.Sequence = sequence.Substring(rPrimerStartPos , 2 + 4 + nextRPrimerCounter);
                nextRPrimer.Sequence = sequence.Substring(rPrimerStartPos , 2 + 4 + nextRPrimerCounter + 1);
                nextRPrimerCounter++;
            }
            //Return R Primer
            rPrimer.Sequence = rPrimer.GetReverseComplement();
            strReturn[1] = rPrimer.Sequence;
            return strReturn;
        }
    }
 }

