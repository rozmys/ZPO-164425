using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BudzetApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }

    public abstract class OperacjaFinansowa
    {
        public decimal Kwota { get; set; }
        public DateTime Data { get; set; }
        public string Opis { get; set; }

        public abstract string GetTypOperacji();
    }


    public class Transakcja: OperacjaFinansowa
    {
        public string Kategoria { get; set; }
        public string Typ { get; set; }


        public Transakcja(string kategoria, string typ, decimal kwota, DateTime data, string opis)
        {
            Kategoria = kategoria;
            Typ = typ;
            Kwota = kwota;
            Data = data;
            Opis = opis;
        }

        public override string GetTypOperacji()
        {
            return Typ;
        }


    }

    public class BudzetException: Exception
    {
        public BudzetException(string message) : base(message)
        {
        }
    }

    public interface IBudzetManager
    {
        void DodajTransakcje(Transakcja transakcja);
        void UsunTransakcje(int id);
        List<Transakcja> PobierzTransakcje();
        decimal GetPrzychody();
        decimal GetWydatki();
        decimal GetOsz();
    }

    public class BudzetManager : IBudzetManager
    {
        public readonly List<Transakcja> transakcje = new List<Transakcja>();

        public void DodajTransakcje(Transakcja transakcja)
        {
            if (transakcje == null || string.IsNullOrWhiteSpace(transakcja.Kategoria) || transakcja.Kwota == 0){
                throw new ArgumentException("Transakcja nie mo¿e byæ pusta lub mieæ nieprawid³owych danych.");
            }

            transakcje.Add(transakcja);

        }

        public void UsunTransakcje(int id)
        {
            if (id < 0 || id >= transakcje.Count)
            {
                throw new BudzetException("Nieprawid³owy indeks.");
            }

            transakcje.RemoveAt(id);
        }

        public List<Transakcja> PobierzTransakcje()
        {
            return transakcje;
        }

        public decimal GetPrzychody()
        {
            return transakcje.Where(t => t.Typ == "Przychód").Sum(t => t.Kwota);
        }

        public decimal GetWydatki()
        {
            return transakcje.Where(t => t.Typ == "Wydatek").Sum(t => t.Kwota);
        }

        public decimal GetOsz()
        {
            return GetPrzychody() - GetWydatki();
        }

     }

    public static class Ob³ugaPliku
    {
        public static void ZapiszTransakcjeDoPliku(List<Transakcja> transakcje, string sciezka)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Transakcja>));
                using (var writer = new StreamWriter(sciezka))
                {
                    serializer.Serialize(writer, transakcje);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas zapisywania do pliku: {ex.Message}");
            }
        }

        public static List<Transakcja> WczytajTransakcjeZCSV(string sciezka)
        {
            var lista = new List<Transakcja>();
            try
            {
                using (var reader = new StreamReader(sciezka))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length < 5) continue; // Sprawdzenie poprawnoœci danych
                        var transakcja = new Transakcja(parts[0], parts[1], decimal.Parse(parts[2]), DateTime.Parse(parts[3]), parts[4]);
                        lista.Add(transakcja);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas wczytywania z pliku: {ex.Message}");
            }
            return lista;
        }

    }

    public class Config
    {
        private static Config _instance;
        private static readonly object _lock = new object();

        public string Waluta { get; set; } = "PLN";

        private Config() { }

        public static Config Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = WczytajZPliku("config.xml") ?? new Config();
                    }
                    return _instance;
                }
            }
        }

        public static Config WczytajZPliku(string sciezka)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (var reader = new StreamReader(sciezka))
                {
                    return (Config)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas wczytywania konfiguracji: {ex.Message}");
                return null;
            }
        }

        public void ZapiszDoPliku(string sciezka)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                using (var writer = new StreamWriter(sciezka))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas zapisywania konfiguracji: {ex.Message}");
            }
        }
    }

}
