namespace Cipher 
{
    /// <summary>
    /// Interface for basic Encryption algorithm class
    /// </summary>
    public interface ICipher
    {
        string Encrypt(string message);
        string Decrypt(string message);
    }
}
