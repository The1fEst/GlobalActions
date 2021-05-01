using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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
		}
	}
}