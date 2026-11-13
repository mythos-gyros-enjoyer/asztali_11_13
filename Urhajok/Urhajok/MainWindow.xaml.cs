using System.Windows;

namespace Urhajok;

public class Urhajo
{
    public string Nev { get; set; } = "";
    public int Legenyseg { get; set; }
    public int MaxHatotav { get; set; }
    public int UzemenyagSzint { get; set; }
    public string Gyarto { get; set; } = "";
    public string KuldetesStatusz { get; set; } = "";
    public int InditasEve { get; set; }
}

public partial class MainWindow : Window
{
    private List<Urhajo> urhajok = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private static List<Urhajo> Adatok() => new()
    {
        new Urhajo { Nev = "Falcon-9", Legenyseg = 7, MaxHatotav = 550000, UzemenyagSzint = 87, Gyarto = "SpaceX", KuldetesStatusz = "Sikeres", InditasEve = 2020 },
        new Urhajo { Nev = "Orion-X1", Legenyseg = 4, MaxHatotav = 820000, UzemenyagSzint = 73, Gyarto = "NASA", KuldetesStatusz = "Folyamatban", InditasEve = 2023 },
        new Urhajo { Nev = "StarRider", Legenyseg = 12, MaxHatotav = 910000, UzemenyagSzint = 66, Gyarto = "Boeing", KuldetesStatusz = "Sikeres", InditasEve = 2019 },
        new Urhajo { Nev = "SolarWind", Legenyseg = 3, MaxHatotav = 300000, UzemenyagSzint = 92, Gyarto = "ESA", KuldetesStatusz = "Sikertelen", InditasEve = 2018 },
        new Urhajo { Nev = "Galaxy-Cruiser", Legenyseg = 18, MaxHatotav = 1200000, UzemenyagSzint = 54, Gyarto = "Blue Origin", KuldetesStatusz = "Sikeres", InditasEve = 2022 },
        new Urhajo { Nev = "Nova-Prime", Legenyseg = 10, MaxHatotav = 760000, UzemenyagSzint = 80, Gyarto = "Roscosmos", KuldetesStatusz = "Sikeres", InditasEve = 2017 },
        new Urhajo { Nev = "Pioneer-VII", Legenyseg = 6, MaxHatotav = 640000, UzemenyagSzint = 70, Gyarto = "NASA", KuldetesStatusz = "Folyamatban", InditasEve = 2024 },
        new Urhajo { Nev = "Titan-Hawk", Legenyseg = 14, MaxHatotav = 1050000, UzemenyagSzint = 48, Gyarto = "SpaceX", KuldetesStatusz = "Sikeres", InditasEve = 2021 },
        new Urhajo { Nev = "CosmoJet", Legenyseg = 5, MaxHatotav = 430000, UzemenyagSzint = 93, Gyarto = "JAXA", KuldetesStatusz = "Sikertelen", InditasEve = 2016 },
        new Urhajo { Nev = "Nebula-FX", Legenyseg = 11, MaxHatotav = 870000, UzemenyagSzint = 60, Gyarto = "ESA", KuldetesStatusz = "Sikeres", InditasEve = 2020 },
    };

    private bool VanAdat()
    {
        if (urhajok.Count > 0) return true;
        txtEredmeny.Text = "Előbb töltsd be az adatokat!";
        return false;
    }

    private void Betoltes_Click(object sender, RoutedEventArgs e)
    {
        urhajok = Adatok();
        dgUrhajok.ItemsSource = urhajok;
        txtEredmeny.Text = "Adatok betöltve.";
    }

    private void Darab_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        txtEredmeny.Text = $"Összesen {urhajok.Count} űrhajó van.";
    }

    private void Hatotav_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        var u = urhajok.OrderByDescending(x => x.MaxHatotav).First();
        txtEredmeny.Text = $"A legnagyobb hatótáv: {u.Nev} ({u.MaxHatotav})";
    }

    private void Sikeres_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        var lista = urhajok.Where(x => x.KuldetesStatusz == "Sikeres").ToList();
        dgUrhajok.ItemsSource = lista;
        txtEredmeny.Text = $"Sikeres küldetések: {lista.Count}";
    }

    private void Atlag_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        txtEredmeny.Text = $"Átlagos legénységszám: {urhajok.Average(x => x.Legenyseg):F1}";
    }

    private void Csoportositas_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        txtEredmeny.Text = string.Join("\n", urhajok
            .GroupBy(x => x.Gyarto)
            .OrderBy(g => g.Key)
            .Select(g => $"{g.Key}: {g.Count()} db"));
    }

    private void Kereses_Click(object sender, RoutedEventArgs e)
    {
        if (!VanAdat()) return;
        string kulcs = txtKereses.Text.Trim();
        var talalatok = urhajok
            .Where(x => x.Nev.IndexOf(kulcs, StringComparison.CurrentCultureIgnoreCase) >= 0)
            .ToList();
        dgUrhajok.ItemsSource = talalatok;
        txtEredmeny.Text = talalatok.Count == 0 ? "Nincs találat." : $"Találatok: {talalatok.Count}";
    }
}
