using System;
using System.Net.Http;
using IdentityModel.Client;

namespace PwdClient
{
    class Program
    {
        static void Main(string[] args)
        {
             var diso = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            if (diso.IsError)
            {
                Console.WriteLine(diso.Error);
            }
            var tokenClient=new TokenClient(diso.TokenEndpoint,"pwdclient","secret");
           var tokenResponse= tokenClient.RequestResourceOwnerPasswordAsync("jesse1","123456").Result;
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }

            HttpClient client=new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
           var response= client.GetAsync("http://localhost:5002/api/values").Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.WriteLine("Hello World!"); 
        }
    }
}
