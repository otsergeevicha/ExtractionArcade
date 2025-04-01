using ExtractionArcade.Scripts.Player;
using ExtractionArcade.Scripts.Services.Factory;
using UnityEngine;

namespace ExtractionArcade.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public Hero CreateHero(Hero hero) =>
            Object.Instantiate(hero);
    }
}