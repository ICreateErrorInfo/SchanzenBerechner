namespace Berechnung {

    public class Flugbahn {

        private readonly double _absprungHöhe;
        private readonly double _sprungWeite;

        Flugbahn(double absprungHöhe,
                 double absprungWinkel,
                 double absprungGeschwindigkeit,
                 double sprungWeite, double scale) {

            _absprungHöhe           = absprungHöhe;
            AbsprungWinkelRad       = absprungWinkel;
            AbsprungGeschwindigkeit = absprungGeschwindigkeit;
            _sprungWeite            = sprungWeite;
            Scale                   = scale;

        }

        public double AbsprungHöhe => _absprungHöhe * Scale;
        public double SprungWeite  => _sprungWeite  * Scale;

        public double AbsprungGeschwindigkeit { get; }
        public double AbsprungWinkelRad       { get; }
        public double AbsprungWinkelDeg       => Berechne.ToDeg(AbsprungWinkelRad);

        public double Scale { get; }

        public double Y(double x) {
            return Berechne.Y(x, AbsprungGeschwindigkeit, AbsprungWinkelRad, AbsprungHöhe);
        }

        public Flugbahn WithScale(double scale) {
            return new Flugbahn(
                absprungHöhe: AbsprungHöhe,
                absprungWinkel: AbsprungWinkelRad,
                absprungGeschwindigkeit: AbsprungGeschwindigkeit,
                sprungWeite: SprungWeite,
                scale: scale);
        }

        public static Flugbahn Create(
            Schanze schanze,
            double absprungGeschwindigkeit) {

            var weite = Berechne.Weite(
                v0: absprungGeschwindigkeit,
                alpha: schanze.AbsprungwinkelRad);

            return new Flugbahn(
                absprungHöhe: schanze.Höhe,
                absprungWinkel: schanze.AbsprungwinkelRad,
                absprungGeschwindigkeit: absprungGeschwindigkeit,
                sprungWeite: weite,
                scale: 1);
        }

    }

}