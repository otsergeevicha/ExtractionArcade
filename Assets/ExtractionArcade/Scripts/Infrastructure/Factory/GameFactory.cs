using Player;
using Services.Factory;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public Hero CreateHero(Hero hero) => 
            Object.Instantiate(hero)
                .GetComponent<Hero>();
    }
}