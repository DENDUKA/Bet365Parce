using System.Collections.Generic;

namespace bet365WEBAPI.Entities
{
	public class FinalScores
	{
		public Dictionary<string, float> Team1 = new Dictionary<string, float>();
		public Dictionary<string, float> Draw = new Dictionary<string, float>();
		public Dictionary<string, float> Team2 = new Dictionary<string, float>();
	}
}