using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Common
{
    internal static class ObslugaJSON<T>
    {
        public static ObservableCollection<T> PobierzDaneZJSON(string nazwaPliku)
        {
            if (File.Exists(nazwaPliku))
            {
                string dane = File.ReadAllText(nazwaPliku);

                ObservableCollection<T> listaDanych = JsonConvert.DeserializeObject<ObservableCollection<T>>(dane);
                return listaDanych;
            }
            return new ObservableCollection<T>();
        }
        public static void ZapiszDoJSON(IEnumerable<T> listaDanych, string nazwaPliku)
        {
            if(!Directory.Exists(Staticks.FolderDanych))
                Directory.CreateDirectory(Staticks.FolderDanych);

            string dane = JsonConvert.SerializeObject(listaDanych);
            File.WriteAllText(nazwaPliku, dane);
        }
    }
}
