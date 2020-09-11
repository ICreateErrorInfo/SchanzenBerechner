using Berechnung;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using SchanzenBerechner.Model;

namespace SchanzenBerechner {

    public partial class SchanzenVisualisierung: UserControl {

        readonly SchanzenVisualisierungViewModel _viewModel;

        public SchanzenVisualisierung() {

            InitializeComponent();

            _viewModel = new SchanzenVisualisierungViewModel();

            BindToViewModel(RenderMetricsProperty, nameof(SchanzenVisualisierungViewModel.RenderMetrics));

            DataContext = _viewModel;

            void BindToViewModel(DependencyProperty dependencyProperty, string viewModelPropertyName) {
                SetBinding(
                    dependencyProperty, 
                    new Binding(viewModelPropertyName) {
                        Source = _viewModel,
                    });
            }
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
            me.InvalidateModel();
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
            me.InvalidateModel();

        }

        public bool RenderMetrics {
            get => (bool) GetValue(RenderMetricsProperty);
            set => SetValue(RenderMetricsProperty, value);
        }

        public static readonly DependencyProperty RenderMetricsProperty =
            DependencyProperty.Register(
                name: nameof(RenderMetrics),
                propertyType: typeof(bool),
                ownerType: typeof(SchanzenVisualisierung),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: false,
                    flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender) {
                    BindsTwoWayByDefault = true
                }
            );

        void InvalidateModel() {
            _viewModel.Invalidate(Schanze, Flugbahn);

        }

    }

}