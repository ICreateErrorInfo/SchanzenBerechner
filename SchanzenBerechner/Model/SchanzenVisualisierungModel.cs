using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using Berechnung;

namespace SchanzenBerechner.Model {

    class SchanzenVisualisierungViewModel: INotifyPropertyChanged {

        public SchanzenVisualisierungViewModel() {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) {

                Winkel winkel          = Winkel.FromDeg(22);
                double geschwindigkeit = 20 / 3.6;
                double schanzenHöhe    = 0.16;

                var schanze  = Berechnung.Schanze.Create(schanzenHöhe, winkel);
                var flugbahn = Berechnung.Flugbahn.Create(schanze, geschwindigkeit);

                Invalidate(schanze, flugbahn);
            }
        }

        private SchanzenViewModel _schanzenViewModel;

        public SchanzenViewModel Schanze {
            get { return _schanzenViewModel; }
            private set {
                _schanzenViewModel = value;
                OnPropertyChanged();
            }
        }

        private FlugbahnViewModel _flugbahnModel;

        public FlugbahnViewModel Flugbahn {
            get { return _flugbahnModel; }
            private set {
                _flugbahnModel = value;
                OnPropertyChanged();
            }
        }

        double _canvasWidth;

        public double CanvasWidth {
            get { return _canvasWidth; }
            private set {
                _canvasWidth = value;
                OnPropertyChanged();
            }
        }

        double _canvaHeight;

        public double CanvasHeight {
            get { return _canvaHeight; }
            private set {
                _canvaHeight = value;
                OnPropertyChanged();
            }
        }

        bool _bodenAnzeigen;

        public bool BodenAnzeigen {
            get { return _bodenAnzeigen; }
            private set {
                _bodenAnzeigen = value;
                OnPropertyChanged();
            }
        }

        public void Invalidate(Schanze schanze, Flugbahn flugbahn) {

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
            BodenAnzeigen = schanze  != null || flugbahn != null;
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
                height =  Math.Max(flugbahn.SprungHöhe, height);
            }

            return new Size(width, height);
        }

    }

}