using System;
using System.Collections.Generic;
using Canvases.Views;
using Inventory;
using Inventory.Items;
using JoystickLogic;
using Player.Module.Parent;
using Plugins.MonoCache;
using Reflex;
using Services.Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace Canvases
{
    public class Hud : MonoCache
    {
        [SerializeField] private PickUpItemScreen _pickUpItemScreen;
        [SerializeField] private Slider _healthView;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Sprite _iconHealth;

        private HeroHealthView _heroHealthView;
        private IInputService _input;
        private IReadOnlyList<PickUpItem> _poolItems;
        private HeroModule _heroModule;
        private InventoryService _inventoryService;
        private PickUpItem _item;

        public event Action<string> OpenedInventory;
        public event Action<PickUpItem> UsedItem;

        public void Construct(Coroutines coroutine, Camera cashCamera, IInputService input, HeroModule heroModule,
            InventoryService inventoryService, IReadOnlyList<PickUpItem> poolItems)
        {
            _inventoryService = inventoryService;
            _heroModule = heroModule;
            _poolItems = poolItems;
            _input = input;
            _joystick.Construct(cashCamera, input);
            _heroHealthView = new HeroHealthView(coroutine, _healthView, heroModule.HeroHealth);

            foreach (PickUpItem item in _poolItems)
                item.OnTriggered += PickUpItemTriggered;

            _pickUpItemScreen.InActive();
        }

        protected override void OnDisabled()
        {
            foreach (PickUpItem item in _poolItems)
                item.OnTriggered -= PickUpItemTriggered;
            
            _pickUpItemScreen.OnUsed -= Use;
            _pickUpItemScreen.OnTakeInventory -= PutInventory;
            _pickUpItemScreen.OnCancellation -= ReturnGameState;
        }

        public void OnClickInventory(string ownerID)
        {
            OpenedInventory?.Invoke(ownerID);
            _input.OffControls();
        }

        private void PickUpItemTriggered(PickUpItem item)
        {
            _item = item;
            
            _input.OffControls();
            _pickUpItemScreen.OnActive(item.GetName());

            _pickUpItemScreen.OnUsed += Use;
            _pickUpItemScreen.OnTakeInventory += PutInventory;
            _pickUpItemScreen.OnCancellation += ReturnGameState;
        }

        private void Use()
        {
            _item.InActive();
            UsedItem?.Invoke(_item);
            ReturnGameState();
        }

        private void PutInventory()
        {
            _inventoryService.AddItemsToInventory("Hero",
                _item.Use().ToString(), _iconHealth);
            
            _item.InActive();
            ReturnGameState();
        }

        private void ReturnGameState()
        {
            _pickUpItemScreen.InActive();
            _input.OnControls();
        }
    }
}