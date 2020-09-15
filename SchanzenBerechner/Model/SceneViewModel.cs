using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace SchanzenBerechner.Model {

    public class SceneViewModel: ViewModel {

        readonly ObservableCollection<SettingViewModel> _settings;

        public SceneViewModel() {
            _settings                   =  new ObservableCollection<SettingViewModel>();
            _settings.CollectionChanged += OnSettingCollectionChanged;
        }

        public ObservableCollection<SettingViewModel> Settings => _settings;

        private void OnSettingCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            UpdateScale();
        }

        void UpdateScale() {

            if (Settings.Count == 0) {
                return;
            }

            var maxWidth = Settings.Select(s => s.CalculateNaturalSize().Width)
                                   .Max();

            var scale = 1000 / maxWidth;

            foreach (var setting in Settings) {
                setting.Rescale(scale);
            }
        }

    }

}