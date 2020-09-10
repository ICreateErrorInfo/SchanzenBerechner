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

            Matrix m = Matrix.Identity;
            m.Rotate(flugbahn.AbsprungWinkel.Deg);

            Vector v = new Vector(flugbahn.Scale, 0);
            

            AbsprungTangentenKontrollpunkt1 = StartPunkt + v;
            AbsprungTangentenKontrollpunkt2 = StartPunkt + v * m;

            var winkelSize = flugbahn.Scale * 0.2;
            v = new Vector(winkelSize, 0);

            AbsprungWinkelPunkt1 = StartPunkt + v;
            AbsprungWinkelPunkt2 = StartPunkt + v * m;
            AbsprungWinkelSize   = new Size(winkelSize, winkelSize);
        }

        public Point           StartPunkt { get; }
        public PointCollection Punkte     { get; }

        public Point Scheitelpunkt                  { get; }
        public Point AbsprungTangentenKontrollpunkt1 { get; }
        public Point AbsprungTangentenKontrollpunkt2 { get; }
        public Point AbsprungWinkelPunkt1           { get; }
        public Point AbsprungWinkelPunkt2           { get; }
        public Size AbsprungWinkelSize           { get; }

        public Point EndPunkt { get; }

    }

}