using System.Numerics;


namespace Cipher
{
    /// <summary>
    /// Alphabet class that supports modded indexing
    /// </summary>
    public class Alphabet : IAlphabet
    {
        private readonly string alphabet;


        public char this[int index] => alphabet[(index % alphabet.Length + alphabet.Length) % alphabet.Length];

        public char this[BigInteger index] => alphabet[((int)(index % alphabet.Length) + alphabet.Length) % alphabet.Length];


        public Alphabet()
        {
            alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
        public Alphabet(string symbols)
        {
            alphabet = symbols;
        }
        public int Length => alphabet.Length;
        public int IndexOf(char character) => alphabet.IndexOf(character);

        public bool Contains(char character) => alphabet.Contains(character);
        public bool Contains(char character, out int indexOfChar)
        {
            indexOfChar = alphabet.IndexOf(character);
            return indexOfChar != -1;
        }
        public override string ToString()
        {
            return alphabet;
        }

    }

    internal class IntOffset
    {
        public int Value { get; private set; }
        private readonly int alphabetLength;
        public IntOffset(int length)
        {
            Value = 0;
            alphabetLength = length;
        }

        public void SetOffset(BigInteger value) => Value = ((int)(value % alphabetLength) + alphabetLength) % alphabetLength;
    }
}
