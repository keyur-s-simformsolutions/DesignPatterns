namespace Singleton
{
    class Program
    {
        static void Main()
        {
            Singleton fromFacebook = Singleton.GetInstance;
            fromFacebook.PrintDetails("From Facebook");
            Singleton fromTwitter = Singleton.GetInstance;
            fromTwitter.PrintDetails("From Twitter");
            Console.ReadLine();
        }
    }
}
