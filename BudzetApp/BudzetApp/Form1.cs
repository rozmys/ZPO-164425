using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public class Transakcja
    {
        public string Kategoria { get; set; }
        public string Typ { get; set; }
        public decimal Kwota { get; set; }
        public DateTime Data { get; set; }

        public bool SprKwota(decimal kwota)
        {
            return Kwota >= 0;
        }

        public Transakcja(string kategoria, string typ, decimal kwota, DateTime data)
        {
            Kategoria = kategoria;
            Typ = typ;
            Kwota = kwota;
            Data = data;
        }


    }

    public class ArgumentException: Exception
    {
        public ArgumentException(string message) : base(message)
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
        }

     }
}
