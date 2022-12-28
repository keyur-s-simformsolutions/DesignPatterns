using Circuit_Breaker.Data;
using Microsoft.Extensions.Options;

namespace Circuit_Breaker.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private MessageOptions _messageOptions;
        public MessageRepository(IOptions<MessageOptions> messageOptions)
        {
            _messageOptions = messageOptions.Value;
        }
        public async Task<string> GetGoodbyeMessage()
        {
            Console.WriteLine("MessageRepository GetGoodbyeMessage running");
            ThrowRandomException();
            return _messageOptions.GoodbyeMessage;
        }

        public async Task<string> GetHelloMessage()
        {
            Console.WriteLine("MessageRepository GetHelloMessage running");
            ThrowRandomException();
            return _messageOptions.HelloMessage;
        }
        private void ThrowRandomException()
        {
            var diceRoll = new Random().Next(0, 10);

            if (diceRoll > 5)
            {
                Console.WriteLine("ERROR! Throwing Exception");
                throw new Exception("Exception in MessageRepository");
            }
        }
    }
}
