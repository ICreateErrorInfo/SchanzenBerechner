﻿#region Using Directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

using Berechnung;

#endregion

namespace SchanzenBerechner.Model {

    [DebuggerDisplay("{" + nameof(DisplayString) + ",nq}")]
    class SchanzenVisualisierungViewModel: INotifyPropertyChanged {

        Schanze  _orgSchanze;
        Flugbahn _orgFlugbahn;

        public SchanzenVisualisierungViewModel() {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) {

                var    winkel          = Winkel.FromDeg(22);
                var    geschwindigkeit = Geschwindigkeit.FromKmProH(20);
                double schanzenHöhe    = 0.16;

                var schanze  = Berechnung.Schanze.Create(schanzenHöhe, winkel);
                var flugbahn = Berechnung.Flugbahn.Create(schanze, geschwindigkeit);

                Invalidate(schanze, flugbahn);

                RenderMetrics = true;
            }

        }

        private SchanzenViewModel _schanzenViewModel;

        public SchanzenViewModel Schanze {
            get => _schanzenViewModel;
            private set {
                _schanzenViewModel = value;
                OnPropertyChanged();
            }
        }

        private FlugbahnViewModel _flugbahnModel;

        public FlugbahnViewModel Flugbahn {
            get => _flugbahnModel;
            private set {
                _flugbahnModel = value;
                OnPropertyChanged();
            }
        }

        double _canvasWidth;

        public double CanvasWidth {
            get => _canvasWidth;
            private set {
                _canvasWidth = value;
                OnPropertyChanged();
            }
        }

        double _canvaHeight;

        public double CanvasHeight {
            get => _canvaHeight;
            private set {
                _canvaHeight = value;
                OnPropertyChanged();
            }
        }

        bool _renderScene;

        public bool RenderScene {
            get => _renderScene;
            private set {
                _renderScene = value;
                OnPropertyChanged();
            }
        }

        bool _renderMetrics;

        public bool RenderMetrics {
            get => _renderMetrics;
            set {
                _renderMetrics = value;
                OnPropertyChanged();
            }
        }

        string _displayString;

        public string DisplayString {
            get => _displayString;
            set {
                _displayString = value;
                OnPropertyChanged();
            }
        }

        public void Invalidate(Schanze schanze, Flugbahn flugbahn) {

            _orgSchanze  = schanze;
            _orgFlugbahn = flugbahn;

            var orgSize = CalculateDesiredCanvasSize(schanze, flugbahn);
            // Wir sagen es soll alles auf 1000 Pixel Platz haben...

            var scale = 1000 / orgSize.Width;

            schanze  = schanze?.WithScale(scale);
            flugbahn = flugbahn?.WithScale(scale);

            var size = CalculateDesiredCanvasSize(schanze, flugbahn);

            CanvasWidth  = size.Width;
            CanvasHeight = size.Height;

            Schanze       = schanze  != null ? new SchanzenViewModel(schanze) : null;
            Flugbahn      = flugbahn != null ? new FlugbahnViewModel(schanze, flugbahn) : null;
            RenderScene   = schanze  != null || flugbahn != null;
            DisplayString = ToDisplayString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        static Size CalculateDesiredCanvasSize(Schanze schanze, Flugbahn flugbahn) {

            var width  = 1.0;
            var height = 1.0;

            if (schanze != null) {
                width  = schanze.Länge;
                height = schanze.Höhe;
            }

            if (flugbahn != null) {
                width  += flugbahn.SprungWeite;
                height =  Math.Max(flugbahn.ScheitelpunktY, height);
            }

            return new Size(width, height);
        }

        public string ToDisplayString() {
            var sb = new StringBuilder();
            if (_orgFlugbahn != null) {
                sb.AppendLine($"Absprunggeschwindigkeit: {_orgFlugbahn.AbsprungGeschwindigkeit.KmProH:F2}km/h");
                sb.AppendLine($"Aufprallgeschwindigkeit: {_orgFlugbahn.AufprallGeschwindigkeit.KmProH:F2}km/h");
                sb.AppendLine($"Absprungwinkel:          {_orgFlugbahn.AbsprungWinkel.Deg:F2}°");
                sb.AppendLine($"Aufprallwinkel:          {_orgFlugbahn.AufprallWinkel.Deg:F2}°");
                sb.AppendLine($"Sprunghöhe:              {_orgFlugbahn.ScheitelpunktY:F2}m");
                sb.AppendLine($"Sprungweite:             {_orgFlugbahn.SprungWeite:F2}m");
            }

            if (_orgSchanze != null) {
                sb.AppendLine($"Schanzenhöhe:            {_orgSchanze.Höhe:F2}m");
                sb.AppendLine($"Schanzenlänge:           {_orgSchanze.Länge:F2}m");
                sb.AppendLine($"Schanzenradius:          {_orgSchanze.Radius:F2}m");
            }

            return sb.ToString();
        }

    }

}