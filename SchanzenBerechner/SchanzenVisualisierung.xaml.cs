using System.Windows;
using System.Windows.Controls;

using SchanzenBerechner.Model;

namespace SchanzenBerechner {

    public partial class SchanzenVisualisierung: UserControl {

        public SchanzenVisualisierung() {

            InitializeComponent();

            ViewModel = new SettingViewModel();

        }

        public SettingViewModel ViewModel {
            get => (SettingViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                name: nameof(ViewModel),
                propertyType: typeof(SettingViewModel),
                ownerType: typeof(SchanzenVisualisierung),
                typeMetadata: new FrameworkPropertyMetadata(
                    defaultValue: null,
                    flags: FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    propertyChangedCallback: OnViewModelChanged));

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var me = (SchanzenVisualisierung) d;
            me.DataContext = me.ViewModel;
        }

    }

}