using ExtractionArcade.Scripts.GameCamera;
using ExtractionArcade.Scripts.Player;

namespace ExtractionArcade.Scripts.Services.Factory
{
    public interface IGameFactory
    {
        Hero CreateHero(Hero hero);
        CameraFollow CreateCameraHero(CameraFollow camera);
    }
}