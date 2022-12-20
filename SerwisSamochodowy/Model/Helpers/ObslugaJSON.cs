using Newtonsoft.Json;
using SerwisSamochodowy.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisSamochodowy.Model.Helpers
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
            return null;
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
