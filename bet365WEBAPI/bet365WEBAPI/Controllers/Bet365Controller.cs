using bet365WEBAPI.Entities;
using System.Web.Http;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace bet365WEBAPI.Controllers
{
    /// <summary>
    /// Конттрол заглушка - имитация сервера для обсчёта Вилок
    /// </summary>
	public class Bet365Controller : ApiController
	{
		// POST api/Bet365Controller/FootballLiveEvent
        /// <summary>
        /// Принимает данный из BET365 LinveEvent
        /// </summary>
        /// <param name="footballEvennt"></param>
		[HttpPost]
        public void FootballLiveEvent([FromBody] Bet365FootballLiveEvent footballEvennt)
        {
            
        }
    }
}