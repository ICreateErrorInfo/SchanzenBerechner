using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

using Berechnung;

namespace SchanzenBerechner.Model {

    class FlugbahnViewModel {

        public FlugbahnViewModel(Schanze schanze, Flugbahn flugbahn) {

            var punkte   = new List<Point>();
            var segments = 100;
            var step     = (int) flugbahn.SprungWeite / segments;

            // Horizontaler Offset, wenn Schnaze vorhanden
            var x0 = schanze?.Länge ?? 0;

            // Absprungpunkt
            var x = 0.0;
            var y = flugbahn.Y(x);
            StartPunkt = new Point(x0 + x, y);
            punkte.Add(StartPunkt);

            // Bahnpunkte
            for (int i = 1; i <= segments; i++) {

                x = i * step;
                y = flugbahn.Y(x);

                punkte.Add(new Point(x0 + x, y));
            }

            // Aufprallpunkt
            x = flugbahn.SprungWeite;
            y = flugbahn.Y(x);

            EndPunkt = new Point(x0 + x, y);
            punkte.Add(EndPunkt);

            Punkte = new PointCollection(punkte);

            Scheitelpunkt = new Point(x0 + flugbahn.ScheitelpunktX, flugbahn.ScheitelpunktY);
        }

        public Point           StartPunkt { get; }
        public PointCollection Punkte     { get; }

        public Point Scheitelpunkt { get; }

        public Point EndPunkt { get; }

    }

}