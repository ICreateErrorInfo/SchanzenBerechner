using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ICreateErrorInfo.Shapes {

    public abstract class Arrow: Shape {

        public static readonly DependencyProperty ArrowAngleProperty =
            DependencyProperty.Register(name: nameof(ArrowAngle),
                                        propertyType: typeof(double),
                                        ownerType: typeof(Arrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: 45.0,
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ArrowAngle {
            set => SetValue(ArrowAngleProperty, value);
            get => (double) GetValue(ArrowAngleProperty);
        }

        public static readonly DependencyProperty ArrowLengthProperty =
            DependencyProperty.Register(name: nameof(ArrowLength),
                                        propertyType: typeof(double),
                                        ownerType: typeof(Arrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: 8.0,
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ArrowLength {
            set => SetValue(ArrowLengthProperty, value);
            get => (double) GetValue(ArrowLengthProperty);
        }

        public static readonly DependencyProperty ArrowedEndsProperty =
            DependencyProperty.Register(name: nameof(ArrowedEnds),
                                        propertyType: typeof(ArrowedEnds),
                                        ownerType: typeof(Arrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: ArrowedEnds.End,
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure));

        public ArrowedEnds ArrowedEnds {
            set => SetValue(ArrowedEndsProperty, value);
            get => (ArrowedEnds) GetValue(ArrowedEndsProperty);
        }

        public static readonly DependencyProperty IsArrowClosedProperty =
            DependencyProperty.Register(name: nameof(IsArrowClosed),
                                        propertyType: typeof(bool),
                                        ownerType: typeof(Arrow),
                                        typeMetadata: new FrameworkPropertyMetadata(
                                            defaultValue: false,
                                            flags: FrameworkPropertyMetadataOptions.AffectsMeasure));

        public bool IsArrowClosed {
            set => SetValue(IsArrowClosedProperty, value);
            get => (bool) GetValue(IsArrowClosedProperty);
        }

        protected sealed override Geometry DefiningGeometry {
            get {
                var pathGeometry = new PathGeometry();
                var pathFigure   = DefinePathFigure(out var lineSegment);

                if (lineSegment == null || pathFigure == null) {
                    return pathGeometry;
                }

                pathGeometry.Figures.Add(pathFigure);

                int count = lineSegment.Points.Count;

                if (count > 0) {
                    // Pfeil am Anfang der Polyline
                    if ((ArrowedEnds & ArrowedEnds.Start) == ArrowedEnds.Start) {
                        Point pt1 = pathFigure.StartPoint;
                        Point pt2 = lineSegment.Points[0];
                        pathGeometry.Figures.Add(CreateArrowPathFigure(pt2, pt1));
                    }

                    // Pfeil am Ende der Polyline
                    if ((ArrowedEnds & ArrowedEnds.End) == ArrowedEnds.End) {
                        Point pt1 = count == 1 ? pathFigure.StartPoint : lineSegment.Points[count - 2];
                        Point pt2 = lineSegment.Points[count - 1];
                        pathGeometry.Figures.Add(CreateArrowPathFigure(pt1, pt2));
                    }
                }

                return pathGeometry;
            }
        }

        protected abstract PathFigure DefinePathFigure(out PolyLineSegment polyLineSegment);

        PathFigure CreateArrowPathFigure(Point pt1, Point pt2) {

            var pathFigure = new PathFigure();

            var lineSegment = new PolyLineSegment();
            pathFigure.Segments.Add(lineSegment);

            Matrix matx = Matrix.Identity;
            Vector vect = pt1 - pt2;

            vect.Normalize();
            vect *= ArrowLength;

            matx.Rotate(ArrowAngle / 2);
            pathFigure.StartPoint = pt2 + vect * matx;
            lineSegment.Points.Add(pt2);

            matx.Rotate(-ArrowAngle);
            lineSegment.Points.Add(pt2 + vect * matx);
            pathFigure.IsClosed = IsArrowClosed;

            return pathFigure;
        }

    }

}