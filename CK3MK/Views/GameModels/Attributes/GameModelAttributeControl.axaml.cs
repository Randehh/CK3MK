using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.GameModels.Attributes;
using System;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Views {
	public class GameModelAttributeControl : UserControl {
		public GameModelAttributeControl() {
			InitializeComponent();

			DataContextChanged += GameModelAttributeControl_DataContextChanged;
		}

		private void GameModelAttributeControl_DataContextChanged(object? sender, EventArgs e) {
			if (DataContext is GameModelAttributeControlVM) return;
			DataContext = new GameModelAttributeControlVM(this, DataContext as IGameModelAttribute);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
