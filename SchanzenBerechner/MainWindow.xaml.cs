﻿using System;
using System.Windows;
using System.Windows.Controls;

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
            _viewModel  = new SceneViewModel();
            DataContext = _viewModel;

            InitializeComponent();

            SchanzenVisualisierung.ViewModel = _viewModel;

            OnBerechnenClick(this, null);
        }

        private void OnBerechnenClick(object sender, RoutedEventArgs e) {
            try {

                var winkel          = Winkel.FromDeg(double.Parse(AbsprungWinkelTextBox.Text));
                var geschwindigkeit = Geschwindigkeit.FromKilometerProStunde(double.Parse(AbsprungGeschwindigkeitTextBox.Text));
                var schanzenHöhe    = Länge.FromCentimeter(double.Parse(AbsprungHöheTextBox.Text));

                var schanze  = Schanze.Create(schanzenHöhe, winkel);
                var flugbahn = Flugbahn.Create(schanze, geschwindigkeit);
                var setting  = SettingViewModel.Create(schanze, flugbahn);

                _viewModel.Settings.Add(setting);
                _viewModel.SelectedSetting = setting;
            } catch (Exception ex) {
                // TODO Fehler visualisieren
                SchanzenVisualisierung.ViewModel = null;
            }

        }

        void OnDeleteSettingClick(object sender, RoutedEventArgs e) {
            if (sender is Button button &&
                button.Tag is SettingViewModel setting) {

                _viewModel.Settings.Remove(setting);
            }
        }

    }

}