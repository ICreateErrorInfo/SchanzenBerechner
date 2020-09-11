using System;
using System.Text;
using System.Windows;

using Berechnung;

namespace SchanzenBerechner {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        public MainWindow() {
            InitializeComponent();
        }

        private void OnBerechnenClickTab1(object sender, RoutedEventArgs e) {
            try {

                Winkel winkel          = Winkel.FromDeg(double.Parse(AbsprungWinkelTextBox_Tab1.Text));
                double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox_Tab1.Text) / 3.6;
                double schanzenHöhe    = double.Parse(AbsprungHöheTextBox_Tab1.Text)            * 0.01;

                var schanze  = Schanze.Create(schanzenHöhe, winkel);
                var flugbahn = Flugbahn.Create(schanze, geschwindigkeit);

                SchanzenVisualisierung.Schanze  = schanze;
                SchanzenVisualisierung.Flugbahn = flugbahn;
                OutputTab1.Text                 = GetOutputText(schanze, flugbahn);
            } catch (Exception ex) {
                OutputTab1.Text                 = ex.Message;
                SchanzenVisualisierung.Schanze  = null;
                SchanzenVisualisierung.Flugbahn = null;
            }
        }

        private string GetOutputText(Schanze schanze, Flugbahn flugbahn) {
            var sb = new StringBuilder();
            if (flugbahn != null) {
                sb.AppendLine($"Absprunggeschwindigkeit: {flugbahn.AbsprungGeschwindigkeit * 3.6:F2}km/h");
                sb.AppendLine($"Aufprallgeschwindigkeit: {flugbahn.AufprallGeschwindigkeit * 3.6:F2}km/h");
                sb.AppendLine($"Absprungwinkel:          {flugbahn.AbsprungWinkel.Deg:F2}°");
                sb.AppendLine($"Aufprallwinkel:          {flugbahn.AufprallWinkel.Deg:F2}°");
                sb.AppendLine($"Sprunghöhe:              {flugbahn.ScheitelpunktY:F2}m");
                sb.AppendLine($"Sprungweite:             {flugbahn.SprungWeite:F2}m");
            }

            if (schanze != null) {
                sb.AppendLine($"Schanzenhöhe:            {schanze.Höhe:F2}m");
                sb.AppendLine($"Schanzenlänge:           {schanze.Länge:F2}m");
                sb.AppendLine($"Schanzenradius:          {schanze.Radius:F2}m");
            }

            return sb.ToString();
        }

        private void OnBerechneClickTab2(object sender, RoutedEventArgs e) {
            double höhe            = double.Parse(SprungHöheTextBox_Tab2.Text);
            double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox_Tab2.Text) / 3.6;
            double schanzenHöhe    = 0.16; // TODO Schanzenhöhe...

            var winkel = Winkel.FromRad(Wurfparabel.Abwurfwinkel(v0: geschwindigkeit, y0: schanzenHöhe, ys: höhe));

            var schanze  = Schanze.Create(schanzenHöhe, winkel);
            var flugbahn = Flugbahn.Create(schanze, geschwindigkeit);

            AbsprungWinkelText_Tab2.Content   = winkel.Deg;
            SprungEntfernungText_Tab2.Content = $"{flugbahn.SprungWeite:F2}m";

            SchanzenVisualisierung.Schanze  = schanze;
            SchanzenVisualisierung.Flugbahn = flugbahn;
        }

        private void OnBerechneClickTab3(object sender, RoutedEventArgs e) {

        }

    }

}