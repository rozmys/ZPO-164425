using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;

namespace BudzetApp
{
    public partial class Form1 : Form
    {
        private decimal przychody = 0;
        private decimal wydatki = 0;
        private decimal oszczednosci = 0;
        private BudzetManager budzetManager = new BudzetManager();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            OdswiezWidok();
            btnDodaj.Click += btnDodaj_Click;
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label1_Click_1(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void label1_Click_2(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["colUsun"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string? kategoria = row.Cells["colKat"].Value?.ToString();
                string? kwotaStr = row.Cells["colKwota"].Value?.ToString();

                if (decimal.TryParse(kwotaStr, out decimal kwota))
                {
                    if (kategoria == "Przychody")
                        przychody -= kwota;
                    else if (kategoria == "Wydatki")
                        wydatki -= kwota;
                }

                dataGridView1.Rows.RemoveAt(e.RowIndex);
                AktualizujPodsumowanie();
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                decimal kwota = decimal.Parse(Kwota.Text);
                string? kategoria = comKat.SelectedItem?.ToString();
                string opis = textBox1.Text;

                if (string.IsNullOrWhiteSpace(kategoria))
                {
                    MessageBox.Show("Wybierz kategoriê transakcji.");
                    return;
                }

                Transakcja transakcja = new Transakcja(kategoria, kwota, DateTime.Now, opis);
                budzetManager.DodajTransakcje(transakcja);

                OdswiezWidok();
                WyczyscFormularz();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"B³¹d: {ex.Message}");
            }
        }

        private void WyczyscFormularz()
        {
            Kwota.Text = "";
            textBox1.Text = "";
            comKat.SelectedIndex = -1;
        }

        private void OdswiezWidok()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = budzetManager.PobierzTransakcje();

            lblPrzyZl.Text = $"{budzetManager.GetPrzychody()}";
            lblWydZl.Text = $"{budzetManager.GetWydatki()}";
            lblOszZl.Text = $"{budzetManager.GetOsz()}";
        }

        private void AktualizujPodsumowanie()
        {
            przychody = 0;
            wydatki = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                decimal kwota;
                if (decimal.TryParse(row.Cells["colKwota"].Value?.ToString(), out kwota))
                {
                    string? kategoria = row.Cells["colKategoria"].Value?.ToString();
                    if (kategoria == "Przychody")
                        przychody += kwota;
                    else
                        wydatki += kwota;
                }
            }

            oszczednosci = przychody - wydatki;

            lblPrzy.Text = $"Przychody: {przychody:C}";
            lblWyd.Text = $"Wydatki: {wydatki:C}";
            lblOsz.Text = $"Oszczêdnoœci: {oszczednosci:C}";
        }
    }

    public abstract class OperacjaFinansowa
    {
        public decimal Kwota { get; set; }
        public DateTime Data { get; set; }
        public string? Opis { get; set; }

        protected OperacjaFinansowa(decimal kwota, DateTime data, string? opis)
        {
            Kwota = kwota;
            Data = data;
            Opis = opis;
        }
    }

    public class Transakcja : OperacjaFinansowa
    {
        public string Kategoria { get; set; }

        public Transakcja(string kategoria, decimal kwota, DateTime data, string? opis)
            : base(kwota, data, opis)
        {
            Kategoria = kategoria;
        }

        public string GetKatOperacji()
        {
            return Kategoria;
        }
    }

    public class BudzetException : Exception
    {
        public BudzetException(string message) : base(message) { }
    }

    public class BudzetManager
    {
        private readonly List<Transakcja> transakcje = new List<Transakcja>();

        public void DodajTransakcje(Transakcja transakcja)
        {
            if (transakcja == null || string.IsNullOrWhiteSpace(transakcja.Kategoria) || transakcja.Kwota == 0)
                throw new BudzetException("Nieprawid³owa transakcja.");

            transakcje.Add(transakcja);
        }

        public void UsunTransakcje(int id)
        {
            if (id >= 0 && id < transakcje.Count)
                transakcje.RemoveAt(id);
        }

        public List<Transakcja> PobierzTransakcje()
        {
            return new List<Transakcja>(transakcje);
        }

        public decimal GetPrzychody()
        {
            return transakcje.Where(t => t.Kategoria == "Przychody").Sum(t => t.Kwota);
        }

        public decimal GetWydatki()
        {
            return transakcje.Where(t => t.Kategoria == "Wydatki").Sum(t => t.Kwota);
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
            using (var writer = new StreamWriter(sciezka))
            {
                foreach (var t in transakcje)
                {
                    writer.WriteLine($"{t.Kategoria};{t.Kwota};{t.Data:yyyy-MM-dd};{t.Opis}");
                }
            }
        }

        public static List<Transakcja> WczytajTransakcjeZCSV(string sciezka)
        {
            var lista = new List<Transakcja>();
            using (var reader = new StreamReader(sciezka))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    if (parts.Length >= 4 &&
                        decimal.TryParse(parts[1], out decimal kwota) &&
                        DateTime.TryParse(parts[2], out DateTime data))
                    {
                        lista.Add(new Transakcja(parts[0], kwota, data, parts[3]));
                    }
                }
            }
            return lista;
        }
    }

    public class Config
    {
        private static Config? _instance;
        private static readonly object _lock = new object();
        public string Waluta { get; set; } = "PLN";

        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new Config();
                    }
                }
                return _instance;
            }
        }

        public static Config? WczytajZPliku(string sciezka)
        {
            if (!File.Exists(sciezka))
                return null;

            using (var reader = new StreamReader(sciezka))
            {
                var serializer = new XmlSerializer(typeof(Config));
                var configObj = serializer.Deserialize(reader);
                if (configObj is Config config)
                    return config;
                else
                    return null;
            }
        }

        public void ZapiszDoPliku(string sciezka)
        {
            using (var writer = new StreamWriter(sciezka))
            {
                var serializer = new XmlSerializer(typeof(Config));
                serializer.Serialize(writer, this);
            }
        }
    }
}