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
            // Load client secrets.
          
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            
        }

        public static void Send(Event @event)
        {

            try
            {

                UserCredential credential;
                // Load client secrets.
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
                var calenderId = "primary";


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
                Events events = request.Execute();

            //var newEvent = new Event();

            //EventDateTime start = new EventDateTime();
            //EventDateTime end = new EventDateTime();

            //start.DateTime = new DateTime(2022, 9, 29);
            //end.DateTime = new DateTime(2022, 9, 30);
            //newEvent.Start = start;
            //newEvent.End = end;


            ////newEvent.Id = "Testevent";
            //newEvent.Summary = "Kill riley";
            //newEvent.Description = "Make riley have a bad bad day";

            // = service.Events.Insert(@event, calenderId).Execute();
            //MessageBox.Show("kill riley");

            var sumEvent = new Event();
            sumEvent = @event;
            Event recurringEvent = service.Events.Insert(sumEvent, calenderId).Execute();
                MessageBox.Show("that shoulda worked");

            }
            catch
            {
                //do nothing
                MessageBox.Show("That didn't work... contact the Caleb");
            }

}

    }
}