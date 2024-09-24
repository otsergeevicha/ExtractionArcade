using SO;

namespace Player.Module.Parent
{
    public class HeroModule
    {
        public HeroModule(HeroData heroData)
        {
            HeroHealth = new HeroHealth(heroData.MaxHealth);
            HeroStrength = new HeroStrength(heroData.MaxStrength);
            HeroEndurance = new HeroEndurance(heroData.MaxEndurance);
            HeroWisdom = new HeroWisdom(heroData.MaxWisdom);
        }
        
        public HeroHealth HeroHealth { get; private set; }
        public HeroStrength HeroStrength { get; private set; }
        public HeroEndurance HeroEndurance { get; private set; }
        public HeroWisdom HeroWisdom { get; private set; }
    }
}