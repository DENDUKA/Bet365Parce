using bet365WEBAPI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace bet365WEBAPI.Controllers
{
	/// <summary>
	/// Контрол - для парсинга bet365 LiveEvent
	/// </summary>
	public class ValuesController : ApiController
	{
		/// <summary>
		/// Получает HTML с 365, парситт и оттправляетт данные на сервер "Вилок"
		/// </summary>
		public void Post([FromBody] Bet365LiveFootbalEventModel someObject)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			someObject.Parce();

			someObject.SendToServer();

			sw.Stop();
			Debug.WriteLine($"Всё время : {sw.ElapsedMilliseconds}");
		}
	}
}
