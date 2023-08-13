namespace Stats.LifeTime
{
    public class CounterService
    {
        public ICounter counter { get; }
        public CounterService(ICounter counter)
        {
            this.counter = counter;
        }
    }
}
