using Cipher;
using CommandLine;
using System;

namespace CipherCLI
{
    //TODO dont run program if -h flag passed
    public class Options
    {
        private int _selectedLanguage;
        public int SelectedLanguage => _selectedLanguage;

        [Option('l', "language", Default = "en-US", HelpText = "language. \n'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'(en-US),\n'АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789'(ru-RU) are supported")]
        private string Language
        {
            set
            {
                switch (value)
                {
                    case "en-US":
                        _selectedLanguage = 0;
                        break;
                    case "ru-RU":
                        _selectedLanguage = 1;
                        break;
                    default:
                        Console.WriteLine($"Language '{value}' is  incorrect, selected default(en-US)");
                        _selectedLanguage = 0;
                        break;
                }
            }
        }

        [Option('c', "Cipher", Default = "Caesar", HelpText = "Caesar or Vizhener")]
        public string CipherAlgorithm { get; set; }

        [Option('k', "Key", Default = "123", HelpText = "key for cipher alorithm ( for caesar key is type of BigInteger, for vizhener key is type of string)")]
        public string Key { get; set; }
    }
}