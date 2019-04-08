using JAG.DevTest2019.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JAG.DevTest2019.Host.Controllers
{
  public class LeadController : Controller
  {
    public ActionResult Index()
    {
      return View(new LeadViewModel());
    }

    public async Task<ActionResult> SubmitLead(LeadViewModel model)
    {
      //TODO: 6. Call the WebAPI service here & pass results to UI
      var result = await HttpPost(model);
    
      model.Results = result;

      return View("Index", model);
    }
    private static async Task<LeadResultViewModel>HttpPost(LeadViewModel lead)
    {

      HttpResponseMessage responseMessage;
      using (var client = new HttpClient())
      {

        client.BaseAddress = new Uri("http://localhost:8099");
        responseMessage = await client.PostAsJsonAsync("api/Lead", lead);
        responseMessage.EnsureSuccessStatusCode();

      }

      return await responseMessage.Content.ReadAsAsync<LeadResultViewModel>();
    }
  }
}