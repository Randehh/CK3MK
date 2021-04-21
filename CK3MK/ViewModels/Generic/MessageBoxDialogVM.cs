using CK3MK.Views.Generic;
using ReactiveUI;
using System.Reactive;

namespace CK3MK.ViewModels.Generic {
	public class MessageBoxDialogVM : ViewModelBase {

		private MessageBoxDialog m_Window;

		private string m_MessageText = "";
		public string MessageText {
			get => m_MessageText;
			set => this.RaiseAndSetIfChanged(ref m_MessageText, value);
		}

		public ReactiveCommand<Unit, Unit> OnCommand_Ok { get; }

		public MessageBoxDialogVM(MessageBoxDialog window) {
			m_Window = window;

			OnCommand_Ok = ReactiveCommand.Create(CloseDialog);
		}

		private void CloseDialog() {
			m_Window.Close();
		}
	}
}
