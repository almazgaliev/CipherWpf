using System.Numerics;

namespace Cipher
{
    public interface IAlphabet
    {
        public char this[int index] { get; }
        public char this[BigInteger index] { get; }
        public int Length { get; }

        public bool Contains(char character);
        public bool Contains(char character, out int indexOfChar);
    }

}
