using Inventory;
using Inventory.Data;
using Inventory.SaveLoad;
using Services.Factory;
using SO;

namespace Canvases
{
    public class WindowModule
    {
        private InventoryService _inventoryService;

        public WindowModule(InventoryData inventoryData, IGameFactory gameFactory) => 
            CreateHeroInventory(inventoryData);

        private void CreateHeroInventory(InventoryData inventoryData)
        {
            var gameStateProvider = new GameStatePlayerPrefsProvider(inventoryData);
            gameStateProvider.LoadGameState();

            _inventoryService = new InventoryService(gameStateProvider);
            var gameState = gameStateProvider.GameState;

            foreach (InventoryGridData inventory in gameState.Inventories)
                _inventoryService.RegisterInventory(inventory);
        }
    }
}