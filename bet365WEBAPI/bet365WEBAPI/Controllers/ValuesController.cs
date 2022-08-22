using bet365WEBAPI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;

namespace bet365WEBAPI.Controllers
{
	public class ValuesController : ApiController
	{
		// GET api/values
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		public void Post([FromBody] Bet365LiveFootbalEventModel someObject)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			someObject.Parce();

			someObject.SendToServer();

			sw.Stop();
			Debug.WriteLine($"Всё время : {sw.ElapsedMilliseconds}");
		}

		// PUT api/values/5
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}
