namespace UdpEcho
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8998;
            if (args.Length > 0) port = int.Parse(args[1]);

            var server = new Server();
            server.Run(port);
        }
    }
}
