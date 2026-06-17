namespace WebApi.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;

        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        //  Aqui você aplica a função de para salvar a ação
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string menssagem = $"{logLevel.ToString()}? {eventId.Id} - {formatter(state, exception)}";

            this.GerarArquivoLogo(menssagem);
        }

        private void GerarArquivoLogo(string menssagem)
        {
            string caminho = @"C:\Users\w5i20\Documents\Log\log_api_web.txt";

            using (StreamWriter streamWriter = new StreamWriter(caminho, true))
            {
                try
                {
                    streamWriter.WriteLine(menssagem);
                    streamWriter.Close();
                }
                catch(Exception)
                {
                    throw;
                }
            }

        }
    }
}
