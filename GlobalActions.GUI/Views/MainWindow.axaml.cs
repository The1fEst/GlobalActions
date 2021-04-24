using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.NodeSystem;
using GlobalActions.GUI.ViewModels;

namespace GlobalActions.GUI.Views {
    public class MainWindow : Window {
        private readonly MainWindowViewModel _vm;

        public MainWindow() {
            DataContext = _vm = new MainWindowViewModel();

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