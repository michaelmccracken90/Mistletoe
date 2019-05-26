namespace Mistletoe.Web.Helpers
{
    using System.IO;

    public class FileHelper
    {
        public static bool EncryptFile(string filePath)
        {
            bool result = false;

            if(File.Exists(filePath))
            {
                File.Encrypt(filePath);
            }
            return result;
        }

        public static bool DecryptFile(string filePath)
        {
            bool result = false;

            if (File.Exists(filePath))
            {
                File.Decrypt(filePath);
            }
            return result;
        }

        public static bool DeleteFile(string filePath)
        {
            bool result = false;

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                result = true;
            }

            return result;
        }
    }
}