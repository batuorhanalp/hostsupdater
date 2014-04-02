using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using IpListUpdater.BLL;

namespace IpListUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Güncel IP listesi alınıyor...");
            HttpStatusCode statusCode; 
            var ips = ApiCaller.GetData("http://ipaddresses.batuorhanalp.com.tr/api/address", "", out statusCode);

            if (statusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("hosts dosyasına güncel IP adresleri ekleniyor...");
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System),
                    @"drivers\etc\hosts");

                string line;
                var newFileText = new StringBuilder();
                var file = new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith("#") &&
                        !(line.Contains("twitter") || line.Contains("t.co") || line.Contains("youtube")))
                    {
                        newFileText.AppendLine(line);

                    }
                }
                foreach (var ip in ips)
                {
                    Console.WriteLine(string.Format("{0} {1}", ip.Ip, ip.Url));
                    newFileText.AppendLine(string.Format("{0} {1}", ip.Ip, ip.Url));
                }
                try
                {
                    File.WriteAllText(path, newFileText.ToString());
                    File.Create(path).Close();
                }
                catch (Exception)
                {
                    Console.WriteLine("Uygulamayı yönetici olarak açarak tekrar deneyiniz.");
                }
                Console.WriteLine("İşlem tamamlandı.");                
            }
            else
            {
                Console.WriteLine("Güncel IP adresleri alınamadı. İnternet bağlantınızı kontrol ettikten sonra tekrar deneyiniz.");
            }
        }
    }
}
