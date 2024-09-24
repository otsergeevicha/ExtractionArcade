namespace Player.Module
{
    public class HeroWisdom
    {
        private readonly int _maxWisdom;
        private int _currentWisdom;

        public HeroWisdom(int maxWisdom)
        {
            _maxWisdom = maxWisdom;
            _currentWisdom = maxWisdom;
        }
    }
}