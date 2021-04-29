using Avalonia.Controls;
using CK3MK.Views.RootPages;
using System.Collections.Generic;
using System.Linq;

namespace CK3MK.ViewModels.RootPages {
	public class RootFlowPageVM : ViewModelBase {

		public static RootFlowPageVM MainFlowPage { get; private set; }

		private RootFlowPage m_Control;
		private Stack<PageFlowInstance> m_PageFlowInstances = new Stack<PageFlowInstance>();
		private Dictionary<int, string> SimplePageFlow {
			get {
				Dictionary<int, string> simpleFlow = new Dictionary<int, string>();
				for(int i = m_PageFlowInstances.Count - 1; i >= 0; i--) {
					PageFlowInstance instance = m_PageFlowInstances.ElementAt(i);
					simpleFlow.Add(instance.index, instance.name);
				}
				return simpleFlow;
			}
		}

		private PageHistoryControl m_PageHistoryControl;
		private Grid m_DockGrid;

		public RootFlowPageVM(RootFlowPage control) {
			m_Control = control;
			MainFlowPage = this;

			m_DockGrid = m_Control.FindControl<Grid>("DockGrid");
			m_PageHistoryControl = m_Control.FindControl<PageHistoryControl>("PageHistoryControl");
			m_PageHistoryControl.GetViewModel().RegisterActions(NavigateTo);

			//Push home page
			PushControl("Home", new HomePage());
		}

		public void PushControl(string name, UserControl control) {
			PageFlowInstance nextPage = new PageFlowInstance() {
				index = m_PageFlowInstances.Count + 1,
				name = name,
				control = control
			};
			m_PageFlowInstances.Push(nextPage);
			UpdateView();
		}

		public void PopControl() {
			if(m_PageFlowInstances.Count <= 1) {
				return;
			}
			m_PageFlowInstances.Pop();
			UpdateView();
		}

		public void NavigateTo(int index) {
			while(m_PageFlowInstances.Count > 1) {
				if (m_PageFlowInstances.Peek().index == index) {
					return;
				}
				PopControl();
			}
		}

		private void UpdateView() {
			m_PageHistoryControl.GetViewModel().SetHistoryButtons(SimplePageFlow);
			m_DockGrid.Children.Clear();
			m_DockGrid.Children.Add(m_PageFlowInstances.Peek().control);
		}

		private struct PageFlowInstance {
			public int index;
			public string name;
			public UserControl control;
		}
	}
}
