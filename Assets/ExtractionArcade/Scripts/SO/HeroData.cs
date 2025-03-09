using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "NewHero", menuName = "Characters/Hero", order = 1)]
    public class HeroData : ScriptableObject
    {
        [Range(1, 5000)]
        public int MaxHealth = 1;
        [Range(1, 5000)]
        public int MaxStrength = 1;
        [Range(1, 5000)]
        public int MaxEndurance = 1;
        [Range(1, 5000)]
        public int MaxWisdom = 1;
        
        [Range(3, 8)]
        public int Speed = 3;
        
        [HideInInspector] public int Blend = Animator.StringToHash("Blend");
    }
}