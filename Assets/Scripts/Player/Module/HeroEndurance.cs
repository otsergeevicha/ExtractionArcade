namespace Player.Module
{
    public class HeroEndurance
    {
        private readonly int _maxEndurance;
        private int _currentEndurance;

        public HeroEndurance(int maxEndurance)
        {
            _maxEndurance = maxEndurance;
            _currentEndurance = maxEndurance;
        }
    }
}