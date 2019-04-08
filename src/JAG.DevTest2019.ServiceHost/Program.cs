using JAG.DevTest2019.ServiceHost.WebAPI;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;

namespace JAG.DevTest2019.ServiceHost
{
  class Program
  {
    private static IDisposable _WebApiServiceHost;

    private static Timer timer = new Timer(60000) { Enabled = true };

    private static TimeSpan lastCheck = new TimeSpan(0, 1, 0);

    static void Main(string[] args)
    {
      timer.Elapsed += Timer_Elapsed;
      Console.WriteLine($"Starting WebAPI");
      int apiPort = 8099;
      string apiHost = "http://localhost";
      string url = $"{apiHost}:{apiPort}";

      _WebApiServiceHost = WebApp.Start<WebApiStartup>(url);
      Console.WriteLine($"WebAPI hosted on {url}");


      Console.WriteLine($"Press enter to exit");
      Console.ReadLine();
    }

    private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {

      try
      {
        string connectionString = "Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = JAG2019; Data Source = TRACKDS1G019337";
        using (var sqlConnection = new SqlConnection(connectionString))
        using (var sqlCommand = new SqlCommand() { Connection = sqlConnection })
        {

          DateTime previousCheckTime = DateTime.Now.Subtract(lastCheck);
          sqlCommand.CommandText = $"SELECT COUNT(*) FROM Lead WHERE [ReceivedDateTime] >= '{previousCheckTime}'";
          sqlCommand.Connection.Open();

          int totalNewLeads = Convert.ToInt32(sqlCommand.ExecuteScalar());

          Console.WriteLine($"Total new leads between {DateTime.Now} and  {previousCheckTime} = {totalNewLeads}");
        }
      }
      catch (Exception)
      {

      }

    }
  }
}
