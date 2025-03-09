using Plugins.MonoCache;
using UnityEngine;

namespace Player
{
    public class Hero : MonoCache
    {
        public void Start()
        {
            transform.position = new Vector3(0f, 15f, 0f);
        }
    }
}