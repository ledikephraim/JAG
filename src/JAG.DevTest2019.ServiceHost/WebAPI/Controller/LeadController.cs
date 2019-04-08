using JAG.DevTest2019.Model;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace JAG.DevTest2019.LeadService.Controllers
{
  public class LeadController : ApiController
  {
    [HttpPost]
    [ResponseType(typeof(LeadResponse))]
    public HttpResponseMessage Post(Lead request)
    {


      //TODO: 7. Write the lead to the DB
      var result = writeToDB(request);

      LeadResponse response = new LeadResponse()
      {
        LeadId = result.Item1,
        IsDuplicate = result.Item2,
        IsSuccessful = result.Item3,
        IsCapped = result.Item4,
        Messages = result.Item5.Split(';'),
      };
      Console.WriteLine($"Lead received {request.FirstName} {request.Surname}");

      return Request.CreateResponse(HttpStatusCode.OK, response);
    }
    private Tuple<long, bool, bool, bool, string> writeToDB(Lead request)
    {
      var parametersNonQueryString = "";

      if (request.LeadParameters != null)
      {
        foreach (var param in request.LeadParameters)
        {
          parametersNonQueryString += $"INSERT INTO LeadParameter VALUES({param.Key},{param.Value})";

        }
      }
      long identity = 0;
      bool IsDuplicate = false;
      bool isSuccessful = false;
      bool isCapped = false;
      string message = "";

      string connectionString = "Integrated Security = SSPI; Persist Security Info = False; Initial Catalog = JAG2019; Data Source = TRACKDS1G019337";
      using (var sqConnection = new SqlConnection(connectionString))
      using (var sqlCommand = new SqlCommand())
      {
        sqlCommand.Connection = sqConnection;


        var commandText =
          $"DECLARE @Identity INT = NULL;" +
          $"DECLARE @IsDuplicate BIT = 0;" +
          $"DECLARE @IsSuccessful BIT = 1;" +
          $"DECLARE @IsCapped BIT = 0;" +
          $"DECLARE @TrackingCodeTotalLeads INT = 0;" +
          $"DECLARE @Message NVARCHAR(MAX) = NULL;" +
          $"IF EXISTS(SELECT 1 FROM Lead WHERE ContactNumber ='{request.ContactNumber}' OR Email = '{request.EmailAddress}') " +
          $"BEGIN " +
          $"SET @IsDuplicate = 1 ;" +
          $"SET @IsSuccessful = 0;" +
          $"SET @Message = 'duplicate on [email/contact]';" +
          $"SELECT @TrackingCodeTotalLeads =  COUNT(*) FROM Lead WHERE TrackingCode = '{request.TrackingCode}';" +
          "IF(@TrackingCodeTotalLeads > 10) " +
          "BEGIN " +
          "SET @IsCapped = 1;" +
          $"SET @Message = @Message + ';' + CONVERT(NVARCHAR, @TrackingCodeTotalLeads) + ' leads recieved today, {request.TrackingCode} is capped';" +
          "END " +
          "UPDATE Lead " +
          "SET IsDuplicate = @IsDuplicate," +
              "IsSuccessful = @IsSuccessful, " +
              "IsCapped = @IsCapped " +

          "WHERE " +
          $"ContactNumber = '{request.ContactNumber}' OR Email = '{request.EmailAddress}' " +
          $"SELECT @Identity = LeadId FROM Lead WHERE ContactNumber = '{request.ContactNumber}' OR Email = '{request.EmailAddress}';" +
          "END " +
          "ELSE " +
          "BEGIN " +
          $"INSERT INTO Lead VALUES('{request.TrackingCode = "123"}'," +
          $"'{request.FirstName}'," +
          $"'{request.Surname}'," +
          $"'{request.ContactNumber}'," +
          $"'{request.EmailAddress}'," +
          "GETDATE()," +
          $"{(string.IsNullOrEmpty(request.ClientIpAddress) ? "NULL" : request.ClientIpAddress)}," +
          $"{(string.IsNullOrEmpty(request.UserAgent) ? "NULL" : request.ClientIpAddress)}," +
          $"{(string.IsNullOrEmpty(request.ReferrerUrl) ? "NULL" : request.ReferrerUrl)}," +
          $"NULL," +
          $"NULL," +
          $"NULL) " +
          "SELECT @Identity = SCOPE_IDENTITY();" +
          $"{parametersNonQueryString}; " +
          "END " +
          "" +
          "SELECT @Identity 'id',@Message 'message',@IsDuplicate 'IsDuplicate',@IsSuccessful 'isSuccessful',@IsCapped 'isCapped'";

        sqlCommand.CommandText = commandText;
        sqlCommand.Connection.Open();

        var reader = sqlCommand.ExecuteReader();
        if (reader.Read())

        {
          identity = long.Parse(reader["id"].ToString());
          IsDuplicate = Convert.ToBoolean(reader["IsDuplicate"]);
          isSuccessful = Convert.ToBoolean(reader["isSuccessful"]);
          isCapped = Convert.ToBoolean(reader["isCapped"]);
          message = reader["message"].ToString();
        }

      }
      return new Tuple<long, bool, bool, bool, string>(identity, IsDuplicate, isSuccessful, isCapped, message);

    }

  }
}

