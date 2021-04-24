using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GlobalActions.GUI.Views {
    public class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);

            Task.Run(() => {
                while (HotKeyHandler.GetHotKey<int>(out var key)) {
                    if (key != default) {
                        ScriptsList.Instance.Toggle(key);
                    }
                }
            });
        }
    }
}