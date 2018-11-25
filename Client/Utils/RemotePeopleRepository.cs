using Client.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Utils
{
    class RemotePeopleRepository
    {
        private static RestClient _restClient = new RestClient("https://localhost:44339");

        public static IRestResponse<List<Person>> Get()
        {
            var request = new RestRequest("api/people", Method.GET);
            var response = _restClient.Execute<List<Person>>(request);

            return response;
        }

        public static IRestResponse Add(Person person)
        {
            var request = new RestRequest("api/people", Method.POST);
            request.AddBody(person);

            var response = _restClient.Execute(request);

            return response;
        }

        public static IRestResponse Add(ICollection<Person> people)
        {
            var request = new RestRequest("api/people", Method.POST);
            request.AddBody(people);

            var response = _restClient.Execute(request);

            return response;
        }

        public static IRestResponse Delete(Guid guid)
        {
            var request = new RestRequest("api/people/{id}", Method.DELETE);
            request.AddUrlSegment("id", guid);

            var response = _restClient.Execute(request);

            return response;
        }

        public static IRestResponse Delete(ICollection<Guid> guids)
        {
            var request = new RestRequest("api/people", Method.DELETE);
            request.AddBody(guids);

            var response = _restClient.Execute(request);

            return response;
        }
    }
}
