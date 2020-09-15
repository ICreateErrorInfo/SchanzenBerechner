using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

using Berechnung;

namespace SchanzenBerechner.Model {

    public class FlugbahnViewModel: ViewModel {

        public FlugbahnViewModel(Schanze schanze, Flugbahn flugbahn) {

            var punkte   = new List<Point>();
            var segments = 100;
            var step     = (int) flugbahn.SprungWeite.Meter / segments;

            // Horizontaler Offset, wenn Schnaze vorhanden
            var x0 = schanze?.Länge.Meter ?? 0;

            // Absprungpunkt
            var x = 0.0;
            var y = flugbahn.Y(x).Meter;
            StartPunkt = new Point(x0 + x, y);
            punkte.Add(StartPunkt);

            // Bahnpunkte
            for (int i = 1; i <= segments; i++) {

                x = i * step;
                y = flugbahn.Y(x).Meter;

                punkte.Add(new Point(x0 + x, y));
            }

            // Aufprallpunkt
            x = flugbahn.SprungWeite.Meter;
            y = flugbahn.Y(x).Meter;

            EndPunkt = new Point(x0 + x, y);
            punkte.Add(EndPunkt);

            Punkte = new PointCollection(punkte);

            Scheitelpunkt = new Point(x0 + flugbahn.ScheitelpunktX.Meter, flugbahn.ScheitelpunktY.Meter);

            var winkelSize     = flugbahn.Scale * 0.2;
            var tangentenLänge = flugbahn.Scale;
            // Absprung
            Matrix m = Matrix.Identity;
            m.Rotate(flugbahn.AbsprungWinkel.Deg);

            Vector v = new Vector(tangentenLänge, 0);

            AbsprungTangentenKontrollpunkt1 = StartPunkt + v;
            AbsprungTangentenKontrollpunkt2 = StartPunkt + v * m;
            
            v = new Vector(winkelSize, 0);

            AbsprungWinkelPunkt1 = StartPunkt + v;
            AbsprungWinkelPunkt2 = StartPunkt + v * m;
            AbsprungWinkelSize   = new Size(winkelSize, winkelSize);

            // Aufprall
            m = Matrix.Identity;
            m.Rotate(-flugbahn.AufprallWinkel.Deg);

            v = new Vector(-tangentenLänge, 0);

            AufprallTangentenKontrollpunkt1 = EndPunkt + v;
            AufprallTangentenKontrollpunkt2 = EndPunkt + v * m;
            
            v = new Vector(-winkelSize, 0);

            AufprallWinkelPunkt1 = EndPunkt + v;
            AufprallWinkelPunkt2 = EndPunkt + v * m;
            AufprallWinkelSize   = new Size(winkelSize, winkelSize);
        }

        public Point           StartPunkt    { get; }
        public PointCollection Punkte        { get; }
        public Point           EndPunkt      { get; }
        public Point           Scheitelpunkt { get; }

        public Point AbsprungTangentenKontrollpunkt1 { get; }
        public Point AbsprungTangentenKontrollpunkt2 { get; }
        public Point AbsprungWinkelPunkt1            { get; }
        public Point AbsprungWinkelPunkt2            { get; }
        public Size  AbsprungWinkelSize              { get; }

        public Point AufprallTangentenKontrollpunkt1 { get; }
        public Point AufprallTangentenKontrollpunkt2 { get; }
        public Point AufprallWinkelPunkt1            { get; }
        public Point AufprallWinkelPunkt2            { get; }
        public Size  AufprallWinkelSize              { get; }

    }

}