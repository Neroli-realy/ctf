using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Diagnostics;

namespace FibSocket_solve
{
    class Program
    {
        //get 20 fibonaccai nums if i spilled it write :)
        public static void fibonacci(int[] arr)
        {
            int index = 0;
            int n1 = 1, n2 = 2, n3, i, number; //start from 3 like the file
            number = 20;
            for (i = 2; i < number; ++i) //loop starts from 2 because 0 and 1 are already printed    
            {
                n3 = n1 + n2;
                arr[index] = n3;
                index++;
                n1 = n2;
                n2 = n3;
            }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Lets Solve!");

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("[+]Connecting to Server...");

            //reopen the challenge each time i lost connection
            while (true)
            {
               
                    Process.Start(@"C:\Users\Neroli\Downloads\FibSocket(1).exe");
                    //create socket tcp
                    Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    
                    //to store nums
                    int[] fibos = new int[200];
                    //get nums
                    fibonacci(fibos);


                    while (true)
                    {

                        try { s.Connect("127.0.0.1", 7777); }
                        catch { }
                        if (s.Connected)
                        {
                            Console.WriteLine("[+] Connected");
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        }
                    }

                    int num = 0;
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {

                            byte[] inStream = new byte[4];
                            Int32 bytes = s.Receive(inStream, inStream.Length, 0);
                            string st = "";
                            st = Encoding.UTF8.GetString(inStream, 0, bytes);
                            Console.Write(st);
                            if (i == 13)
                            {
                                var test = BitConverter.ToInt16(inStream, 0);
                                Console.WriteLine(test.ToString());
                                int indexOfNum = 0;

                                //just for debugging :)
                                foreach (char c in st)
                                    Console.WriteLine("{}==> " + ((int)c).ToString());
                                for (int t = 0; t < fibos.Length; t++)
                                {
                                    if (fibos[t] == (int)st[0])
                                    {
                                        indexOfNum = t;
                                        break;
                                    }
                                }

                                //first loop
                                int response = fibos[indexOfNum + 1];
                                var rawBytes = BitConverter.GetBytes(response);
                                //   uint send =  BitConverter.ToUInt32(rawBytes , 0);
                                // Console.WriteLine("{}==> " + send.ToString());
                                s.Send(rawBytes, rawBytes.Length, 0);

                                //second loop
                                while(true)
                                {
                                    byte[] inStream2 = new byte[1024];
                                    Int32 bytes2 = s.Receive(inStream2, inStream2.Length, 0);
                                    string st2 = "";
                                    st2 = Encoding.UTF8.GetString(inStream2, 0, bytes2);
                                    Console.WriteLine("------> " + st2);
                                    if (st2.Contains("Well done"))
                                    {
                                        var win = BitConverter.GetBytes(233);
                                        s.Send(win, win.Length, 0);
                                        for (int l = 0; l < 3; l++)
                                        {
                                            byte[] inStream3 = new byte[1024];
                                            Int32 bytes3 = s.Receive(inStream3, inStream3.Length, 0);
                                            string st3 = "";
                                            st3 = Encoding.UTF8.GetString(inStream3, 0, bytes3);
                                            Console.Write(st3);
                                        }
                                        Console.ReadLine();
                                    }
                                }
                                
                                

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;

                        }
                        // Console.WriteLine(i.ToString());
                    }

                   
                Console.WriteLine("{0} Restarting");
                
             }
                
            
           
        }
    }
}
