using ChatApp.Models.Message;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{

    //Warning: the above code holds the shared app data in a static field in the controller class.
    //This is just for the example, and it is generally a bad practice!
    //Use a database or other persistent storage to hold data,
    //which should survive between the app requests and should be shared between all app users.
    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> s_messages = new List<KeyValuePair<string, string>>();

        [HttpGet]
        public IActionResult Show()
        {
            if (s_messages.Count < 1) 
            { 
                return View(new ChatViewModel());
            }

            var chatModel = new ChatViewModel()
            {
                Messages = s_messages
                    .Select(m => new MessageViewModel()
                    {
                        Sender = m.Key,
                        MessageText = m.Value
                    })
                    .ToList()
            };

            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMessage = chat.CurrentMessage;

            s_messages.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));

            return RedirectToAction("Show");
        }
    }
}
