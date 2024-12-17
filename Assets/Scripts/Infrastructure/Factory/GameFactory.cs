using GameCamera;
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

        public MainCamera CreateMainCamera(MainCamera mainCamera) => 
            Object.Instantiate(mainCamera)
                .GetComponent<MainCamera>();
    }
}