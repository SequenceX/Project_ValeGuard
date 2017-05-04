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
                seq1 = new GeneSequence("GTGACCACAGGCGTCCACAGCCAGGTTCAGCTGGTTCAGTCTGGCGCTGAAGTCAAGAAACCTGGGAGTTCCGTGAAGGTGTCCTGCAAAGCTTCTGGAGGGACCTTTTCCTCACTCGCCATTAGCTGGGTACGCCAAGCACCAGGTCAGGGTCTGGAATGGATGGGAGGGATAATCCCCATCTTTGGCACTGCCAACTATGCCCAGAAGTTCCAGGGAAGGGTCACCATCACAGCTGATGAGAGCACCAGTACGGCCTATATGGAGCTGAGCAGCTTGCGGTCTGAGGATACAGCCGTGTACTACTGTGCAAGAGGAGGCTCAGTGAGTGGCACTCTTGTGGACTTCGACATTTGGGGTCAAGGCACCATGGTGACAGTCTCTTCCGCTTCGACCAAGGGACCTAG", "TEST1");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadKey();
                return 0;
            }
            Console.WriteLine("Seq1 Sequence: " + seq1.Sequence);



            string[] outputPrimer=new string[2];
            try
            {
                outputPrimer= seq1.GetGenestrandSeqPrimer(90, 120, 1000, 100);



                Console.WriteLine("F_Primer Seq: "+ outputPrimer[0]);
                Console.WriteLine("R_Primer Seq: "+ outputPrimer[1]);
                Primer PrimerTest = new Primer(outputPrimer[0], 50, 50);
                PrimerTest.CreateSeqFile("TEST_F1", @"C:\Users\Florian\Desktop");// PFAD ANPASSEN
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadKey();
                return 0;
            }





            Console.WriteLine("End of Test1");

            Console.WriteLine(seq1.ReadInSeqFile(@"C:\Users\Florian\Desktop\SEQ1.txt"));




            Console.WriteLine("End of Test");

            Console.ReadKey();
            return 0;
        }
    }
}
