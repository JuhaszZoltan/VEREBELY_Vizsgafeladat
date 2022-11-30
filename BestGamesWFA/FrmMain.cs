using Microsoft.Data.SqlClient;
using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace BestGamesWFA
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            cb.SelectedIndexChanged += OnSearch;
            tb.TextChanged += OnSearch;
            cb.TextChanged += OnSearch;
            dgv.CellClick += OnGoogleSearch;
            link.LinkClicked += OnLinkClick;
        }

        private void OnLinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(link.Text) { UseShellExecute = true });
        }

        private void OnGoogleSearch(object? sender, DataGridViewCellEventArgs e)
        {
            link.Text = "https://www.google.com/search?q="
                + dgv[1, e.RowIndex].Value.ToString().Replace(" ", "+");
        }

        private void OnSearch(object? sender, EventArgs e) => FillDGV();

        private void FillDGV()
        {
            dgv.Rows.Clear();
            using SqlConnection conn = new("Server=(localdb)\\MSSQLLocalDB; Database=bestgames;");
            conn.Open();
            var rdr = new SqlCommand(
                "SELECT game.id, title, year, genre.name " +
                "FROM game INNER JOIN genre " +
                "ON genreId = genre.id " +
                $"WHERE title LIKE '{tb.Text}%' " +
                $"AND genre.name LIKE '{cb.Text}%' " +
                "ORDER BY title ASC;",
                conn).ExecuteReader();
            while (rdr.Read()) dgv.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FillDGV();
            PopCb();
        }

        private void PopCb()
        {
            using SqlConnection conn = new("Server=(localdb)\\MSSQLLocalDB; Database=bestgames;");
            conn.Open();
            var rdr = new SqlCommand(
                "SELECT name FROM genre ORDER BY name;",
                conn).ExecuteReader();
            while (rdr.Read()) cb.Items.Add(rdr[0]);
            
        }
    }
}