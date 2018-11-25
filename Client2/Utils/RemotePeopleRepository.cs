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
            request.RequestFormat = DataFormat.Json;
            var response = _restClient.Execute<List<Person>>(request);

            return response;
        }

        public static IRestResponse Add(Person person)
        {
            var request = new RestRequest("api/people", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(person);

            var response = _restClient.Execute(request);

            return response;
        }

        public static IRestResponse Add(ICollection<Person> people)
        {
            var request = new RestRequest("api/people/batch", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(people);

            var response = _restClient.Execute(request);

            return response;
        }

        public static IRestResponse Delete(Guid guid)
        {
            var request = new RestRequest("api/people/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", guid);

            var response = _restClient.Execute(request);

            return response;
        }

        public static IRestResponse Delete(ICollection<Guid> guids)
        {
            var request = new RestRequest("api/people", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(guids);

            var response = _restClient.Execute(request);

            return response;
        }
    }
}
