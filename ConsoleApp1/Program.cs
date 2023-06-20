namespace Lesson
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger[] loggers =
            {
                new ConsoleLogWritter(),
                new FileLogWritter(),
                new SecureConsoleLogWritter(),
                new HybridLogWritter(),
                new SecureFileLogWritter()
            };

            Pathfinder[] pathfinders =
            {
                new Pathfinder(loggers[0]),
                new Pathfinder(loggers[1]),
                new Pathfinder(loggers[2]),
                new Pathfinder(loggers[3]),
                new Pathfinder(loggers[4]) 
            };

            foreach(var pathfinder in pathfinders)
            {
                pathfinder.Find();
            }
        }
    }

    class ConsoleLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureConsoleLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                Console.WriteLine(message);
            }
        }
    }

    class SecureFileLogWritter : ILogger
    {
        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                File.WriteAllText("log.txt", message);
            }
        }
    }

    class HybridLogWritter : ILogger
    {
        private readonly ILogger _consoleLogger;
        private readonly ILogger _fileLogger;

        public HybridLogWritter()
        {
            _consoleLogger = new ConsoleLogWritter();
            _fileLogger = new FileLogWritter();
        }

        public void WriteError(string message)
        {
            _consoleLogger.WriteError(message);

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                _fileLogger.WriteError(message);
        }
    }

    class Pathfinder
    {
        private readonly ILogger _logger;

        public Pathfinder(ILogger logger)
        {
            _logger = logger;
        }

        public void Find()
        {
            string result = "Найден путь: ";
            _logger.WriteError(result);
        }
    }

    interface ILogger
    {
        void WriteError(string message);
    }
}