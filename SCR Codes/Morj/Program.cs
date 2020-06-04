using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Morj
{
    class Program
    {
        static void Main(string[] args)
        {
            string Splash = "░░░░░░░▄▄████▄▄▄░░░░░░▄▄██████▄▄░░░░░░░▄▄████▄▄▄░░░░░░░▄▄████▄▄▄░░░░░░░▄▄████▄▄▄\r\n\r\nDeveloped by: \r\n         *professor la*  \r\n         *MR SAfA*  \r\n\r\n     +Contact Us : professor.la@protonmail.ch \r\n\r\n░░░░░░░▄▄████▄▄▄░░░░░░▄▄██████▄▄░░░░░░░▄▄████▄▄▄░░░░░░░▄▄████▄▄▄░░░░░░░▄▄████▄▄▄";
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (char c in Splash)
            {
                Random r = new Random();
                if (c != ' ')
                {
                    System.Threading.Thread.Sleep(r.Next(5,200));
                }
                Console.Write(c);
            }
            Console.WriteLine("\r\n\r\n\r\n");
            Console.ResetColor();


            bool istrue = false;

            Console.Write("Please Insert The Passwords Location: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string address = Console.ReadLine();
            Console.ResetColor();
            string[] Passwords = File.ReadAllLines(address);


            Console.WriteLine();

            Console.Write("Please Insert The AZX File Location: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            address = Console.ReadLine();
            Console.ResetColor();
            string Data = File.ReadAllText(address);


            string key = "";
            string decryptedData = "";
            for (int i = 0; i < Passwords.Length; i++)
            {
                try
                {
                    decryptedData = smethod_5(Data, Passwords[i]);
                    key = Passwords[i];
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Password " + i + " is right!");
                    Console.WriteLine("\r\n\r\n         Right Password: " + key);
                    istrue = true;
                    break;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Password " + i + " is incorrect!");
                    continue;
                }
                Console.ResetColor();
            }

            if (istrue)
            {
                try
                {
                    Console.Write("\r\n\r\n\r\nEnter A Full Address to Save Dicrypted Data With File Name: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    address = Console.ReadLine();
                    Console.ResetColor();

                    File.WriteAllText(address, decryptedData);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("File Saved Succeeded!");
                    Console.ResetColor();

                    Console.Write("\r\nAre You Want To Save The Right Password? (y / n)");
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    if (Console.ReadLine() == "y")
                    {
                        Console.ResetColor();
                        Console.Write("\r\nEnter Full Address And File Name: ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        File.WriteAllText(Console.ReadLine(), key);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Password Saved Succeeded!");
                        Console.ResetColor();
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\n\r\nThere Was A Eror!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\r\n\r\nWe Can't Find Your Password. You can try With Another Passwords!");
                Console.ResetColor();
            }

            Console.WriteLine("\r\n\r\n\r\nPress Any key to Exit ...");
            Console.ReadKey();
        }


        public static string smethod_5(string string_0, string string_1)
        {
            string result = null;
            checked
            {
                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(string_0)))
                {
                    RijndaelManaged rijndaelManaged = c9_smethod_6(string_1);
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] array = new byte[(int)(memoryStream.Length - 1L) + 1];
                        int count = cryptoStream.Read(array, 0, (int)memoryStream.Length);
                        result = Encoding.UTF8.GetString(array, 0, count);
                    }
                }
                return result;
            }
        }

        private static RijndaelManaged smethod_6(string string_0)
        {
            byte[] salt = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(string_0));
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(string_0, salt);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.KeySize = 256;
            checked
            {
                rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes((int)Math.Round((double)rijndaelManaged.BlockSize / 8.0));
                rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes((int)Math.Round((double)rijndaelManaged.KeySize / 8.0));
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                rijndaelManaged.Mode = CipherMode.CFB;
                return rijndaelManaged;
            }
        }

        private static RijndaelManaged c9_smethod_6(string string_0)
        {
            byte[] salt = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(string_0));
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(string_0, salt);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.KeySize = 256;
            checked
            {
                rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes((int)Math.Round((double)rijndaelManaged.BlockSize / 8.0));
                rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes((int)Math.Round((double)rijndaelManaged.KeySize / 8.0));
                rijndaelManaged.Padding = PaddingMode.PKCS7;
                rijndaelManaged.Mode = CipherMode.CFB;
                return rijndaelManaged;
            }
        }

        /*
        public static string smethod_7(int int_0)
        {
            string text = "";
            checked
            {
                for (int i = 1; i <= int_0; i++)
                {
                    int charCode = (int)Math.Round((double)Conversion.Int(unchecked(73f * VBMath.Rnd() + 48f)));
                    text += Strings.Chr(charCode).ToString();
                }
                return text;
            }
        }

        public static string smethod_8(int int_0)
        {
            string text = "";
            if (int_0 < 8)
            {
                int_0 = 8;
            }
            long num = (long)(checked((int)Math.Round((double)(unchecked((float)(checked(int_0 - 8)) * VBMath.Rnd() + 8f)))));
            checked
            {
                for (long num2 = 1L; num2 <= num; num2 += 1L)
                {
                    VBMath.Randomize();
                    int num3 = (int)Math.Round((double)Conversion.Int(unchecked(8f * VBMath.Rnd() + 48f)));
                    if (Operators.CompareString(Strings.Chr(num3).ToString(), "O", false) != 0 & Operators.CompareString(Strings.Chr(num3).ToString(), "0", false) != 0)
                    {
                        text += Strings.Chr(num3).ToString();
                    }
                    else
                    {
                        num3++;
                        text += Strings.Chr(num3).ToString();
                    }
                }
                return text;
            }
        }

        public static string smethod_9(string string_0)
        {
            string text = "";
            checked
            {
                int num = string_0.Length - 1;
                for (int i = 0; i <= num; i++)
                {
                    text += Conversions.ToString(string_0[string_0.Length - i - 1]);
                }
                return text;
            }
        }

        public static byte[] smethod_10(string string_0)
        {
            return Encoding.ASCII.GetBytes(string_0);
        }

        public static string smethod_11(byte[] byte_0)
        {
            return Convert.ToBase64String(byte_0);
        }

        public static void smethod_12(Array array_0, ref string string_0)
        {
            int num = Information.UBound(array_0, 1);
            string_0 = "";
            int num2 = num;
            checked
            {
                for (int i = 0; i <= num2; i++)
                {
                    int num3 = array_0[i] / 16 + 48;
                    if (num3 >= 58)
                    {
                        num3 = num3 - 58 + 65;
                    }
                    string_0 += Conversions.ToString(Strings.Chr(num3));
                    num3 = array_0[i] % 16 + 48;
                    if (num3 >= 58)
                    {
                        num3 = num3 - 58 + 65;
                    }
                    string_0 += Conversions.ToString(Strings.Chr(num3));
                }
            }
        }*/
    }
}
