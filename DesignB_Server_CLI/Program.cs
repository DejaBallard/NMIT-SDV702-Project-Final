using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace DesignB_Server_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up server configuration
            Uri _baseAddress = new Uri("http://localhost:60064/");
            HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);
            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );
            // Create server
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            // Start listening
            server.OpenAsync().Wait();
            Console.WriteLine("DesignB Web-API Self hosted on " + _baseAddress);
            Console.WriteLine("Connecting to Database...");
            TestConnection();
            Console.ReadLine();
            server.CloseAsync().Wait();

        }

        private static void TestConnection()
        {
            try
            {
                clsDbConnection.CheckTables();
                Console.WriteLine("Connected!");
                Console.WriteLine("Hit ENTER to exit...");

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.GetBaseException());
                Console.WriteLine("\nCould be the connection string in app.config is wrong");
                Console.WriteLine("Or you havnt ran the database creation yet");
                Console.WriteLine("\nPress 1 -If you need a Database called dbdesignb, would you like a example one to be made?");
                string lcResult = Console.ReadLine();
                if (lcResult == "1")
                {
                    try
                    {
                        clsDbConnection.CreateDB();
                        Console.WriteLine("Completed!");
                        Console.WriteLine("Hit ENTER to refresh...");
                        Console.ReadLine();
                        Console.Clear();
                        TestConnection();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine(ex2.GetBaseException());
                    }
                }
            }
        }
    }
}
