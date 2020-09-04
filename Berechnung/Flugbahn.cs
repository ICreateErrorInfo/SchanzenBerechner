namespace Berechnung {

    public class Flugbahn {

        private readonly double _absprungHöhe;
        private readonly double _sprungWeite;
        private readonly double _sprungHöhe;

        Flugbahn(double absprungHöhe,
                 double absprungWinkel,
                 double absprungGeschwindigkeit,
                 double sprungWeite,
                 double sprungHöhe,
                 double scale) {

            _absprungHöhe           = absprungHöhe;
            AbsprungWinkelRad       = absprungWinkel;
            AbsprungGeschwindigkeit = absprungGeschwindigkeit;
            _sprungWeite            = sprungWeite;
            _sprungHöhe             = sprungHöhe;
            Scale                   = scale;

        }

        public double AbsprungHöhe => _absprungHöhe * Scale;
        public double SprungWeite  => _sprungWeite  * Scale;
        public double SprungHöhe   => _sprungHöhe   * Scale;

        public double AbsprungGeschwindigkeit { get; }
        public double AbsprungWinkelRad       { get; }
        public double AbsprungWinkelDeg       => Winkel.ToDeg(AbsprungWinkelRad);

        public double Scale { get; }

        public double Y(double x) {
            return SchrägerWurf.Y(
                x: x / Scale,
                v0: AbsprungGeschwindigkeit,
                alpha: AbsprungWinkelRad,
                y0: _absprungHöhe) * Scale;
        }

        public Flugbahn WithScale(double scale) {
            return new Flugbahn(
                absprungHöhe: AbsprungHöhe,
                absprungWinkel: AbsprungWinkelRad,
                absprungGeschwindigkeit: AbsprungGeschwindigkeit,
                sprungWeite: SprungWeite,
                sprungHöhe: SprungHöhe,
                scale: scale);
        }

        public static Flugbahn Create(
            Schanze schanze,
            double absprungGeschwindigkeit) {

            var weite = SchrägerWurf.Weite(
                v0: absprungGeschwindigkeit,
                alpha: schanze.AbsprungwinkelRad);

            var höhe = SchrägerWurf.Höhe(
                v0: absprungGeschwindigkeit,
                alpha: schanze.AbsprungwinkelRad);

            // TODO Bug in Höhenberechnung...
            höhe += schanze.Höhe;

            return new Flugbahn(
                absprungHöhe: schanze.Höhe,
                absprungWinkel: schanze.AbsprungwinkelRad,
                absprungGeschwindigkeit: absprungGeschwindigkeit,
                sprungWeite: weite,
                sprungHöhe: höhe,
                scale: 1);
        }

    }

}