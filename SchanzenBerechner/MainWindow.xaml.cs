using System;
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

                var winkel          = Winkel.FromDeg(double.Parse(AbsprungWinkelTextBox_Tab1.Text));
                var geschwindigkeit = Geschwindigkeit.FromKmProH(double.Parse(AbsprungGeschwindigkeitTextBox_Tab1.Text));
                var schanzenHöhe    = double.Parse(AbsprungHöheTextBox_Tab1.Text) * 0.01;

                var schanze  = Schanze.Create(schanzenHöhe, winkel);
                var flugbahn = Flugbahn.Create(schanze, geschwindigkeit);

                SchanzenVisualisierung.Schanze  = schanze;
                SchanzenVisualisierung.Flugbahn = flugbahn;
                OutputTab1.Text                 = SchanzenVisualisierung.ViewModel.DisplayString;
            } catch (Exception ex) {
                OutputTab1.Text                 = ex.Message;
                SchanzenVisualisierung.Schanze  = null;
                SchanzenVisualisierung.Flugbahn = null;
            }
        }
        
        private void OnBerechneClickTab2(object sender, RoutedEventArgs e) {
            var höhe            = double.Parse(SprungHöheTextBox_Tab2.Text);
            var geschwindigkeit = Geschwindigkeit.FromKmProH(double.Parse(AbsprungGeschwindigkeitTextBox_Tab2.Text));
            var schanzenHöhe    = 0.16; // TODO Schanzenhöhe...

            var winkel = Winkel.FromRad(Wurfparabel.Abwurfwinkel(v0: geschwindigkeit.MProS, y0: schanzenHöhe, ys: höhe));

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