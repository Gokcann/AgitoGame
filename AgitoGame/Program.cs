using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgitoGame
{
    class Program
    {
        Random rnd = new Random();
        string string_number = "";
        int count1, count2, index = 0;
        int forecastcounter = 1;
        List<int> listnumber = new List<int>();
        List<int> foolist = new List<int>();
        int reply;
        int step1, step2 = 0;
        int dg;
        Dictionary<int, string> forecastlist = new Dictionary<int, string>();
        bool existscontrol = true;
        bool digitscontrol = true;
        static void Main(string[] args)
        {
            
            Program p = new Program();
            p.StartGame();
            Console.ReadLine();
        }

        private void StartGame()
        {
            /*
             * *******************************************************************************************
             *** NOT:CreateNumbers ve RandomFunc metotları bir önceki soruların çözümünden alıntıdır...***
             * *******************************************************************************************
             */
            Console.WriteLine("Seviye seçiniz(4,5,6,7,):");
            int choose;

            while (!int.TryParse(Console.ReadLine(), out choose))
            {
                Console.Write("Hatali Giriş!!");
            }
            
            //seviye secimi kontrolu
            if ( (choose == 4) || (choose == 5) || (choose == 6) || (choose == 7) )
            {
                GameEngine(choose);
            }
            else
            {
                Console.WriteLine("Hatalı Giriş!!\n\n");
                StartGame();
            }
        }

        private void GameEngine(int ch)
        {
            //farkli rakamlardan olusan random sayiyi olusturma
            int[] foo = new int[ch];
            foo = CreateNumbers(ch);
            /*
             * 
             * NOT: Dosyadaki ornek oyun ciktisini denemek icin 74. satiri yoruma alip 80-84. satirlari acin
             * 
             */
            //foo[0] = 4;
            //foo[1] = 6;
            //foo[2] = 2;
            //foo[3] = 8;
            //string_number = "4628";


            //rakamlari listeye aliyorum tek tek karsilastirmak icin
            for (int i = 0; i < foo.Length; i++)
            {
                foolist.Add(foo[i]);
            }
            bool firststep = false;
            //let's go started
            Console.WriteLine("Rakamları farklı 4 Basamaklı bir sayı giriniz: ");

            while (!int.TryParse(Console.ReadLine(), out reply))
            {
                Console.Write("Hatali Giriş!!");
            }

            //ilk asama icin girilen sayinin kontrolu
            if (reply > 0 && (GetDigit(reply) == ch))
            {
                firststep = true;
            }else
            {
                Console.WriteLine("Hatali Giriş!!");
            }
            //kontroller tamamsa karar yapilarina gecis
            if(firststep)
            {
                //sayiyi bulana kadar
                while (Convert.ToInt32(string_number) != reply)
                {
                    //sayac
                    forecastcounter++;
                    listnumber.Clear();
                    //sonradan yapilan girisler icin yukaridaki kontrollerin aynisi
                    if (reply > 0 )
                    {
                        if (GetDigit(reply) == ch && GetExists(reply) && !string_number.Substring(0,1).Equals("0") && GetDifferentDigits())
                        {
                            //step1 ve step2 basamaklari temsil ediyor
                            step1 = 0;
                            step2 = 0;

                            foreach (int e in foo)
                            {
                                foreach (int b in listnumber)
                                {
                                    if (e == b && (step1 == step2) )
                                    {
                                        //count1 +adet i temsil ediyor
                                        count1++;
                                    }
                                    else if(e == b)
                                    {
                                        //count2 -adet i temsil ediyor
                                        count2++;
                                    }
                                    step2++;
                                }
                                step2 = 0;
                                step1++;

                            }
                            //kontrollerden sonra tahmin listesine aliyorum
                            if ( (count1 > 0) || (count2 > 0))
                            {
                                string s = "";
                                if(count1 > 0)
                                {
                                    s += "+" + count1.ToString();
                                }
                                if(count2 > 0)
                                {
                                    s += " -" + count2.ToString();
                                }
                                forecastlist.Add(reply, s);
                            }
                            else
                            {
                                forecastlist.Add(reply, "0");
                            }
                            //her tahmin icin +adet ve -adet sifirlaniyor
                            count1 = 0;
                            count2 = 0;

                            //tahmin listesi ekrana yaziliyor
                            foreach (KeyValuePair<int, string> kvp in forecastlist.ToList())
                            {
                                Console.WriteLine(kvp.Key + " Tahmini için " + kvp.Value);
                            }
                            Console.Write("------------------\n");
                            Console.Write("Rakamları farklı 4 Basamaklı bir sayı giriniz: ");
                            //bir sonraki tahmini aliyorum
                            while (!int.TryParse(Console.ReadLine(), out reply))
                            {
                                Console.Write("Hatali Giriş!!");
                            }
                            


                        }
                        else
                        {
                            digitscontrol = true;
                            Console.Write("Rakamları farklı 4 Basamaklı bir sayı giriniz: ");
                            while (!int.TryParse(Console.ReadLine(), out reply))
                            {
                                Console.Write("Hatali Giriş!!");
                            }
                        }
                    }
                    
                }
                Console.WriteLine("Tebrikler " + forecastcounter + " kerede bildiniz...");
            }
            
        }

        private bool GetDifferentDigits()
        {
            for(int i = 0; i < listnumber.Count; i++)
            {
                for(int j=i+1; j < listnumber.Count; j++)
                {
                    if (listnumber[i] == listnumber[j])
                    {
                        digitscontrol = false;
                        break;
                    }
                }
            }
            return digitscontrol;
        }

        private bool GetExists(int reply)
        {
            foreach (KeyValuePair<int, string> kvp in forecastlist.ToList())
            {
                if (kvp.Key == reply)
                {
                    existscontrol = false;
                    break;
                }
                else
                {
                    existscontrol = true;
                }
             }
            return existscontrol;
        }

        private int GetDigit(int input)
        {
            int digit = 0;
            int cloneinput = input;
            int dg;
            while (input >= 1)
            {
                input /= 10;
                digit++;
            }
            while (cloneinput > 0)
            {
                cloneinput = Math.DivRem(cloneinput, 10, out dg);
                listnumber.Add(dg);
            }
            listnumber.Reverse();
            return digit;
        }
        private int[] CreateNumbers(int input)
        {
            int[] boo = new int[input];
            
            List<int> output = new List<int>();
            int min = 0;
            int max = 9;
            int counter = 0;
            int temp;
            bool control = true;
            //to add the first number
            output.Add(RandomFunc(min, max));
            //then automatically checks and adds in do-while blocks
            do
            {
                //the following two if decision is narrow the range of RandomFunc
                if (output.Min() == min)
                {
                    min++;
                }
                if (output.Max() == max)
                {
                    max--;
                }
                temp = RandomFunc(min, max);
                counter++;
                foreach (int i in output.ToList())
                {
                    if (i == temp)
                    {
                        control = false;
                        break;
                    }
                    else
                    {
                        control = true;
                    }
                }
                if (control)
                {
                    output.Add(temp);
                }
            } while (output.Count < input);


            for (int j=0; j<output.Count; j++)
            {
                boo[j] = output[j];
                string_number += Convert.ToString(output[j]);
            }
            return boo;

        }

        public int RandomFunc(int min, int max)
        {
            int post_random = rnd.Next(min, max + 1);
            return post_random;
            
        }

        
    }

    }
