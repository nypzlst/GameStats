namespace Stats.TimeMiddleware
{
    public class DefaultTime : ITime
    {
        public string GetTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
