namespace Stats.LifeTime
{
    public class RandomCounter : ICounter
    {
        static Random rnd = new Random();
        private int value;
        public RandomCounter()
        {
            value = rnd.Next(0,100000000);
        }
        public int Value
        {
            get => value;
        }
    }
}
