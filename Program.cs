using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Saldainis_Laboras2
{

    class Saldainis
    {
        string pavadinimas;
        string tipas;
        double kgKaina;



        public Saldainis(string pavadinimas, string tipas, double kgKaina)
        {
            this.pavadinimas = pavadinimas;
            this.tipas = tipas;
            this.kgKaina = kgKaina;



        }
        public string imtiPavadinima() { return pavadinimas; }
        public string imtiTipa() { return tipas; }
        public double imtikgKaina() { return kgKaina; }



    }
    internal class Program
    {


        static void Main(string[] args)
        {

            const int Cn = 100;
            const int Cf = 100;
            string fv1 = "TextFile1.txt";
            string fv2 = "TextFile2.txt";
            string fv3 = "Rezultatai.txt";


            double n1, n2;
            double N1, N2;
            double k;
            double kiekis1, kiekis2;
            string pav1, vard1;
            string pav2, vard2;
            int vard11, brangindeks;

            Saldainis[] S1 = new Saldainis[Cn];
            Saldainis[] S2 = new Saldainis[Cf];

            skaityti(fv1, S1, out n1, out kiekis1, out vard1, out N1, out k);
            skaityti(fv2, S2, out n2, out kiekis2, out vard2, out N2, out k);
            int a1 = Indeksas(S1, kiekis1, n1);
            int a2 = Indeksas(S2, kiekis2, n2);
            string vardas = kurisbrangiau(S1, S2, kiekis1, kiekis2, vard1, vard2, N1, N2);
            double[] Formuotas = formuoti(S1, S2, kiekis1, kiekis2, N1, N2);
            spausdinti(fv3, S1, kiekis1, vard1, n1, a1, vardas, N1, N2, Formuotas);
            spausdinti(fv3, S2,  kiekis2,  vard2, n2, a2, vardas, N1, N2, Formuotas);
            

            Console.WriteLine(a1);
            Console.WriteLine(S1[2].imtikgKaina());

            formuoti(S1, S2, kiekis1, kiekis2, N1, N2);
           

        

        }



        static void skaityti(string FD, Saldainis[] S1, out double n, out double kiekis, out string vardas, out double N, out double k)

        {
            string pavadinimas;
            string tipas;
            double kgKaina;




            using (StreamReader reader = new StreamReader(FD))
            {
                string[] parts;
                string line;
                line = reader.ReadLine();
                vardas = line;
                line = reader.ReadLine();
                kiekis = double.Parse(line);

                for (int i = 0; i < kiekis; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    pavadinimas = parts[0];
                    tipas = parts[1];
                    kgKaina = double.Parse(parts[2]);


                    S1[i] = new Saldainis(pavadinimas, tipas, kgKaina);

                }
                line = reader.ReadLine();
                n = double.Parse(line);
                N = double.Parse(line);
                k = double.Parse(line);

                


            }

        }




        static int Indeksas(Saldainis[] S1, double kiekis, double n)
        {

            int brangindeks = 0;

            for (int i = 0; i < kiekis - 1; i++)
            {
                if (S1[i].imtikgKaina() * n > S1[i + 1].imtikgKaina() * n)
                {
                    brangindeks = i;

                }
                else brangindeks = i + 1;

            }
            return brangindeks;
        }
        static void spausdinti(string FV, Saldainis[] S1, double kiekis, string vard1, double n, int a, string vardas, double N1, double N2, double[] Formuotas, double k)
        {
            using (var fr = File.AppendText(FV))
            {
                const string virsus =
                  "|-----------------|--------------|-----------------|\r\n"
                  + "| Pavadinimas   | Tipas        | Kaina kilogramui| \r\n"
                  + "|---------------|--------------|-----------------|";
                fr.WriteLine(vard1);
                fr.WriteLine(virsus);

                for (int i = 0; i < kiekis; i++)
                {
                    fr.WriteLine("| {0,-12} | {1,8} | {2,5} |",
                      S1[i].imtiPavadinima(), S1[i].imtiTipa(), S1[i].imtikgKaina());
                    fr.WriteLine("-----------------------------------------------------");

                }

                fr.WriteLine("Kiekvieno saldainio pavadinimo yra po" + n + " kilogramu");


                for (int i = 0; i < kiekis; i++)
                {
                    fr.WriteLine(S1[i].imtiPavadinima() + " | " + "  Saldainiai kainuoja  " + S1[i].imtikgKaina() * n);


                }
                for (int i = 0; i < kiekis; i++)
                {
                    if (S1[a].imtikgKaina() * n == S1[i].imtikgKaina() * n)
                    {


                        fr.WriteLine("Brangiausiu saldainiu tipas: " + S1[i].imtiTipa());
                    }
                }
                fr.WriteLine("Studento vardas, kurio rinkinys kainuoja brangiau : " + vardas);
Console.Write("Sudarytas Masyvas :");

                for (int i = 0; i < Formuotas.Length; i++)
                {

                    Console.Write(Formuotas[i] + " ");
                }
                    Console.WriteLine(" : Atrinkti skaiciai didesni uz " + k + " ");
                    const string virsus =
                      "|-----------------|------------|-----------------|\r\n"
                      + "| Pavadinimas   |  Tipas     | Kaina kilogramui    | \r\n"
                      + "|---------------|------------|-----------------|";
                    Console.WriteLine(virsus);
                    for (int i = 0; i < Formuotas.Length; i++)
                    {

                        if (Formuotas[i] >k)
                        {
                            Console.WriteLine("| {0,-12} | {1,8} | {2,11} | ",
                           S1[i].imtiPavadinima(), S1[i].imtiTipa(), S1[i].imtikgKaina());
                            Console.WriteLine("----------------------------------------------------------");

                        }
                    }
                }
                
               
        }
        }
        static string kurisbrangiau(Saldainis[] S1, Saldainis[] S2, double kiekis1, double kiekis2, string vard1, string vard2, double N1, double N2)
        {

            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < kiekis1; i++)
            {
                sum1 += S1[i].imtikgKaina() * N1;

            }

            for (int i = 0; i < kiekis2; i++)
            {
                sum2 += S2[i].imtikgKaina() * N2;

            }

            if (sum1 > sum2)
            {
                return vard1;
            }

            else return vard2;
        }

        static double [] formuoti(Saldainis[] S1, Saldainis[] S2, double kiekis1, double kiekis2, double N1, double N2  )
        {
            double n = kiekis1 +kiekis2;
 int a = 0;
             double [] C = new double[6];
           
            for (int i = 0; i < kiekis1; i++)
            {
                C[a] = S1[i].imtikgKaina() * N1;
                a++;
            }
            for (int i = 0; i < kiekis2; i++)
            {
                C[a] = S2[i].imtikgKaina() * N1;
                a++;
            }

            return C;
        }
    }
}





