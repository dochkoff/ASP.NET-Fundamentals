namespace ChatApp.Models.Message
{
    public class ChatViewModel
    {
        public MessageViewModel CurrentMessage { get; set; } = new MessageViewModel();

        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
    }
}
