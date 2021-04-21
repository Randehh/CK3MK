using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK3MK.Models {
	public class ModProject {
		public string DisplayName { get; set; }
		public string Name { get; set; }
		public string Version { get; set; }
		public List<string> Tags { get; set; }
		public string Path { get; set; }
	}
}
