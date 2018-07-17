using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();

            Console.WriteLine("done");
            Console.ReadKey();
        }

        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client1", "secret1");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("apiOne");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            Console.WriteLine("====================================");
            Console.WriteLine("Get owner resource");

            var tokenClient2 = new TokenClient(disco.TokenEndpoint, "ro.client", "ro.secret");
            var tokenResponse2 = await tokenClient2.RequestResourceOwnerPasswordAsync("jeremy", "password", "apiOne");
            if (tokenResponse2.IsError)
            {
                Console.WriteLine(tokenResponse2.Error);
                return;
            }

            Console.WriteLine(tokenResponse2.Json);
            Console.WriteLine("\n\n");
        }
    }
}
