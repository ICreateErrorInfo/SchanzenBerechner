using System.Windows;

using Berechnung;

namespace SchanzenBerechner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnBerechnenClickTab1(object sender, RoutedEventArgs e) 
        {
            double winkel          = double.Parse(AbsprungWinkelTextBox_Tab1.Text);
            double winkelRad       = Berechne.ToRad(winkel);
            double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox_Tab1.Text) / 3.6;
            double höhe            = Berechne.Höhe(v0: geschwindigkeit, alpha: winkelRad);
            double schanzenHöhe    = 0.16; // TODO Schanzenhöhe...

            SprungHöheText_Tab1.Content       = höhe;
            SprungEntfernungText_Tab1.Content = Berechne.Weite(v0: geschwindigkeit, alpha:winkelRad);
            
            var schanze  = Schanze.Create(schanzenHöhe, winkelRad);
            var flugbahn = Flugbahn.Create(schanze, geschwindigkeit);

            SchanzenVisualisierung.Schanze  = schanze;
            SchanzenVisualisierung.Flugbahn = flugbahn;
        }

        private void OnBerechneClickTab2(object sender, RoutedEventArgs e)
        {
            double höhe = double.Parse(SprungHöheTextBox_Tab2.Text);
            double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox_Tab2.Text)/3.6;

            AbsprungWinkelText_Tab2.Content = Berechne.Winkel(v0: geschwindigkeit, ys: höhe);
        }
        private void OnBerechneClickTab3(object sender, RoutedEventArgs e)
        {

        }
    }
}
