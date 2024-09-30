using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Game", menuName = "Config/GameData", order = 1)]
    public class GameData : ScriptableObject
    {
        [Range(1, 10)]
        public int CountItems = 1;
    }
}