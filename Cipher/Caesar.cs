using System.Numerics;
using System.Text;

namespace Cipher
{
    public class Caesar : ICipher
    {
        public readonly IAlphabet[] alphabets;
        private readonly Offset[] offsets;
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
            offsets = new Offset[alphabets.Length];
            for (int i = 0; i < alphabets.Length; i++)
            {
                offsets[i] = new Offset(alphabets[i].Length);
            }

            Key = 0;
        }
        public Caesar(IAlphabet[] alphabets) : this()
        {
            this.alphabets = alphabets;
            offsets = new Offset[alphabets.Length];
            for (int i = 0; i < alphabets.Length; ++i)
                offsets[i] = new Offset(alphabets[i].Length);
        }

        public Caesar(IAlphabet alphabet) : this()
        {
            alphabets = new IAlphabet[] { alphabet };
            offsets = new Offset[alphabets.Length];
            for (int i = 0; i < alphabets.Length; ++i)
                offsets[i] = new Offset(alphabets[i].Length);
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
            #region SimpleCase
            //if (char.IsLetter(character))
            //{
            //    if (char.IsLower(character))
            //        return (char)('a' + (character - 'a' + key) % Lenght);
            //    else
            //        return (char)('A' + (character - 'A' + key) % Lenght);
            //}
            //else if (char.IsDigit(character))
            //{
            //    return (char)('0' + (character - '0' + key) % Lenght % 10);
            //}
            //else
            //    return character;
            #endregion

            #region MediumCase
            // every alphabet is string here
            //if (AlphabetUpper.Contains(character))
            //    return AlphabetUpper[(AlphabetUpper.IndexOf(character) + characterOffset) % Lenght];

            //else if (AlphabetLower.Contains(character))
            //    return AlphabetLower[(AlphabetLower.IndexOf(character) + characterOffset) % Lenght];

            //else if (char.IsDigit(character))
            //    return (char)('0' + (character - '0' + digitOffset) % 10);

            //else if (AdditionalAlphabetsContains(character, out int indexOfAlphabet, out int indexOfChar))
            //    return AdditionalAlphabets[indexOfAlphabet][(indexOfChar + characterOffset) % AdditionalAlphabets[indexOfAlphabet].Length];
            //else
            //    return character;
            #endregion 

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
