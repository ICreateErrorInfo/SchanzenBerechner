﻿using System.Windows;

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
            double winkelRad       = Winkel.ToRad(winkel);
            double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox_Tab1.Text) / 3.6;
            double schanzenHöhe    = 0.16; // TODO Schanzenhöhe...

            var schanze  = Schanze.Create(schanzenHöhe, winkelRad);
            var flugbahn = Flugbahn.Create(schanze, geschwindigkeit);

            SprungHöheText_Tab1.Content       = flugbahn.SprungHöhe;
            SprungEntfernungText_Tab1.Content = flugbahn.SprungWeite;

            SchanzenVisualisierung.Schanze  = schanze;
            SchanzenVisualisierung.Flugbahn = flugbahn;
        }

        private void OnBerechneClickTab2(object sender, RoutedEventArgs e)
        {
            double höhe = double.Parse(SprungHöheTextBox_Tab2.Text);
            double geschwindigkeit = double.Parse(AbsprungGeschwindigkeitTextBox_Tab2.Text)/3.6;

            AbsprungWinkelText_Tab2.Content = SchrägerWurf.Winkel(v0: geschwindigkeit, ys: höhe);
        }
        private void OnBerechneClickTab3(object sender, RoutedEventArgs e)
        {

        }
    }
}
