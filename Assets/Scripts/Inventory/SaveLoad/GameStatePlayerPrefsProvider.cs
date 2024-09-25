using System.Collections.Generic;
using Inventory.Data;
using SO;
using UnityEngine;

namespace Inventory.SaveLoad
{
    public class GameStatePlayerPrefsProvider : IGameStateProvider, IGameStateSaver
    {
        private InventoryData _inventoryData;

        public GameStatePlayerPrefsProvider(InventoryData inventoryData) => 
            _inventoryData = inventoryData;

        private const string Key = "key";

        public GameStateData GameState { get; private set; }

        public void SaveGameState()
        {
            string json = JsonUtility.ToJson(GameState);
            PlayerPrefs.SetString(Key, json);
        }

        public void LoadGameState()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                string json = PlayerPrefs.GetString(Key);
                GameState = JsonUtility.FromJson<GameStateData>(json);
            }
            else
            {
                GameState = InitFromSettings();
                SaveGameState();
            }
        }

        private GameStateData InitFromSettings()
        {
            var gameState = new GameStateData
            {
                Inventories = _inventoryData.Inventories
            };

            foreach (InventoryGridData inventoryData in gameState.Inventories)
                CreateInventory(inventoryData.OwnerId, inventoryData.Size);

            return gameState;
        }

        private InventoryGridData CreateInventory(string ownerId, Vector2Int size)
        {
            List<InventorySlotData> createdInventorySlots = new List<InventorySlotData>();
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