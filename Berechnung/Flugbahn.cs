namespace Berechnung {

    public class Flugbahn {

        private readonly double _absprungHöhe;
        private readonly double _sprungWeite;
        private readonly double _scheitelpunktX;
        private readonly double _scheitelpunktY;

        Flugbahn(double absprungHöhe,
                 Winkel absprungWinkel,
                 double absprungGeschwindigkeit,
                 double sprungWeite,
                 double scheitelpunktX,
                 double scheitelpunktY,
                 double scale) {

            _absprungHöhe           = absprungHöhe;
            AbsprungWinkel          = absprungWinkel;
            AbsprungGeschwindigkeit = absprungGeschwindigkeit;
            _sprungWeite            = sprungWeite;
            _scheitelpunktY         = scheitelpunktY;
            _scheitelpunktX         = scheitelpunktX;
            Scale                   = scale;

        }

        public double AbsprungHöhe   => _absprungHöhe   * Scale;
        public double SprungWeite    => _sprungWeite    * Scale;
        public double ScheitelpunktX => _scheitelpunktX * Scale;
        public double ScheitelpunktY => _scheitelpunktY * Scale;

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
                absprungHöhe: _absprungHöhe,
                absprungWinkel: AbsprungWinkel,
                absprungGeschwindigkeit: AbsprungGeschwindigkeit,
                sprungWeite: _sprungWeite,
                scheitelpunktX: _scheitelpunktX,
                scheitelpunktY: _scheitelpunktY,
                scale: scale);
        }

        public static Flugbahn Create(
            Schanze schanze,
            double absprungGeschwindigkeit) {

            var weite = Wurfparabel.Weite(
                v0: absprungGeschwindigkeit,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad);

            var scheitelpunktX = Wurfparabel.ScheitelpunktX(
                v0: absprungGeschwindigkeit,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad
            );

            var höhe = Wurfparabel.ScheitelpunktY(
                v0: absprungGeschwindigkeit,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad);

            return new Flugbahn(
                absprungHöhe: schanze.Höhe,
                absprungWinkel: schanze.Absprungwinkel,
                absprungGeschwindigkeit: absprungGeschwindigkeit,
                sprungWeite: weite,
                scheitelpunktX: scheitelpunktX,
                scheitelpunktY: höhe,
                scale: 1);
        }

    }

}