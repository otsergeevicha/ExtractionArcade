using Inventory.Items;
using SO;
using UnityEngine;

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

        public void UseItem(TypeItem item)
        {
            switch (item)
            {
                case TypeItem.Health_Potion:
                    HeroHealth.ApplyHeal(2500);
                    break;
                case TypeItem.Endurance_Potion:
                    Debug.Log($"Not realization effect");
                    break;
                case TypeItem.Strength_Potion:
                    Debug.Log($"Not realization effect");
                    break;
                case TypeItem.Wisdom_Potion:
                    Debug.Log($"Not realization effect");
                    break;
                default:
                    Debug.Log($"There are no matching used items");
                    break;
            }
        }
    }
}