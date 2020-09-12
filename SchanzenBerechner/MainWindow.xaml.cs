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
            OnBerechnenClickTab1(this,null);
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

    }

}