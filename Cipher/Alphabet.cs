using System.Numerics;


namespace Cipher
{
    public class MemAlphabet : IAlphabet
    {
        private readonly char begin;
        private readonly char end;
        public char this[int index] => (char)(begin + (index % Length + Length) % Length);

        public char this[BigInteger index] => (char)(begin + ((int)(index % Length) + Length) % Length);

        private int length;
        public int Length { get => length; }

        public MemAlphabet()
        {
            begin = 'A';
            end = 'Z';
            length = end - begin + 1;
        }

        public MemAlphabet(char begin, char end)
        {
            if (begin >= end)
                throw new System.Exception("end is not less than begin");
            this.begin = begin;
            this.end = end;
            length = end - begin + 1;
        }

        public int IndexOf(char character)
        {
            if (end < character || begin > character)
                return -1;
            else
                return character - begin;
        }

        public bool Contains(char character) => character >= begin && character <= end;
        public bool Contains(char character, out int indexOfChar)
        {
            indexOfChar = IndexOf(character);
            return indexOfChar != -1;
        }
        public override string ToString()
        {
            int capacity = end - begin + 1;
            var s = new System.Text.StringBuilder(capacity);
            for (int i = 0; i < capacity; ++i)
                s.Append(begin + i);
            return s.ToString();
        }
    }
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

    public class Offset
    {
        public int Value { get; private set; }
        private readonly int alphabetLength;
        public Offset(int length)
        {
            Value = 0;
            alphabetLength = length;
        }

        public void SetOffset(BigInteger value) => Value = ((int)(value % alphabetLength) + alphabetLength) % alphabetLength;
    }
}
