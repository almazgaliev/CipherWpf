using System;
using Cipher;
using CommandLine;

namespace CipherCLI
{
    public class Program
    {
        private static readonly IAlphabet[] alphabets;
        private static ICipher cipher;
        private static int selectedCipher = 0;
        private static int selectedLanguage = 1;

        private class Options
        {
            public int selectedLanguage;
            [Option('l', "language", Default = "en-US", HelpText = "language. \n'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'(en-US),\n'АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789'(ru-RU) are supported")]
            private string Language
            {
                set
                {
                    switch (value)
                    {
                        case "en-US":
                            selectedLanguage = 0;
                            break;
                        case "ru-RU":
                            selectedLanguage = 1;
                            break;
                        default:
                            Console.WriteLine("Language is incorrect, selected en-US");
                            selectedLanguage = 0;
                            break;
                    }
                }
            }
            public int selectedCipher;
            [Option('c', "Cipher", Default = "Caesar", HelpText = "Caesar or Vizhener")]
            private string Cipher
            {
                set
                {
                    switch (value)
                    {
                        case "Caesar":
                            cipher = new Caesar();
                            break;
                        case "Vizhener":
                            selectedLanguage = 1;
                            break;
                        default:
                            Console.WriteLine("Cipher is incorrect, selected e");
                            selectedLanguage = 0;
                            break;
                    }
                }
            }
        }
        static Program()
        {
            alphabets = new IAlphabet[]
            {
                new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"),
                new Alphabet("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789"),
            };
        }
        static void Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args).WithParsed((o) => { selectedLanguage = o.selectedLanguage; cipher =  });
            cipher = new Caesar(alphabets[0])
            {
                Key = 123
            };
            Console.WriteLine("Hello World!");
        }
    }
}
