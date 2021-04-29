using Avalonia.Controls;
using CK3MK.Views.RootPages;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace CK3MK.ViewModels.RootPages {
	public class PageHistoryControlVM : ViewModelBase {

		private PageHistoryControl m_Control;
		private StackPanel m_HistoryStackPanel;

		private Action<int> m_OnHistoryButtonPressed = delegate { };

		public PageHistoryControlVM(PageHistoryControl control) {
			m_Control = control;
			m_HistoryStackPanel = m_Control.FindControl<StackPanel>("HistoryStackPanel");
		}

		public void RegisterActions(Action<int> OnHistoryButtonPressed) {
			m_OnHistoryButtonPressed += OnHistoryButtonPressed;
		}

		public void SetHistoryButtons(Dictionary<int, string> buttons) {
			m_HistoryStackPanel.Children.Clear();
			
			foreach(KeyValuePair<int, string> pair in buttons) {
				m_HistoryStackPanel.Children.Add(CreateHistoryButton(pair.Key, pair.Value));

				if (pair.Key != buttons.Count) {
					m_HistoryStackPanel.Children.Add(new TextBlock() { Text = "->", VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
				}
			}
		}

		private Button CreateHistoryButton(int index, string name) {
			return new Button() {
				Content = name,
				Command = ReactiveCommand.Create(m_OnHistoryButtonPressed),
				CommandParameter = index,
			};
		}
	}
}
