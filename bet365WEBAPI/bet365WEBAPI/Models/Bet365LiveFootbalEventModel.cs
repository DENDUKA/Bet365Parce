using bet365WEBAPI.Entities;
using HtmlAgilityPack;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace bet365WEBAPI.Models
{
	/// <summary>
	/// Класс - модель для получения и парсинга в нужный формат и отправлки данных на сервер для обсета вилок
	/// </summary>
	public class Bet365LiveFootbalEventModel
	{
		public int Id { get; set; }
		public string Html { get; set; }
		private Bet365FootballLiveEvent parsedEvent;
		private Stopwatch sw = new Stopwatch();

		public Bet365FootballLiveEvent Parce()
		{
			parsedEvent = new Bet365FootballLiveEvent();

			if (String.IsNullOrEmpty(Html))
			{
				return parsedEvent;
			}


			Debug.WriteLine("Парсинг начало");

			sw.Start();

			var doc = new HtmlDocument();
			doc.LoadHtml(Html);

			foreach (var node in doc.DocumentNode.ChildNodes.Where(x => x.Attributes["class"].Value == "sip-MarketGroup "))
			{
				var text = node?.ChildNodes[0]?.ChildNodes[0]?.InnerText;

				switch (text)
				{
					case "Fulltime Result":
						{
							FulltimeResult(node.ChildNodes[1]);

							break;
						};
					case "Final Score":
						{
							FinalScore(node.ChildNodes[1]);

							break;
						}
					case "Double Chance":
						{
							DoubleChance(node.ChildNodes[1]);

							break;
						}
					default:
						{
							break;
						}
				};
			}

			sw.Stop();

			Debug.WriteLine($"Парсинг конец {sw.ElapsedMilliseconds}");

			return parsedEvent;
		}

		/// <summary>
		/// Парсинг блока Двойного шанса
		/// </summary>
		/// <param name="node"></param>
		private void DoubleChance(HtmlNode node)
		{
			var bidNodes = node.ChildNodes[0].ChildNodes[0];
			float result = 0;

			//Проверка на доступностть ставки на событие
			if (bidNodes?.ChildNodes[0] != null && !bidNodes.ChildNodes[0].HasClass("gl-Participant_Suspended"))
				if (float.TryParse(bidNodes?.ChildNodes[0]?.ChildNodes[1]?.InnerHtml, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
				{
					parsedEvent.DoubleChanceT1D = result;
				}

			if (bidNodes?.ChildNodes[1] != null && !bidNodes.ChildNodes[1].HasClass("gl-Participant_Suspended"))
				if (float.TryParse(bidNodes?.ChildNodes[1]?.ChildNodes[1]?.InnerHtml, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
				{
					parsedEvent.DoubleChanceT2D = result;
				}

			if (bidNodes?.ChildNodes[2] != null && !bidNodes.ChildNodes[2].HasClass("gl-Participant_Suspended"))
				if (float.TryParse(bidNodes?.ChildNodes[2]?.ChildNodes[1]?.InnerHtml, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
				{
					parsedEvent.DoubleChanceT1T2 = result;
				}
		}

		/// <summary>
		/// Парсин блока итогового счёта
		/// </summary>
		/// <param name="node"></param>
		public void FinalScore(HtmlNode node)
		{
			var bidNodes = node.ChildNodes[0];

			FinalScoreColumn(bidNodes.ChildNodes[0], parsedEvent.finalScores.Team1);
			FinalScoreColumn(bidNodes.ChildNodes[1], parsedEvent.finalScores.Draw);
			FinalScoreColumn(bidNodes.ChildNodes[2], parsedEvent.finalScores.Team2);
		}

		public void FinalScoreColumn(HtmlNode node, Dictionary<string, float> res)
		{
			foreach (var n in node.ChildNodes)
			{
				if (n.ChildNodes.Count == 2)
				{
					if (!n.HasClass("gl-ParticipantCentered_Suspended"))
						if (float.TryParse(n?.ChildNodes[1]?.InnerText, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
						{
							res.Add(n.ChildNodes[0].InnerText, result);
						}
				}
			}
		}

		/// <summary>
		/// Парсинг блока итогового счёта
		/// </summary>
		/// <param name="node"></param>
		public void FulltimeResult(HtmlNode node)
		{
			var bidNodes = node.ChildNodes[0].ChildNodes[0];
			float result = 0;

			parsedEvent.Team1 = bidNodes?.ChildNodes[0]?.ChildNodes[0]?.InnerHtml;
			parsedEvent.Team2 = bidNodes?.ChildNodes[2]?.ChildNodes[0]?.InnerHtml;

			//Проверка на доступность ставки на событие
			if (bidNodes?.ChildNodes[0] != null && !bidNodes.ChildNodes[0].HasClass("gl-Participant_Suspended"))
				if (float.TryParse(bidNodes?.ChildNodes[0]?.ChildNodes[1]?.InnerHtml, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
				{
					parsedEvent.Win1 = result;
				}

			if (bidNodes?.ChildNodes[1] != null && !bidNodes.ChildNodes[1].HasClass("gl-Participant_Suspended"))
				if (float.TryParse(bidNodes?.ChildNodes[1]?.ChildNodes[1]?.InnerHtml, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
				{
					parsedEvent.Draw = result;
				}

			if (bidNodes?.ChildNodes[2] != null && !bidNodes.ChildNodes[2].HasClass("gl-Participant_Suspended"))
				if (float.TryParse(bidNodes?.ChildNodes[2]?.ChildNodes[1]?.InnerHtml, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
				{
					parsedEvent.Win2 = result;
				}
		}

		/// <summary>
		/// Отправка на сервер (для поиска вилок)
		/// В данном случае сам себе, только на другой контрол
		/// </summary>
		internal void SendToServer()
		{
			if (parsedEvent != null)
			{
				string data = JsonConvert.SerializeObject(parsedEvent);

				var client = new RestClient(@"https://localhost:44344/api/Bet365/FootballLiveEvent");
				var request = new RestRequest("", Method.Post);
				request.RequestFormat = DataFormat.Json;
				request.AddBody(data);
				client.Execute(request);	
			}
		}
	}
}