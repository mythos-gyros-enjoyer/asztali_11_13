using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

namespace Dolgozatok;

public class Dolgozat
{
    public string Nev { get; set; } = "";
    public int Eletkor { get; set; }
    public int Pontszam { get; set; }
}

public partial class MainWindow : Window
{
    private static readonly string FajlUtvonal =
        Path.Combine(AppContext.BaseDirectory, "dolgozatok.csv");

    private readonly ObservableCollection<Dolgozat> dolgozatok = new();

    public MainWindow()
    {
        InitializeComponent();
        Betoltes();
        dgDolgozatok.ItemsSource = dolgozatok;
    }

    private void Betoltes()
    {
        if (File.Exists(FajlUtvonal))
        {
            foreach (var sor in File.ReadAllLines(FajlUtvonal, Encoding.UTF8))
            {
                var d = sor.Split(';');
                if (d.Length == 3 && int.TryParse(d[1], out int kor) && int.TryParse(d[2], out int pont))
                    dolgozatok.Add(new Dolgozat { Nev = d[0], Eletkor = kor, Pontszam = pont });
            }
            return;
        }

        dolgozatok.Add(new Dolgozat { Nev = "Kiss Péter", Eletkor = 16, Pontszam = 87 });
        dolgozatok.Add(new Dolgozat { Nev = "Nagy Anna", Eletkor = 17, Pontszam = 92 });
        dolgozatok.Add(new Dolgozat { Nev = "Szabó Tamás", Eletkor = 16, Pontszam = 78 });
        dolgozatok.Add(new Dolgozat { Nev = "Tóth Éva", Eletkor = 18, Pontszam = 95 });
        dolgozatok.Add(new Dolgozat { Nev = "Varga Bence", Eletkor = 17, Pontszam = 81 });
        dolgozatok.Add(new Dolgozat { Nev = "Horváth Zsolt", Eletkor = 16, Pontszam = 65 });
        dolgozatok.Add(new Dolgozat { Nev = "Molnár Réka", Eletkor = 18, Pontszam = 89 });
        dolgozatok.Add(new Dolgozat { Nev = "Lakatos Dániel", Eletkor = 17, Pontszam = 72 });
    }

    private void Hozzaadas_Click(object sender, RoutedEventArgs e)
    {
        string nev = txtNev.Text.Trim();
        if (nev.Length == 0)
        {
            MessageBox.Show("Add meg a nevet!");
            return;
        }
        if (!int.TryParse(txtEletkor.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int kor) || kor <= 0)
        {
            MessageBox.Show("Az életkor pozitív egész szám legyen!");
            return;
        }
        if (!int.TryParse(txtPontszam.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int pont) || pont < 0)
        {
            MessageBox.Show("A pontszám nem negatív egész szám legyen!");
            return;
        }

        dolgozatok.Add(new Dolgozat { Nev = nev, Eletkor = kor, Pontszam = pont });
        txtNev.Clear();
        txtEletkor.Clear();
        txtPontszam.Clear();
        txtNev.Focus();
    }

    private void Mentes_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            File.WriteAllLines(FajlUtvonal,
                dolgozatok.Select(d => $"{d.Nev.Replace(';', ',')};{d.Eletkor};{d.Pontszam}"),
                Encoding.UTF8);
            MessageBox.Show($"Mentve: {FajlUtvonal}");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Mentési hiba: " + ex.Message);
        }
    }
}
