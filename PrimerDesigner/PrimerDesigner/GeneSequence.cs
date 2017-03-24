using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        */

        //Konstruktor
        public GeneSequence(string DNASequence)
        {
            this.sequence = DNASequence.ToUpper();
            this.länge = sequence.Length;
            if (true)
            {
                throw new Exception("Bad News");
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
            }
        }
        private int länge;
        public int Länge
        {
            get
            { return länge; }
        }


        //Methoden








        public string[] GetGenestrandSeqPrimer(int distanceBetweenPrimer = 90, int minSeqLänge = 120, int maxSeqLänge = 1000) //Min und Max Länge für Primer mit vorgeschlagener, überschreibbarer anzeige
        {
            string[] strReturn = new string[2] { "", "" };
            //Check der Min&Max Länge für Genestrand
            if (länge < minSeqLänge || länge > maxSeqLänge)
            {
                return strReturn;
            }



            //strReturn[0] = "PCRF";
            //strReturn[1] = "PCRR";
            return strReturn;
        }




        public bool CheckForDnaChar()
        {
            int gcount = sequence.ToUpper().Count(f => f == 'G');
            int ccount = sequence.ToUpper().Count(f => f == 'C');
            int acount = sequence.ToUpper().Count(f => f == 'A');
            int tcount = sequence.ToUpper().Count(f => f == 'T');
            bool check;
            if (gcount+ ccount+ acount+ tcount == länge)
            {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }










    }




    }

