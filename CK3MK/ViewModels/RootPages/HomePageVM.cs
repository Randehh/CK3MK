using CK3MK.Services;
using CK3MK.Utilities;
using CK3MK.ViewModels.Generic;
using CK3MK.Views.RootPages;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace CK3MK.ViewModels.RootPages {
	public class HomePageVM : ViewModelBase {

		private HomePage m_Control;
		private RootFlowPageVM m_FlowPage;

		private string LinkModdingMain => "https://ck3.paradoxwikis.com/Modding";
		private string LinkDiscordServer => @"https://discord.gg/ck3";
		private string LinkDiscordCommunity => @"https://discord.gg/apEvxDZ";
		private string LinkInterfaceModding => @"https://ck3.paradoxwikis.com/Interface";
		private string LinkModStructure => @"https://ck3.paradoxwikis.com/Mod_structure#Creating_initial_files";
		private string LinkFilesFromMSStore => @"https://ck3.paradoxwikis.com/Modding#Extracting_files_From_Microsoft_Store_version";

		private TextBlockWithLinkVM m_LinkVMModdingMain;
		public TextBlockWithLinkVM LinkVMModdingMain {
			get => m_LinkVMModdingMain;
			set => this.RaiseAndSetIfChanged(ref m_LinkVMModdingMain, value);
		}

		private TextBlockWithLinkVM m_LinkVMDiscordServer;
		public TextBlockWithLinkVM LinkVMDiscordServer {
			get => m_LinkVMDiscordServer;
			set => this.RaiseAndSetIfChanged(ref m_LinkVMDiscordServer, value);
		}

		private TextBlockWithLinkVM m_LinkVMDiscordServerCommunity;
		public TextBlockWithLinkVM LinkVMDiscordServerCommunity {
			get => m_LinkVMDiscordServerCommunity;
			set => this.RaiseAndSetIfChanged(ref m_LinkVMDiscordServerCommunity, value);
		}

		public HomePageVM(HomePage control) {
			m_Control = control;

			LinkVMModdingMain = new TextBlockWithLinkVM("Official modding page containing info on setup", "Go to website", LinkModdingMain);
			LinkVMDiscordServer = new TextBlockWithLinkVM("Official CK3 Discord server containing a modding channel", "Join server", LinkDiscordServer);
			LinkVMDiscordServerCommunity = new TextBlockWithLinkVM("Mod Coop Discord server dedicated to modding", "Join server", LinkDiscordCommunity);
		}

		private void PushCharacterControl() {
			RootFlowPageVM.MainFlowPage.PushControl("Characters", GameModelViewCreator.CreateFullViewCategorised(
				new ObservableCollection<string>(ServiceLocator.ModelCacheService.CharactersByCountry.Keys),
				(category) => { return ServiceLocator.ModelCacheService.CharactersByCountry[category].GetObservableCollection(); }
				));
		}

		private void PushDynastyControl() {
			RootFlowPageVM.MainFlowPage.PushControl("Dynasties", GameModelViewCreator.CreateFullView(ServiceLocator.ModelCacheService.Dynasties.GetObservableCollection()));
		}

		private void PushDynastyHouseControl() {
			RootFlowPageVM.MainFlowPage.PushControl("Dynasty houses", GameModelViewCreator.CreateFullView(ServiceLocator.ModelCacheService.DynastyHouses.GetObservableCollection()));
		}
	}
}
