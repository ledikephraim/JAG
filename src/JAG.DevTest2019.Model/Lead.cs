using System;
using System.Collections.Generic;
using System.Linq;

namespace JAG.DevTest2019.Model
{
    public class Lead
    {
        #region Headers
        public string ClientIpAddress { get; set; }
        public string ReferrerUrl { get; set; }
        public string UserAgent { get; set; }
        #endregion Headers

        #region Required system data
        public long LeadId { get; set; }
        public string TrackingCode { get; set; }
        #endregion Required system data

        #region Standard properties
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public virtual string ContactNumber { get; set; }
        #endregion Standard properties

        //All other properties are stored into a list of parameters
        #region Lead parameters
        public LeadParameter[] LeadParameters { get; set; }

        public IDictionary<string, string> GetLeadRequestParmeterDictionary ()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (LeadParameters != null)
            {
                result = LeadParameters.ToDictionary(item => item.Key, item => item.Value);
            }
            return result;
        }
        #endregion Lead parameters
    }
}
