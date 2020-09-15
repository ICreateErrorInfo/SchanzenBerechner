using Berechnung.Einheiten;

namespace Berechnung {

    public class Flugbahn {

        private readonly Länge _absprungHöhe;
        private readonly Länge _sprungWeite;
        private readonly Länge _scheitelpunktX;
        private readonly Länge _scheitelpunktY;

        Flugbahn(Länge absprungHöhe,
                 Winkel absprungWinkel,
                 Geschwindigkeit absprungGeschwindigkeit,
                 Länge sprungWeite,
                 Länge scheitelpunktX,
                 Länge scheitelpunktY,
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

        public Länge AbsprungHöhe   => _absprungHöhe   * Scale;
        public Länge SprungWeite    => _sprungWeite    * Scale;
        public Länge ScheitelpunktX => _scheitelpunktX * Scale;
        public Länge ScheitelpunktY => _scheitelpunktY * Scale;

        public Geschwindigkeit AbsprungGeschwindigkeit { get; }
        public Winkel          AbsprungWinkel          { get; }
        public Winkel          AufprallWinkel          { get; }
        public Geschwindigkeit AufprallGeschwindigkeit { get; }

        public double Scale { get; }

        public Länge Y(double x) {
            return Länge.FromMeter(
                Wurfparabel.Y(
                    x: x / Scale,
                    v0: AbsprungGeschwindigkeit.MeterProSekunde,
                    alpha: AbsprungWinkel.Rad,
                    y0: _absprungHöhe.Meter) * Scale);
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
                v0: absprungGeschwindigkeit.MeterProSekunde,
                y0: schanze.Höhe.Meter,
                alpha: schanze.Absprungwinkel.Rad);

            var scheitelpunktX = Wurfparabel.ScheitelpunktX(
                v0: absprungGeschwindigkeit.MeterProSekunde,
                y0: schanze.Höhe.Meter,
                alpha: schanze.Absprungwinkel.Rad
            );

            var höhe = Wurfparabel.ScheitelpunktY(
                v0: absprungGeschwindigkeit.MeterProSekunde,
                y0: schanze.Höhe.Meter,
                alpha: schanze.Absprungwinkel.Rad);

            var aufprallWinkel = Wurfparabel.AufprallWinkel(
                v0: absprungGeschwindigkeit.MeterProSekunde,
                y0: schanze.Höhe.Meter,
                alpha: schanze.Absprungwinkel.Rad
            );

            var aufprallGeschwindigkeit = Wurfparabel.AufprallGeschwindigkeit(
                v0: absprungGeschwindigkeit.MeterProSekunde,
                y0: schanze.Höhe.Meter,
                alpha: schanze.Absprungwinkel.Rad
            );

            return new Flugbahn(
                absprungHöhe: schanze.Höhe,
                absprungWinkel: schanze.Absprungwinkel,
                absprungGeschwindigkeit: absprungGeschwindigkeit,
                sprungWeite: Länge.FromMeter(weite),
                scheitelpunktX: Länge.FromMeter(scheitelpunktX),
                scheitelpunktY: Länge.FromMeter(höhe),
                aufprallWinkel: Winkel.FromRad(aufprallWinkel),
                aufprallGeschwindigkeit: Geschwindigkeit.FromMeterProSekunde(aufprallGeschwindigkeit),
                scale: 1);
        }

    }

}