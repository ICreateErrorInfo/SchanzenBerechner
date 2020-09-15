using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

using Berechnung;
using Berechnung.Einheiten;

namespace SchanzenBerechner.Model {

    public class FlugbahnViewModel: ViewModel {

        readonly Flugbahn _flugbahn;

        public FlugbahnViewModel(Schanze schanze, Flugbahn flugbahn) {

            _flugbahn = flugbahn??throw new ArgumentNullException(nameof(flugbahn));

            var punkte   = new List<Point>();
            var segments = 100;
            var step     = (int) flugbahn.SprungWeite.Meter / segments;

            // Horizontaler Offset, wenn Schanze vorhanden
            var x0 = schanze?.Länge ?? Länge.Empty;

            // Absprungpunkt
            var x = Länge.Empty;
            var y = flugbahn.Y(x);

            StartPunkt = ToPoint(x0 + x, y);
            punkte.Add(StartPunkt);

            // Bahnpunkte
            for (int i = 1; i <= segments; i++) {

                x = Länge.FromMeter(i * step);
                y = flugbahn.Y(x);

                punkte.Add(ToPoint(x0 + x, y));
            }

            // Aufprallpunkt
            x = flugbahn.SprungWeite;
            y = flugbahn.Y(x);

            EndPunkt = ToPoint(x0 + x, y);
            punkte.Add(EndPunkt);

            Punkte = new PointCollection(punkte);

            Scheitelpunkt = ToPoint(x0 + flugbahn.ScheitelpunktX, flugbahn.ScheitelpunktY);

            var winkelSize     = flugbahn.Scale * 0.2;
            var tangentenLänge = flugbahn.Scale;
            // Absprung
            var m = Matrix.Identity;
            m.Rotate(flugbahn.AbsprungWinkel.Deg);

            var v = new Vector(tangentenLänge, 0);

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

        public Flugbahn Metrics => _flugbahn.WithScale(1);

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

        static Point ToPoint(Länge x, Länge y) => new Point(x.Meter, y.Meter);

    }

}