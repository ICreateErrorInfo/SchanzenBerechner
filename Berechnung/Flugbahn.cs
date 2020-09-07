﻿namespace Berechnung {

    public class Flugbahn {

        private readonly double _absprungHöhe;
        private readonly double _sprungWeite;
        private readonly double _sprungHöhe;

        Flugbahn(double absprungHöhe,
                 Winkel absprungWinkel,
                 double absprungGeschwindigkeit,
                 double sprungWeite,
                 double sprungHöhe,
                 double scale) {

            _absprungHöhe           = absprungHöhe;
            AbsprungWinkel          = absprungWinkel;
            AbsprungGeschwindigkeit = absprungGeschwindigkeit;
            _sprungWeite            = sprungWeite;
            _sprungHöhe             = sprungHöhe;
            Scale                   = scale;

        }

        public double AbsprungHöhe => _absprungHöhe * Scale;
        public double SprungWeite  => _sprungWeite  * Scale;
        public double SprungHöhe   => _sprungHöhe   * Scale;

        public double AbsprungGeschwindigkeit { get; }
        public Winkel AbsprungWinkel          { get; }

        public double Scale { get; }

        public double Y(double x) {
            return Wurfparabel.Y(
                x: x / Scale,
                v0: AbsprungGeschwindigkeit,
                alpha: AbsprungWinkel.Rad,
                y0: _absprungHöhe) * Scale;
        }

        public Flugbahn WithScale(double scale) {
            return new Flugbahn(
                absprungHöhe: AbsprungHöhe,
                absprungWinkel: AbsprungWinkel,
                absprungGeschwindigkeit: AbsprungGeschwindigkeit,
                sprungWeite: SprungWeite,
                sprungHöhe: SprungHöhe,
                scale: scale);
        }

        public static Flugbahn Create(
            Schanze schanze,
            double absprungGeschwindigkeit) {

            var weite = Wurfparabel.Weite(
                v0: absprungGeschwindigkeit,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad);

            var höhe = Wurfparabel.Höhe(
                v0: absprungGeschwindigkeit,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad);

            return new Flugbahn(
                absprungHöhe: schanze.Höhe,
                absprungWinkel: schanze.Absprungwinkel,
                absprungGeschwindigkeit: absprungGeschwindigkeit,
                sprungWeite: weite,
                sprungHöhe: höhe,
                scale: 1);
        }

    }

}