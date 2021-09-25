using System;
using System.Numerics;
using Cipher;
using Cipher.Caesar;
using Cipher.Vizhener;
using CommandLine;

namespace CipherCLI
{
    public class Program
    {
        private static readonly IAlphabet[] alphabets = new[]{
                new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"),
                new Alphabet("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789"),
            };
        private static ICipher cipher;


        static void Main(string[] args)
        {
            int selectedLanguage = 1;
            string cipherAlgoritm = "";
            string key = "key";
            Parser.Default.ParseArguments<Options>(args).WithParsed(
                (o) =>
                {
                    selectedLanguage = o.SelectedLanguage;
                    cipherAlgoritm = o.CipherAlgorithm;
                    key = o.Key.ToUpper();
                }
            );
            switch (cipherAlgoritm)
            {
                case "Caesar":
                    if (BigInteger.TryParse(key, out BigInteger intKey))
                        cipher = new Caesar(alphabets[selectedLanguage]) { Key = intKey };
                    else
                    {
                        Console.WriteLine($"'{key}' is not bigInt");
                        return;
                    }
                    break;
                case "Vizhener":
                    cipher = new Vizhener(alphabets[selectedLanguage]) { Key = key };
                    break;
                default:
                    Console.WriteLine("Cipher is incorrect, selected Caesar");
                    selectedLanguage = 0;
                    break;
            }

            RunAppInteraction();

        }

        // TODO add decode shortcut event handler 
        private static void RunAppInteraction()
        {
            while (true)
            {
                Console.Write("# ");
                string input = Console.ReadLine().Trim().ToUpper();
                if (input != "")
                {
                    string output = cipher.Encrypt(input);
                    Console.WriteLine(output);
                }
            }
        }
    }
}
