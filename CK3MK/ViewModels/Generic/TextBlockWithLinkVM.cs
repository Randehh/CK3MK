using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Runtime.InteropServices;

namespace CK3MK.ViewModels.Generic {
	public class TextBlockWithLinkVM : ViewModelBase {

		private string m_TextBeforeLink;
		public string TextBeforeLink {
			get => m_TextBeforeLink;
			set => this.RaiseAndSetIfChanged(ref m_TextBeforeLink, value);
		}
		public string LinkText { get; set; }
		public string Link { get; set; }

		public ReactiveCommand<Unit,Unit> LinkCommand { get; set; }

		public TextBlockWithLinkVM(string textBeforeLink, string linkText, string link) {
			TextBeforeLink = textBeforeLink;
			LinkText = linkText;
			Link = link;

			LinkCommand = ReactiveCommand.Create(OnLinkClick);
		}

		public void OnLinkClick() {
			OpenUrl(Link);
		}

        private void OpenUrl(string url) {
            try {
                Process.Start(url);
            } catch {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                    Process.Start("xdg-open", url);
                } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    Process.Start("open", url);
                } else {
                    throw;
                }
            }
        }
    }
}
