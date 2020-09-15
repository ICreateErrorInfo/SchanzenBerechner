using System;
using System.Windows;

using Berechnung;
using Berechnung.Einheiten;

using SchanzenBerechner.Model;

namespace SchanzenBerechner {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        readonly SceneViewModel _viewModel;

        public MainWindow() {
            _viewModel                       = new SceneViewModel();
            DataContext                      = _viewModel;

            InitializeComponent();

            SchanzenVisualisierung.ViewModel = _viewModel;

            OnBerechnenClickTab1(this, null);
        }

        private void OnBerechnenClickTab1(object sender, RoutedEventArgs e) {
            try {

                var winkel          = Winkel.FromDeg(double.Parse(AbsprungWinkelTextBox_Tab1.Text));
                var geschwindigkeit = Geschwindigkeit.FromKilometerProStunde(double.Parse(AbsprungGeschwindigkeitTextBox_Tab1.Text));
                var schanzenHöhe    = Länge.FromCentimeter(double.Parse(AbsprungHöheTextBox_Tab1.Text));

                var schanze   = Schanze.Create(schanzenHöhe, winkel);
                var flugbahn  = Flugbahn.Create(schanze, geschwindigkeit);
                var viewModel = SettingViewModel.Create(schanze, flugbahn);

                _viewModel.Settings.Add(viewModel);

                OutputTab1.Text = viewModel.DisplayString;
            } catch (Exception ex) {
                OutputTab1.Text                  = ex.Message;
                SchanzenVisualisierung.ViewModel = null;
            }

        }

    }

}