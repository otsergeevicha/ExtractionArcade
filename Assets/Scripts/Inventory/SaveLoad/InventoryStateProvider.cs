using System.Collections.Generic;
using Inventory.Data;
using SO;
using UnityEngine;

namespace Inventory.SaveLoad
{
    public class InventoryStateProvider : IGameStateProvider, IGameStateSaver
    {
        private readonly InventoryData _inventoryData;

        public InventoryStateProvider(InventoryData inventoryData) =>
            _inventoryData = inventoryData;

        public GameStateData GameState { get; private set; }

        public void LoadGameState()
        {
            if (PlayerPrefs.HasKey(Constants.InventoryKey))
                GameState = JsonUtility.FromJson<GameStateData>(PlayerPrefs.GetString(Constants.InventoryKey));
            else
            {
                GameState = InitFromSettings();
                SaveGameState();
            }
        }

        private GameStateData InitFromSettings() =>
            new ()
            {
                Inventories = new List<InventoryGridData>
                {
                    CreateInventory(_inventoryData.Inventories[0].OwnerId, _inventoryData.Inventories[0].Size)
                }
            };

        public void SaveGameState() => 
            PlayerPrefs.SetString(Constants.InventoryKey, JsonUtility.ToJson(GameState));

        private InventoryGridData CreateInventory(string ownerId, Vector2Int size)
        {
            List<InventorySlotData> createdInventorySlots = _inventoryData.Inventories[0].Slots;
            int length = size.x * size.y;

            for (int i = 0; i < length; i++) 
                createdInventorySlots.Add(new InventorySlotData());
            
            var createdInventoryData = new InventoryGridData
            {
                OwnerId = ownerId,
                Size = size,
                Slots = createdInventorySlots
            };

            return createdInventoryData;
        }
    }
}