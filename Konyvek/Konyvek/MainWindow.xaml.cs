using System.Windows;

namespace Konyvek;

public class Konyv
{
    public string Cim { get; set; } = "";
    public string Szerzo { get; set; } = "";
    public int Oldalszam { get; set; }
    public string Mufaj { get; set; } = "";
    public bool Elerheto { get; set; }
}

public partial class MainWindow : Window
{
    private List<Konyv> konyvek = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private static List<Konyv> Adatok() => new()
    {
        new Konyv { Cim = "1984", Szerzo = "George Orwell", Oldalszam = 328, Mufaj = "Sci-fi", Elerheto = true },
        new Konyv { Cim = "A szolgálólány meséje", Szerzo = "Margaret Atwood", Oldalszam = 311, Mufaj = "Disztópia", Elerheto = false },
        new Konyv { Cim = "Az ember tragédiája", Szerzo = "Madách Imre", Oldalszam = 250, Mufaj = "Dráma", Elerheto = true },
        new Konyv { Cim = "Dűne", Szerzo = "Frank Herbert", Oldalszam = 604, Mufaj = "Sci-fi", Elerheto = true },
        new Konyv { Cim = "Egri csillagok", Szerzo = "Gárdonyi Géza", Oldalszam = 552, Mufaj = "Történelmi", Elerheto = false },
    };

    private bool VanAdat()
    {
        if (konyvek.Count > 0) return true;
        MessageBox.Show("Előbb töltsd be a könyveket!");
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
        konyvek = Adatok();
        dgKonyvek.ItemsSource = konyvek;
    }

    private void Darab_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        MessageBox.Show($"Összesen {konyvek.Count} könyv van.");
    }

    private void Leghosszabb_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        var k = konyvek.OrderByDescending(x => x.Oldalszam).First();
        MessageBox.Show($"A leghosszabb könyv: {k.Cim} ({k.Szerzo}), {k.Oldalszam} oldal.");
    }

    private void Atlag_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        MessageBox.Show($"Átlagos oldalszám: {konyvek.Average(x => x.Oldalszam):F1}");
    }

    private void CsakElerheto_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        dgKonyvek.ItemsSource = konyvek.Where(x => x.Elerheto).ToList();
    }

    private void CsakCimek_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        MessageBox.Show(string.Join("\n", konyvek.Select(x => x.Cim)), "Címek");
    }

    private void Rendezes_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        dgKonyvek.ItemsSource = konyvek.OrderBy(x => x.Szerzo, StringComparer.CurrentCultureIgnoreCase).ToList();
    }
}
