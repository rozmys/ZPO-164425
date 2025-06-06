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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
            var config = Config.WczytajZPliku(configPath);
            if (config != null)
                Config.Instance.Waluta = config.Waluta;
            OdswiezWidok();
            btnDodaj.Click += btnDodaj_Click;
            btnEksp.Click += btnEksp_Click;
            btnImp.Click += btnImp_Click;
            btnClr.Click += btnClr_Click;
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
            string waluta = Config.Instance.Waluta;
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

            string waluta = Config.Instance.Waluta;

            lblPrzyZl.Text = $"{budzetManager.GetPrzychody():0.00} {waluta}";
            lblWydZl.Text = $"{budzetManager.GetWydatki():0.00} {waluta}";
            lblOszZl.Text = $"{budzetManager.GetOsz():0.00} {waluta}";
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

            string waluta = Config.Instance.Waluta;
            lblPrzyZl.Text = $"{przychody:0.00} {waluta}";
            lblWydZl.Text = $"{wydatki:0.00} {waluta}";
            lblOszZl.Text = $"{oszczednosci:0.00} {waluta}";
        }

        private void btnEksp_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Pliki XML (*.xml)|*.xml";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Ob³ugaPliku.ZapiszTransakcjeDoXML(budzetManager.PobierzTransakcje(), dlg.FileName);
                        MessageBox.Show("Eksport zakoñczony!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"B³¹d eksportu: {ex.Message}");
                    }
                }
            }
        }

        private void btnImp_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Pliki XML (*.xml)|*.xml";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lista = Ob³ugaPliku.WczytajTransakcjeZXML(dlg.FileName);
                        foreach (var t in lista)
                            budzetManager.DodajTransakcje(t);
                        OdswiezWidok();
                        MessageBox.Show("Import zakoñczony!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"B³¹d importu: {ex.Message}");
                    }
                }
            }
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usun¹æ wszystkie transakcje?", "Potwierdzenie", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                budzetManager.UsunWszystkieTransakcje();
                OdswiezWidok();
                MessageBox.Show("Wszystkie transakcje zosta³y usuniête.");
            }
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

        public Transakcja() : base(0, DateTime.Now, "") { }

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

        public void UsunWszystkieTransakcje()
        {
            transakcje.Clear();
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
        public static void ZapiszTransakcjeDoXML(List<Transakcja> transakcje, string sciezka)
        {
            var serializer = new XmlSerializer(typeof(List<Transakcja>));
            using (var writer = new StreamWriter(sciezka))
            {
                serializer.Serialize(writer, transakcje);
            }
        }

        public static List<Transakcja> WczytajTransakcjeZXML(string sciezka)
        {
            var serializer = new XmlSerializer(typeof(List<Transakcja>));
            using (var reader = new StreamReader(sciezka))
            {
                return (List<Transakcja>)serializer.Deserialize(reader);
            }
        }
    }

    public class Config
    {
        private static Config? _instance;
        private static readonly object _lock = new object();

        // Dodaj atrybuty XML
        [XmlElement("Waluta")]
        public string Waluta { get; set; } = "PLN";

        // Konstruktor bezparametrowy wymagany przez XmlSerializer
        public Config() { }

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

    }
}