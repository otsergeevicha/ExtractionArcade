using UnityEngine;

namespace ExtractionArcade.Scripts
{
    public static class Constants
    {
        public const string MAIN_SCENE = "MainScene";
        public const int HERO_SPEED = 4;
        public const float HERO_ROTATE_SPEED = 700f;
        public const float Gravity = -9.81f;
        public const float DownforceValue = -1f;
        
        public static readonly int HASH_HERO_IS_WALK = Animator.StringToHash("IsWalk");
    }
}