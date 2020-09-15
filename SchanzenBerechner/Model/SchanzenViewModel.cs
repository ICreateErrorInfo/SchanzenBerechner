﻿using System.Windows;

using Berechnung;

namespace SchanzenBerechner.Model {

    public class SchanzenViewModel: ViewModel {

        private readonly Schanze _schanze;

        public SchanzenViewModel(Schanze schanze) {
            _schanze = schanze;

        }

        public Point StartPunkt => new Point(0, 0);

        public Point AbsprungPunkt => new Point(_schanze.Länge.Meter, _schanze.Höhe.Meter);

        public Point EndPunkt => new Point(_schanze.Länge.Meter, 0);

        public Size  RadiusGröße         => new Size(_schanze.Radius.Meter, _schanze.Radius.Meter);
        public Point SchanzenMittelpunkt => new Point(0, _schanze.Radius.Meter);

    }

}