namespace Inventory.SaveLoad
{
    public interface IGameStateProvider
    {
        void SaveGameState();
        void LoadGameState();
    }
}