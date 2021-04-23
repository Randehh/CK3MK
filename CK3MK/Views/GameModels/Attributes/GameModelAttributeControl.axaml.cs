using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.GameModels.Attributes;
using System;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Views.GameModels.Attributes {
	public class GameModelAttributeControl : UserControl {

		private GameModelAttributeControlVM m_Control;

		public GameModelAttributeControl() {
			InitializeComponent();

			DataContextChanged += GameModelAttributeControl_DataContextChanged;
		}

		private void GameModelAttributeControl_DataContextChanged(object? sender, EventArgs e) {
			if (DataContext is GameModelAttributeControlVM) return;

			if (m_Control != null) {
				m_Control.Attribute = DataContext as IGameModelAttribute;
			} else {
				m_Control = new GameModelAttributeControlVM(this, DataContext as IGameModelAttribute);
			}

			DataContext = m_Control;
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
