using Berechnung;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SchanzenBerechner {

    public partial class SchanzenVisualisierung: UserControl {

        public SchanzenVisualisierung() {
            InitializeComponent();
        }

        public Schanze Schanze {
            get => (Schanze) GetValue(SchanzenBerechnungProperty);
            set => SetValue(SchanzenBerechnungProperty, value);
        }

        public static readonly DependencyProperty SchanzenBerechnungProperty =
            DependencyProperty.Register(
                name: nameof(Schanze),
                propertyType: typeof(Schanze),
                ownerType: typeof(SchanzenVisualisierung),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null,
                    flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnSchanzeChanged));

        static void OnSchanzeChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e) {

            var me = (SchanzenVisualisierung) d;
            me.Refesh();

        }

        public Flugbahn Flugbahn {
            get => (Flugbahn) GetValue(FlugbahnProperty);
            set => SetValue(FlugbahnProperty, value);
        }

        public static readonly DependencyProperty FlugbahnProperty =
            DependencyProperty.Register(
                name: nameof(Flugbahn),
                propertyType: typeof(Flugbahn),
                ownerType: typeof(SchanzenVisualisierung),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null,
                    flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnFlugbahnChanged));

        static void OnFlugbahnChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e) {

            var me = (SchanzenVisualisierung) d;
            me.Refesh();

        }

        void Refesh() {

            SchanzenPath.Data = CreateSchanzenGeometry(Schanze);
            FlugbahnPath.Data = CreateFlugbahnGeometry(Flugbahn);

            var size = CalculateDesiredCanvasSize();
            Canvas.Width  = size.Width;
            Canvas.Height = size.Height;
        }

        Size CalculateDesiredCanvasSize() {
            var width  = .0;
            var height = .0;
            if (Schanze != null) {
                width  = Schanze.GetSchanzenEndPunkt().X;
                height = Schanze.GetSchanzenAbsprungPunkt().Y;
            }

            if (Flugbahn != null) {
                width += Flugbahn.SprungWeite;
                //height=Math TODO..
            }

            return new Size(width, height);
        }

        private static PathGeometry CreateSchanzenGeometry(Schanze schanze) {

            if (schanze == null) {
                return null;
            }

            var pathGeometry = new PathGeometry();
            var figure       = new PathFigure {IsClosed = true};

            figure.Segments.Add(new LineSegment {Point = schanze.GetSchanzenStartPunkt()});
            figure.Segments.Add(new LineSegment {Point = schanze.GetSchanzenEndPunkt()});
            figure.Segments.Add(new LineSegment {Point = schanze.GetSchanzenAbsprungPunkt()});
            figure.Segments.Add(new ArcSegment {
                Size  = schanze.GetSchanzenRadiusGröße(),
                Point = schanze.GetSchanzenStartPunkt(),

            });

            pathGeometry.Figures.Add(figure);
            return pathGeometry;
        }

        private static PathGeometry CreateFlugbahnGeometry(Flugbahn flugbahn) {

            if (flugbahn == null) {
                return null;
            }

            return null;
        }

    }

}