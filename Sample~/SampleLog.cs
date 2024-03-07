namespace Cr7Sund.Sample
{
    public class SampleLog
    {
        public void Start()
        {
            string customChannel = "Sample";
            int userId = 24;
            var logger = new SampleLoggerProxy(customChannel);

            logger.Info("HelloWorld");
            logger.Debug("Hello World");
            logger.Debug("Hello {UserID}", userId);
            logger.Debug("Hello {0} again", userId);
        }
    }
}