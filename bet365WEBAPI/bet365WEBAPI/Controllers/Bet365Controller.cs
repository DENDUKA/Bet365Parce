using bet365WEBAPI.Entities;
using System.Web.Http;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace bet365WEBAPI.Controllers
{
	public class Bet365Controller : ApiController
	{
		// POST api/Bet365Controller/FootballLiveEvent
		[HttpPost]
        public void FootballLiveEvent([FromBody] Bet365FootballLiveEvent footballEvennt)
        {
            
        }
    }
}