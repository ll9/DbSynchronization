using Client.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Utils
{
    class Rest
    {
        public static IList<Person> GetPeople()
        {
            var client = new RestClient("http://example.com");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("resource/{id}", Method.GET);
            var response = client.Execute<List<Person>>(request);

            return response.Data;
        }
    }
}
