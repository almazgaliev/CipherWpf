namespace Cipher 
{
    interface ICipher
    {
        string Encrypt(string message);
        string Decrypt(string message);
    }
}
