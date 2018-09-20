using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace AdminProducto.Controllers
{
    public class EmailsController : ApiController
    {
        // GET: api/Emails
        public JsonResult<List<Message>> Get()
        {
            // The client disconnects from the server when being disposed
            using (Pop3Client client = new Pop3Client())
            {
                string hostname = "pop.gmail.com";
                int port = 995;
                bool useSsl = true;
                string username = "jadelarosapaulino@gmail.com";
                string password = "JAdp.5237";

                // Connect to the server
                client.Connect(hostname, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(username, password);

                // Get the number of messages in the inbox
                int messageCount = 10; // client.GetMessageCount();

                // We want to download all messages
                List<Message> allMessages = new List<Message>(messageCount);

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    allMessages.Add(client.GetMessage(i));
                }

                // Now return the fetched messages
                return Json(allMessages);
            }
        }

        // GET: api/Emails/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Emails
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Emails/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Emails/5
        public void Delete(int id)
        {
        }
    }
}
