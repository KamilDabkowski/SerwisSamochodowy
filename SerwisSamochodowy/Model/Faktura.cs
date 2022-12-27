using SerwisSamochodowy.Common;
using SerwisSamochodowy.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace SerwisSamochodowy.Model
{
    internal class Faktura
    {
        #region properties

        public int IdFaktura { get; private set; }
        public int IdZlecenia { get; private set; }
        public DateTime DataWystawienia { get; private set; }
        public int Numer { get; private set; }

        public Dictionary<string, float> Koszty { get; private set; }
        public float Suma { get; private set; }

        #endregion

        #region ctor

        public Faktura(int idFaktura, int idZlecenia, DateTime dataWystawienia, int numer, Dictionary<string, float> koszty, float suma)
        {
            this.IdFaktura = idFaktura;
            this.IdZlecenia = idZlecenia;
            this.DataWystawienia = dataWystawienia;
            this.Numer = numer;
            this.Koszty = koszty;
            this.Suma = suma;
        }

        #endregion

        #region methods

        public static Faktura WczytajFakture(int idZlecenieNaprawy)
        {
            var result = BazaDanych.Faktury.FirstOrDefault(f => f.IdZlecenia == idZlecenieNaprawy);
            return result;
        }

        public void OtworzFakture()
        {
            string result = string.Empty;
            result += "Data wystawienia: \r\n" + DataWystawienia + "\r\n";
            result += "Numer: \r\n" + Numer + "\r\n";
            result += "Data wystawienia: \r\n" + DataWystawienia + "\r\n";

            int licznik = 1;
            foreach(var koszt in Koszty)
            {
                result += licznik + " " + koszt.Key + ": " + koszt.Value + "\r\n";
                licznik++;
            }

            result += "Suma: \r\n" + Suma + "\r\n";

            if (!Directory.Exists(Staticks.FolderFaktur))
                Directory.CreateDirectory(Staticks.FolderFaktur);

            var fileName = IdFaktura + ".txt";
            File.WriteAllText(Staticks.FolderFaktur + "\\" + fileName , result);

            using (Process fileopener = new Process())
            {
                fileopener.StartInfo.FileName = Staticks.FolderFaktur + "\\" + fileName;
                fileopener.Start();
            }
        }

        #endregion

    }
}
