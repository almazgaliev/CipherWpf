using System.Numerics;
using System.Text;

namespace Cipher.Caesar
{
    public class Caesar : ICipher
    {
        public readonly IAlphabet[] alphabets;
        private readonly IntOffset[] offsets;
        private BigInteger key;
        public BigInteger Key
        {
            get => key;
            set
            {
                key = value;
                for (int i = 0; i < alphabets.Length; ++i)
                {
                    offsets[i].SetOffset(key);
                }
            }
        }

        public Caesar()
        {
            alphabets = new IAlphabet[]
            {
                new Alphabet("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"),
            };
            offsets = new IntOffset[alphabets.Length];
            for (int i = 0; i < alphabets.Length; i++)
            {
                offsets[i] = new IntOffset(alphabets[i].Length);
            }

            Key = 0;
        }
        public Caesar(IAlphabet[] alphabets) : this()
        {
            this.alphabets = alphabets;
            offsets = new IntOffset[alphabets.Length];
            for (int i = 0; i < alphabets.Length; ++i)
                offsets[i] = new IntOffset(alphabets[i].Length);
        }

        public Caesar(IAlphabet alphabet) : this()
        {
            alphabets = new IAlphabet[] { alphabet };
            offsets = new IntOffset[alphabets.Length];
            for (int i = 0; i < alphabets.Length; ++i)
                offsets[i] = new IntOffset(alphabets[i].Length);
        }

        public string Encrypt(string message)
        {
            StringBuilder cryptogram = new(message.Length);
            for (int i = 0; i < message.Length; ++i)
            {
                cryptogram.Append(Encrypt(message[i]));
            }
            return cryptogram.ToString();
        }
        public string Decrypt(string encryptedMessage)
        {
            StringBuilder s = new(encryptedMessage.Length);
            for (int i = 0; i < encryptedMessage.Length; ++i)
                s.Append(Decrypt(encryptedMessage[i]));
            return s.ToString();
        }
        public char Encrypt(char character)
        {

            // TODO add exception throwing if alphabet doesnt contain character
            for (int i = 0; i < alphabets.Length; i++)
            {
                if (alphabets[i].Contains(character, out int indexOfCharacter))
                    return alphabets[i][indexOfCharacter + offsets[i].Value];
            }

            return character;
        }

        public char Decrypt(char character)
        {
            for (int i = 0; i < alphabets.Length; i++)
                if (alphabets[i].Contains(character, out int indexOfCharacter))
                    return alphabets[i][indexOfCharacter - offsets[i].Value];
            return character;
        }
    }
}
