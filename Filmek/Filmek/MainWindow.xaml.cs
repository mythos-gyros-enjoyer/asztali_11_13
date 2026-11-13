using System.Windows;
using System.Windows.Controls;

namespace Filmek;

public class Film
{
    public string Cim { get; set; } = "";
    public string Mufaj { get; set; } = "";
    public int Hossz { get; set; }
    public double Ertekeles { get; set; }
}

public partial class MainWindow : Window
{
    private List<Film> filmek = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private static List<Film> Adatok() => new()
    {
        new Film { Cim = "Csillagok háborúja", Mufaj = "sci-fi", Hossz = 121, Ertekeles = 8.6 },
        new Film { Cim = "Titanic", Mufaj = "dráma", Hossz = 195, Ertekeles = 7.8 },
        new Film { Cim = "A Gyűrűk Ura", Mufaj = "fantasy", Hossz = 201, Ertekeles = 8.8 },
        new Film { Cim = "Mátrix", Mufaj = "sci-fi", Hossz = 136, Ertekeles = 8.7 },
        new Film { Cim = "Shrek", Mufaj = "animáció", Hossz = 90, Ertekeles = 7.9 },
    };

    private bool VanAdat()
    {
        if (filmek.Count > 0) return true;
        MessageBox.Show("Előbb töltsd be a filmeket!");
        return false;
    }

    private async void Betoltes_Click(object sender, RoutedEventArgs e)
    {
        pbBetoltes.Value = 0;
        for (int i = 1; i <= 10; i++)
        {
            await Task.Delay(40);
            pbBetoltes.Value = i * 10;
        }
        filmek = Adatok();
        dgFilmek.ItemsSource = filmek;
    }

    private void Darab_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        MessageBox.Show($"Összesen {filmek.Count} film van.");
    }

    private void Leghosszabb_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        var f = filmek.OrderByDescending(x => x.Hossz).First();
        MessageBox.Show($"Leghosszabb film: {f.Cim}\nHossz: {f.Hossz} perc");
    }

    private void Atlag_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        MessageBox.Show($"Átlag értékelés: {filmek.Average(x => x.Ertekeles):F2}");
    }

    private void CsakSciFi_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        dgFilmek.ItemsSource = filmek.Where(x => x.Mufaj == "sci-fi").ToList();
    }

    private void CsakCimek_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        MessageBox.Show(string.Join("\n", filmek.Select(x => x.Cim)), "Címek");
    }

    private void Rendezes_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        dgFilmek.ItemsSource = filmek.OrderBy(x => x.Cim, StringComparer.CurrentCultureIgnoreCase).ToList();
    }

    private void Filmek_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dgFilmek.SelectedItem is Film f)
            MessageBox.Show($"Kiválasztott film: {f.Cim}\nÉrtékelés: {f.Ertekeles:0.0}");
    }
}
