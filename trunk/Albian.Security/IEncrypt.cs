namespace Albian.Security
{
    public interface IEncrypt
    {
        //void GetKey();
        string EncryptString(string value);
        string EncryptString(string value, int deepth);
        string DecryptString(string value);
        string DecryptString(string value, int deepth);
    }
}