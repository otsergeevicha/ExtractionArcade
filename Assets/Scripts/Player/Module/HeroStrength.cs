namespace Player.Module
{
    public class HeroStrength
    {
        private readonly int _maxStrength;
        private int _currentStrength;

        public HeroStrength(int maxStrength)
        {
            _maxStrength = maxStrength;
            _currentStrength = maxStrength;
        }
    }
}