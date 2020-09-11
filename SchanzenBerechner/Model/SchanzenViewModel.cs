using System.Windows;

using Berechnung;

namespace SchanzenBerechner.Model {

    class SchanzenViewModel {

        private readonly Schanze _schanze;

        public SchanzenViewModel(Schanze schanze) {
            _schanze = schanze;

        }

        public Point StartPunkt => new Point(0, 0);

        public Point AbsprungPunkt => new Point(_schanze.Länge, _schanze.Höhe);

        public Point EndPunkt => new Point(_schanze.Länge, 0);

        public Size  RadiusGröße         => new Size(_schanze.Radius, _schanze.Radius);
        public Point SchanzenMittelpunkt => new Point(0, _schanze.Radius);

    }

}