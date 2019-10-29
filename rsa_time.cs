using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ConsoleApplication4
{
    class Program
    {
       static  void doComputation(int myKeySize)
        {
            RSACryptoServiceProvider myrsa = new RSACryptoServiceProvider();
            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch();
            int size;
            int count = 100;
            swatch.Start();
            for (int i = 0; i < count; ++i)
            {
                myrsa = new RSACryptoServiceProvider(myKeySize);
                size = myrsa.KeySize;
            }
            swatch.Stop();
            Console.WriteLine("Generation time at " + myKeySize + " bit ... " + ((double)swatch.ElapsedMilliseconds / count).ToString() + " ms");
            byte[] plain = new byte[20];
            byte[] ciphertext = myrsa.Encrypt(plain, true);

            swatch.Reset();
            swatch.Start();
            for (int i = 0; i < count; i++)
            {
                ciphertext = myrsa.Encrypt(plain, true);
            }
            Console.WriteLine("Encryption time at " + myKeySize + " bit ... " + ((double)swatch.ElapsedMilliseconds / count).ToString() + " ms");
            swatch.Reset();
            swatch.Start();
            for (int i = 0; i < count; i++)
            {
                plain = myrsa.Decrypt(ciphertext, true);
            }
            swatch.Stop();
            Console.WriteLine("Decryption time at " + myKeySize + " bit ... " + ((double)swatch.ElapsedMilliseconds / count).ToString() + " ms");

            swatch.Reset();
            SHA256Managed myHash = new SHA256Managed();
            string some_text = "this is an important message";
            //sign the message
            byte[] signature = myrsa.SignData(System.Text.Encoding.ASCII.GetBytes(some_text), myHash);
            swatch.Start();
            for (int i = 0; i < count; i++)
            {
                signature = myrsa.SignData(System.Text.Encoding.ASCII.GetBytes(some_text), myHash);
            }
            swatch.Stop();
            Console.WriteLine("Signing time at " + myKeySize + " bit ... " + ((double)swatch.ElapsedMilliseconds / count).ToString() + " ms");

            swatch.Reset();
            bool verified;
            swatch.Start();
            for (int i = 0; i < count; i++)
            {
                verified = myrsa.VerifyData(System.Text.Encoding.ASCII.GetBytes(some_text), myHash, signature);
            }
            swatch.Stop();
            Console.WriteLine("Verification time at " + myKeySize + " bit ... " + ((double)swatch.ElapsedMilliseconds / count).ToString() + " ms");
        }

        static void Main(string[] args)
        {
            int op;
            int myKeySize = 0;
            
            do
            {
                Console.WriteLine("Menu");
                Console.WriteLine("Op 1: 1024");
                Console.WriteLine("Op 2: 2048");
                Console.WriteLine("Op 3: 3072");
                Console.WriteLine("Op 4: 4096");
                Console.WriteLine("Op 5: Exit");

                op = Convert.ToInt16(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        myKeySize = 1024;
                        break;
                    case 2:
                        myKeySize = 2048;
                        break;
                    case 3:
                        myKeySize = 3072;
                        break;
                    case 4:
                        myKeySize = 4096;
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Your option was: {0}", op);
                doComputation(myKeySize);
            } while (op != 5);

            Console.ReadLine();
            
        }
    }
}
