namespace Berechnung {

    public class Flugbahn {

        private readonly double _absprungHöhe;
        private readonly double _sprungWeite;
        private readonly double _scheitelpunktX;
        private readonly double _scheitelpunktY;

        Flugbahn(double absprungHöhe,
                 Winkel absprungWinkel,
                 Geschwindigkeit absprungGeschwindigkeit,
                 double sprungWeite,
                 double scheitelpunktX,
                 double scheitelpunktY,
                 Winkel aufprallWinkel,
                 Geschwindigkeit aufprallGeschwindigkeit,
                 double scale) {

            _absprungHöhe           = absprungHöhe;
            AbsprungWinkel          = absprungWinkel;
            AbsprungGeschwindigkeit = absprungGeschwindigkeit;
            _sprungWeite            = sprungWeite;
            _scheitelpunktY         = scheitelpunktY;
            _scheitelpunktX         = scheitelpunktX;
            Scale                   = scale;
            AufprallWinkel          = aufprallWinkel;
            AufprallGeschwindigkeit = aufprallGeschwindigkeit;

        }

        public double AbsprungHöhe   => _absprungHöhe   * Scale;
        public double SprungWeite    => _sprungWeite    * Scale;
        public double ScheitelpunktX => _scheitelpunktX * Scale;
        public double ScheitelpunktY => _scheitelpunktY * Scale;

        public Geschwindigkeit AbsprungGeschwindigkeit { get; }
        public Winkel          AbsprungWinkel          { get; }
        public Winkel          AufprallWinkel          { get; }
        public Geschwindigkeit AufprallGeschwindigkeit { get; }

        public double Scale { get; }

        public double Y(double x) {
            return Wurfparabel.Y(
                x: x / Scale,
                v0: AbsprungGeschwindigkeit.MProS,
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
                aufprallWinkel: AufprallWinkel,
                aufprallGeschwindigkeit: AufprallGeschwindigkeit,
                scale: scale);
        }

        public static Flugbahn Create(
            Schanze schanze,
            Geschwindigkeit absprungGeschwindigkeit) {

            var weite = Wurfparabel.Weite(
                v0: absprungGeschwindigkeit.MProS,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad);

            var scheitelpunktX = Wurfparabel.ScheitelpunktX(
                v0: absprungGeschwindigkeit.MProS,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad
            );

            var höhe = Wurfparabel.ScheitelpunktY(
                v0: absprungGeschwindigkeit.MProS,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad);

            var aufprallWinkel = Wurfparabel.AufprallWinkel(
                v0: absprungGeschwindigkeit.MProS,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad
            );

            var aufprallGeschwindigkeit = Wurfparabel.AufprallGeschwindigkeit(
                v0: absprungGeschwindigkeit.MProS,
                y0: schanze.Höhe,
                alpha: schanze.Absprungwinkel.Rad
            );

            return new Flugbahn(
                absprungHöhe: schanze.Höhe,
                absprungWinkel: schanze.Absprungwinkel,
                absprungGeschwindigkeit: absprungGeschwindigkeit,
                sprungWeite: weite,
                scheitelpunktX: scheitelpunktX,
                scheitelpunktY: höhe,
                aufprallWinkel: Winkel.FromRad(aufprallWinkel),
                aufprallGeschwindigkeit: Geschwindigkeit.FromMProS(aufprallGeschwindigkeit),
                scale: 1);
        }

    }

}