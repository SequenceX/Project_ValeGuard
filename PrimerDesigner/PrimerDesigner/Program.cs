using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerDesigner
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Erstellung Primer1");
            Primer Primer1 = new Primer("ATATATCGCGCTAGATCGAT", 50, 50);
            Console.WriteLine("Sequence: "+Primer1.Sequence);
            Console.WriteLine("ReverseComplement: "+Primer1.GetReverseComplement());
            Console.WriteLine("Tm(Nearest Neighbour): " + Primer1.GetNearestNeighbourTemp());
            Console.WriteLine("Primer1 Done");
            Console.WriteLine("--------------------------------------------------------");







            //AAACGGGGGTTGCCATATATCGCGCTAGATCGATCGATAAAATTGCCTGATCCCCCCTTTTTCGATCGATAAAATTGCCTGATCCCTTTAAATGATCCCCCAAAGAGGGGATCTCTTTTTGATCCCCCTTTTCCCCCCCTGATCCCCCCCAAAAAAGAGGAGGAGAGTAGATGACCACGGATATTAGCACACATATATATGGGCCCCTTTAGTCTCGTA

            Console.WriteLine("Erstellung Seq1");
            GeneSequence seq1 = null;
            try
            {
                seq1 = new GeneSequence("aTaacactgATATATATATATATATATATATATATATATATATATATATATAcTgAgTgAgTcATcATATaaactggATATATATattaATAaaTAatttaTaaATATATATATATATATATtatatatatatatatatatatatatatatatATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATATA");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadKey();
                return 0;
            }
            Console.WriteLine("Seq1 Sequence: " + seq1.Sequence);




            try
            {
                Console.WriteLine("F_Primer Seq: "+seq1.GetGenestrandSeqPrimer(90, 120, 1000,30)[0]);//Output ist ein array. [0] wird ausgegeben =Primer F
                Console.WriteLine(seq1.GetGenestrandSeqPrimer(90, 120, 1000,20)[1]);//Output ist ein array. [0] wird ausgegeben =Primer R
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadKey();
                return 0;
            }


            //Console.WriteLine(seq1.GetGenestrandSeqPrimer(250,120,1000)[0]);//Output ist ein array. [0] wird ausgegeben =Primer F
            // Console.WriteLine( seq1.GetGenestrandSeqPrimer()[1]);//Output ist ein array. [1] wird ausgegeben =Primer R
            //Console.WriteLine(seq1.CheckForDnaChar());




            Console.WriteLine("End of Test");
            //Console.WriteLine(seq1.Sequence.IndexOf();
            Console.WriteLine("End of Test");

            Console.ReadKey();
            return 0;
        }
    }
}
