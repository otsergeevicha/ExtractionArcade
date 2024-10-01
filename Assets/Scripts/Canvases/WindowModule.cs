using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Config;
using Inventory.Controllers;
using Inventory.Data;
using Inventory.Items;
using Inventory.SaveLoad;
using Inventory.Views;
using Player.Module.Parent;
using Services.Inputs;
using UnityEngine;

namespace Canvases
{
    public class WindowModule : IDisposable
    {
        private readonly InventoryScreenView _inventoryScreenView;
        private readonly IInputService _input;
        private readonly Hud _hud;
        private readonly HeroModule _heroModule;
        
        private InventoryScreenView _inventoryScreen;
        private InventoryService _inventoryService;
        private InventoryScreenController _screenController;

        public WindowModule(IReadOnlyList<InitConfigInventoryGrid> inventoryData,
            InventoryScreenView inventoryScreenView, Hud hud,
            IInputService input, HeroModule heroModule)
        {
            //
            PlayerPrefs.DeleteAll();
            Debug.Log("Убрать!");
            //
            
            _heroModule = heroModule;
            _input = input;
            _hud = hud;
            
            _inventoryScreenView = inventoryScreenView;
            EntryPointInventory(inventoryData);
            
            _hud.OpenedInventory += OnOpenedInventory;
            _inventoryScreenView.UseItem += InventoryOnUseItem;
            _hud.UsedItem += PickUpUsedItem;
        }

        public InventoryService GetInventoryService =>
            _inventoryService;

        private void PickUpUsedItem(PickUpItem pickUpItem) => 
            _heroModule.UseItem(pickUpItem.PickUp());

        private void InventoryOnUseItem(string slot)
        {
            if (Enum.TryParse<TypeItem>(slot.Replace(InventoryConstants.NewValue,
                    InventoryConstants.OldValue), out TypeItem item))
            {
                _heroModule.UseItem(item);
                _inventoryService.RemoveItems("Hero", slot);
            }
        }

        public void Dispose()
        {
            _hud.OpenedInventory -= OnOpenedInventory;
            _inventoryScreenView.UseItem -= InventoryOnUseItem;
            _hud.UsedItem -= PickUpUsedItem;
        }

        private void OnOpenedInventory(string ownerID)
        {
            _inventoryScreenView.Closed += InventoryScreenViewOnClosed;
            _screenController.OpenInventory(ownerID);
        }

        private void InventoryScreenViewOnClosed()
        {
            _input.OnControls();
            _inventoryScreenView.Closed -= InventoryScreenViewOnClosed;
        }

        private void EntryPointInventory(IReadOnlyList<InitConfigInventoryGrid> inventoryData)
        {
            var gameStateProvider = new InventoryStateProvider(inventoryData);
            gameStateProvider.LoadGameState();
            _inventoryService = new InventoryService(gameStateProvider);
            var inventories = gameStateProvider.GameState.Inventories;
            
            foreach (InventoryGridData inventory in inventories) 
                _inventoryService.RegisterInventory(inventory);
            
            _screenController = new InventoryScreenController(_inventoryService, _inventoryScreenView);
        }
    }
}