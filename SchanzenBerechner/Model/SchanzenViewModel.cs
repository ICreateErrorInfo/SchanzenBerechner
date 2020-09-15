using System.Windows;

using Berechnung;

namespace SchanzenBerechner.Model {

    public class SchanzenViewModel: ViewModel {

        private readonly Schanze _schanze;

        public SchanzenViewModel(Schanze schanze) {
            _schanze = schanze;

        }

        public Point StartPunkt          => new Point(x: 0,                    y: 0);
        public Point AbsprungPunkt       => new Point(x: _schanze.Länge.Meter, y: _schanze.Höhe.Meter);
        public Point EndPunkt            => new Point(x: _schanze.Länge.Meter, y: 0);
        public Point SchanzenMittelpunkt => new Point(x: 0,                    y: _schanze.Radius.Meter);
        public Size  RadiusGröße         => new Size(width: _schanze.Radius.Meter, height: _schanze.Radius.Meter);

    }

}