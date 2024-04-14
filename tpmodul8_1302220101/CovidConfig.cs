using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace tpmodul8_1302220101
{
    public class Config
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_demam { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        public Config() { }

        public Config(string satuan_suhu, int batas_hari_demam, string pesan_ditolak, string pesan_diterima)
        {
            this.satuan_suhu = satuan_suhu;
            this.batas_hari_demam = batas_hari_demam;
            this.pesan_ditolak = pesan_ditolak;
            this.pesan_diterima = pesan_diterima;
        }
    }

    internal class CovidConfig
    {
        public Config config { get; set; }
        public string path = "../../../covid_config.json";
        //public static string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        //public string configFileName = "covid_config.json";

        public CovidConfig()
        {
            try
            {
                ReadConfig();
            }
            catch
            {
                setDefault();

            }
        }
        private void ReadConfig()
        {
            config = JsonSerializer.Deserialize<Config>(path);
        }

        public void WriteConfig()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            String jsonString = JsonSerializer.Serialize(config, options);
            string fullPath = path;
            File.WriteAllText(path, jsonString);
        }

        public void setDefault()
        {
            config = new Config("celcius", 14, "Anda tidak diperbolehkan masuk ke dalam gedung ini", "Anda dipersilahkan untuk masuk ke dalam gedung ini");
        }

        public void ubahSatuan()
        {
            if (config.satuan_suhu == "celcius")
            {
                config.satuan_suhu = "farenheit";
            } else
            {
                config.satuan_suhu = "celcius";
            }
        }

        public bool IsInputValid(double suhu, int hariDemam)
        {
            if (config.satuan_suhu == "celcius")
            {
                if (suhu < 36.5 || suhu > 37.5)
                {
                    return false;
                }
            }
            else if (config.satuan_suhu == "farenheit")
            {
                if (suhu < 97.7 || suhu > 99.5)
                {
                    return false;
                }
            }

            if (hariDemam >= config.batas_hari_demam)
            {
                return false;
            }

            return true;
        }
    }
}
