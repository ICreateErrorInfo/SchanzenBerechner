namespace Berechnung {

    public class Flugbahn {

        Flugbahn(double absprungHöhe,
                 double absprungWinkel,
                 double absprungGeschwindigkeit,
                 double sprungWeite) {

            AbsprungHöhe            = absprungHöhe;
            AbsprungWinkelRad       = absprungWinkel;
            AbsprungGeschwindigkeit = absprungGeschwindigkeit;
            SprungWeite             = sprungWeite;

        }

        public double AbsprungHöhe            { get; }
        public double AbsprungWinkelRad       { get; }
        public double AbsprungGeschwindigkeit { get; }
        public double AbsprungWinkelDeg       => Berechne.ToDeg(AbsprungWinkelRad);
        public double SprungWeite             { get; }

        public double Y(double x) {
            return Berechne.Y(x, AbsprungGeschwindigkeit, AbsprungWinkelRad, AbsprungHöhe);
        }

        public static Flugbahn Create(
            Schanze schanze,
            double absprungGeschwindigkeit) {

            var weite = Berechne.Weite(absprungGeschwindigkeit, schanze.AbsprungwinkelRad);
            return new Flugbahn(schanze.Höhe, schanze.AbsprungwinkelRad, absprungGeschwindigkeit, weite);
        }

    }

}