#region Using Directives

using System;
using System.Diagnostics;
using System.Text;
using System.Windows;

using Berechnung;

#endregion

namespace SchanzenBerechner.Model {

    [DebuggerDisplay("{" + nameof(DisplayString) + ",nq}")]
    public class SettingViewModel: ViewModel {

        readonly Schanze  _orgSchanze;
        readonly Flugbahn _orgFlugbahn;

        public SettingViewModel(Schanze schanze = null, Flugbahn flugbahn = null) {
            //if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) {

            //    var winkel          = Winkel.FromDeg(22);
            //    var geschwindigkeit = Geschwindigkeit.FromKilometerProStunde(20);
            //    var schanzenHöhe    = Länge.FromCentimeter(16);

            //    schanze  = Berechnung.Schanze.Create(schanzenHöhe, winkel);
            //    flugbahn = Berechnung.Flugbahn.Create(schanze, geschwindigkeit);

            //    RenderMetrics = true;
            //}

            _orgSchanze  = schanze;
            _orgFlugbahn = flugbahn;
            _renderScene = true;

            Rescale();
        }

        public static SettingViewModel Create(Schanze schanze, Flugbahn flugbahn) {
            var viewModel = new SettingViewModel(schanze, flugbahn);
            return viewModel;
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
            get => _renderScene && _orgSchanze != null && _orgFlugbahn != null;
            set {
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

        public string DisplayString => ToDisplayString();

        public object DisplayName {
            get {
                var winkelDeg = _orgSchanze.Absprungwinkel.Deg;
                var höheCm    = _orgSchanze.Höhe.Centimeter;
                var v0        = _orgFlugbahn.AbsprungGeschwindigkeit.KilometerProStunde;
                return $"Winkel: {winkelDeg:F2}, Höhe: {höheCm:F2}cm, v: {v0:F2}km/h";
            }
        }

        public void Rescale() {
            var orgSize = CalculateNaturalSize();
            // Wir sagen es soll alles auf 1000 Pixel Platz haben...
            var scale = 1000 / orgSize.Width;

            Rescale(scale);
        }

        public void Rescale(double scale) {

            var schanze  = _orgSchanze?.WithScale(scale);
            var flugbahn = _orgFlugbahn?.WithScale(scale);

            var size = CalculateRequiredCanvasSize(schanze, flugbahn);

            CanvasWidth  = size.Width;
            CanvasHeight = size.Height;

            Schanze  = schanze  != null ? new SchanzenViewModel(schanze) : null;
            Flugbahn = flugbahn != null ? new FlugbahnViewModel(schanze, flugbahn) : null;
        }

        public Size CalculateNaturalSize() {
            var naturalSize = CalculateRequiredCanvasSize(_orgSchanze, _orgFlugbahn);
            return naturalSize;
        }

        static Size CalculateRequiredCanvasSize(Schanze schanze, Flugbahn flugbahn) {

            var width  = 1.0;
            var height = 1.0;

            if (schanze != null) {
                width  = schanze.Länge.Meter;
                height = schanze.Höhe.Meter;
            }

            if (flugbahn != null) {
                width  += flugbahn.SprungWeite.Meter;
                height =  Math.Max(flugbahn.ScheitelpunktY.Meter, height);
            }

            return new Size(width, height);
        }

        public string ToDisplayString() {
            var sb = new StringBuilder();
            if (_orgFlugbahn != null) {
                sb.AppendLine($"Absprunggeschwindigkeit: {_orgFlugbahn.AbsprungGeschwindigkeit.KilometerProStunde:F2}km/h");
                sb.AppendLine($"Aufprallgeschwindigkeit: {_orgFlugbahn.AufprallGeschwindigkeit.KilometerProStunde:F2}km/h");
                sb.AppendLine($"Absprungwinkel:          {_orgFlugbahn.AbsprungWinkel.Deg:F2}°");
                sb.AppendLine($"Aufprallwinkel:          {_orgFlugbahn.AufprallWinkel.Deg:F2}°");
                sb.AppendLine($"Sprunghöhe:              {_orgFlugbahn.ScheitelpunktY.Meter:F2}m");
                sb.AppendLine($"Sprungweite:             {_orgFlugbahn.SprungWeite.Meter:F2}m");
            }

            if (_orgSchanze != null) {
                sb.AppendLine($"Schanzenhöhe:            {_orgSchanze.Höhe.Meter:F2}m");
                sb.AppendLine($"Schanzenlänge:           {_orgSchanze.Länge.Meter:F2}m");
                sb.AppendLine($"Schanzenradius:          {_orgSchanze.Radius.Meter:F2}m");
            }

            return sb.ToString();
        }

    }

}