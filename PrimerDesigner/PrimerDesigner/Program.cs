using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimerDesigner
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Primer Primer1 = new Primer("acccctt", 50, 50);
            Console.WriteLine("Die Tm(Nearest Neighbour)  ist: " + Primer1.GetNearestNeighbourTemp());
            Console.WriteLine(Primer1.Sequence);
            Console.WriteLine(Primer1.GetReverseComplement());

            Console.WriteLine(" ");




            GeneSequence seq1 = null;
            try
            {
                seq1 = new GeneSequence("AAGACCgttcccggaaaGC");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.ReadLine();
                Environment.Exit(0);

            }
            Console.WriteLine(seq1.Sequence);
            //Console.WriteLine(Seq1.GetGenestrandSeqPrimer()[0]);//Output ist ein array. [0] wird ausgegeben =Primer F
            // Console.WriteLine( Seq1.GetGenestrandSeqPrimer()[1]);//Output ist ein array. [1] wird ausgegeben =Primer R
            //Console.WriteLine(Seq1.CheckForDnaChar());
            Console.WriteLine("ende");
            Console.ReadKey();



        }
    }
}
