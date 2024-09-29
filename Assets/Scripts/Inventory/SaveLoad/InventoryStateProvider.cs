using System.Collections.Generic;
using System.Linq;
using Inventory.Data;
using SO;
using UnityEngine;

namespace Inventory.SaveLoad
{
    public class InventoryStateProvider : IGameStateProvider, IGameStateSaver
    {
        private readonly IReadOnlyList<InitConfigInventoryGrid> _configInventoryGrids;

        public InventoryStateProvider(IReadOnlyList<InitConfigInventoryGrid> configInventoryGrids) =>
            _configInventoryGrids = configInventoryGrids;

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

        public void SaveGameState() =>
            PlayerPrefs.SetString(Constants.InventoryKey, JsonUtility.ToJson(GameState));

        private GameStateData InitFromSettings()
        {
            List<InventoryGridData> inventoryData = new List<InventoryGridData>();

            foreach (InitConfigInventoryGrid currentInventoryConfig in _configInventoryGrids)
            {
                InventoryGridData newInventoryGridData = new InventoryGridData
                {
                    OwnerId = currentInventoryConfig.OwnerId,
                    Size = currentInventoryConfig.Size,
                    DefaultIcon = currentInventoryConfig.DefaultIcon,
                    Slots = new List<InventorySlotData>(currentInventoryConfig.Slots)
                };
                
                inventoryData.Add(newInventoryGridData);
            }

            return new()
            {
                Inventories = inventoryData.Select(inventory =>
                    CreateInventory(inventory.OwnerId, inventory.DefaultIcon, inventory.Size, inventory.Slots)).ToList()
            };
        }

        private InventoryGridData CreateInventory(string ownerId, Sprite defaultIcon, Vector2Int size,
            List<InventorySlotData> slots)
        {
            List<InventorySlotData> createdInventorySlots = slots;
            int length = size.x * size.y;

            for (int i = 0; i < length; i++)
                createdInventorySlots.Add(new InventorySlotData());

            var createdInventoryData = new InventoryGridData
            {
                OwnerId = ownerId,
                DefaultIcon = defaultIcon,
                Size = size,
                Slots = createdInventorySlots
            };

            return createdInventoryData;
        }
    }
}