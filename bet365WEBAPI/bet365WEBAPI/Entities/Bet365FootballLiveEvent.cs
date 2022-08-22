namespace bet365WEBAPI.Entities
{
	public class Bet365FootballLiveEvent
	{
		public string Team1 { get; set; }
		public string Team2 { get; set; }
		public float Win1 { get; set; }
		public float Win2 { get; set; }
		public float Draw { get; set; }
		public float DoubleChanceT1D { get; set; }
		public float DoubleChanceT2D { get; set; }
		public float DoubleChanceT1T2 { get; set; }

		public FinalScores finalScores = new FinalScores();
	}
}