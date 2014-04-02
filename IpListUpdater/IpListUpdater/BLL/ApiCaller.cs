using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using IpListUpdater.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IpListUpdater.BLL
{
    class ApiCaller
    {
        //List<ApiModel>
        public static List<ApiModel> GetData(string url, string parameters, out HttpStatusCode statusCode)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();
                statusCode = response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonSerializer = new DataContractJsonSerializer(typeof (List<ApiModel>));
                    var objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    var jsonResults = objResponse as List<ApiModel>;
                    response.Close();

                    return jsonResults;
                }
            }
            catch (Exception e)
            {
                statusCode = HttpStatusCode.BadRequest;
                return null;
            }
            statusCode = HttpStatusCode.NotFound;
            return null;
        }
    }
}
