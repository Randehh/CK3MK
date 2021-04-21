using CK3MK.Models;
using CK3MK.Utilities;
using System;
using System.IO;

namespace CK3MK.Services {
	public class ProjectService {

		private ModProject m_CurrentProject = null;
		public ModProject CurrentProject => m_CurrentProject;

		public event EventHandler<ModProject> OnOpenProject;

		public void RegisterNewProject(ModProject project) {
			string fileName = project.Name + ".ck3mod";
			AssetsUtil.SerializeToJson(project, project.Path, fileName);
		}

		public void OpenProject(string projectPath) {
			ModProject project = AssetsUtil.DeserializeFromJson<ModProject>(projectPath);
			OpenProject(project);
		}

		public void OpenProject(ModProject project) {
			m_CurrentProject = project;
			ServiceLocator.GlobalSettingsService.AddRecentProjectPath(GetProjectModFilePath(project));
			OnOpenProject.Invoke(this, project);
		}

		public string GetProjectModFilePath(ModProject project) {
			string fileName = project.Name + ".ck3mod";
			return Path.Combine(project.Path, fileName);
		}
	}
}
