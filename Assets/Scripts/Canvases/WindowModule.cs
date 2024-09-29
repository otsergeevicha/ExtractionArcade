using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Inventory;
using Inventory.Controllers;
using Inventory.Data;
using Inventory.SaveLoad;
using Inventory.Views;
using Services.Inputs;
using SO;
using UnityEngine;

namespace Canvases
{
    public class WindowModule : IDisposable
    {
        private readonly InventoryScreenView _inventoryScreenView;
        private readonly IInputService _input;
        private readonly Hud _hud;
        
        private InventoryScreenView _inventoryScreen;
        private InventoryService _inventoryService;
        private InventoryScreenController _screenController;

        public WindowModule(IReadOnlyList<InitConfigInventoryGrid> inventoryData, InventoryScreenView inventoryScreenView, Hud hud,
            IInputService input)
        {
            _input = input;
            _hud = hud;
            
            _inventoryScreenView = inventoryScreenView;
            EntryPointInventory(inventoryData);
            
            _hud.OpenedInventory += OnOpenedInventory;
        }

        public void Dispose() => 
            _hud.OpenedInventory -= OnOpenedInventory;

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