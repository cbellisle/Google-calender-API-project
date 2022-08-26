using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace googleProject
{
    internal static class Program
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            UserCredential credential;
            // Load client secrets.
            try
            {


                using (var stream =
                       new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    /* The file token.json stores the user's access and refresh tokens, and is created
                     automatically when the authorization flow completes for the first time. */
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });

                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                // request.
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;


               // var calenderId = "primary";

                //service.Events.Delete(calenderId, "28q2ortknqa6190tr4cbu9ub84").Execute();

                // List events.
                Events events = request.Execute();
                Console.WriteLine("Upcoming events:");

                //var newEvent = new Event();

                //EventDateTime start = new EventDateTime();
                //EventDateTime end = new EventDateTime();

                //start.DateTime = new DateTime(2022, 8, 24);
                //end.DateTime = new DateTime(2022, 8, 26);
                //newEvent.Start = start;
                //newEvent.End = end;


                ////newEvent.Id = "Testevent";
                //newEvent.Summary = "Kill riley";
                //newEvent.Description = "Make riley have a bad bad day";

                //Event recurringEvent = service.Events.Insert(newEvent, calenderId).Execute();
                //newEvent.HtmlLink;
                //events.Items.RemoveAt(1);

                if (events.Items == null || events.Items.Count == 0)
                {
                    Console.WriteLine("No upcoming events found.");
                    //lets create our own event

                    //vents.Items.Insert()
                    return;
                }

                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }

            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            
        }

        public static void Send(Event newevent)
        {
            UserCredential credential;

            try
            {
                

                using (var stream =
                       new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    /* The file token.json stores the user's access and refresh tokens, and is created
                     automatically when the authorization flow completes for the first time. */
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });

                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                // request.
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
                Event recurringEvent = service.Events.Insert(newevent, "primary").Execute();
                MessageBox.Show("Tasks successfully added!");
            }
            catch
            {
                MessageBox.Show("That did not work lol!");

                //do nothing
            }

        }

    }
}