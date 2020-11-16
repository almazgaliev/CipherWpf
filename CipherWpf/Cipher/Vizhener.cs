using System;
using System.Text;

namespace Cipher
{
    class Vizhener : ICipher
    {
        private string key;
        public string Key 
        {
            get => key;
            set
            {
                key = value;
                offsets = new int[key.Length];
                for (int i = 0; i < offsets.Length; i++)
                    offsets[i] = 0;
                for (int i = 0; i < key.Length; i++)
                    for (int j = 0; j < alphabets.Length; j++)
                        if (alphabets[j].Contains(key[i], out int index))
                        {
                            offsets[i] = index;
                            break;
                        }
            }
        }


        public readonly IAlphabet[] alphabets;
        private int[] offsets;
        //public Vizhener()
        //{
        //    Alphabets = new IAlphabet[]
        //           {
        //                new Alphabet(),
        //                new Alphabet("abcdefghijklmnopqrstuvwxyz"),
        //                new Alphabet("АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"),
        //                new Alphabet("абвгдеёжзийклмнопрстуфхцчшщъыьэюя"),
        //                new Alphabet("0123456789")
        //           };
        //}

        public Vizhener() 
        {
            alphabets = new IAlphabet[]
                {
                    new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"),
                };
            Key = "A";
        }

        //public Vizhener(IAlphabet[] alphabets)
        //{
        //    this.alphabets = alphabets;
        //    Key = alphabets[0][0].ToString();
        //}

        public Vizhener(IAlphabet alphabet)
        {
            alphabets = new IAlphabet[] { alphabet };
            Key = alphabets[0][0].ToString();
        }


        public string Encrypt(string message)
        {
            StringBuilder s = new StringBuilder(message.Length);
            for (int i = 0; i < message.Length; i++)
                s.Append(Encrypt(message[i], i, message.Length));
            return s.ToString();
        }

        public string Decrypt(string encryptedMessage)
        {
            StringBuilder s = new StringBuilder(encryptedMessage.Length);
            for (int i = 0; i < encryptedMessage.Length; i++)
                s.Append(Decrypt(encryptedMessage[i], i, encryptedMessage.Length));
            return s.ToString();

        }
        public char Encrypt(char character, int indexOfCharacter,int messageLength)
        {
            for (int i = 0; i < alphabets.Length; i++)
                if (alphabets[i].Contains(character, out int index)) 
                {
                    int minLength = Math.Min(messageLength, Key.Length);
                    int offset = offsets[indexOfCharacter % minLength];
                    return alphabets[i][index + offset];
                }
            return character;
        }
        public char Decrypt(char character, int indexOfCharacter, int messageLength)
        {
            for (int i = 0; i < alphabets.Length; i++)
                if (alphabets[i].Contains(character, out int index))
                {
                    int minLength = Math.Min(messageLength, Key.Length);
                    int offset = -offsets[indexOfCharacter % minLength];
                    return alphabets[i][index + offset];
                }
            return character;
        }
    }
}
