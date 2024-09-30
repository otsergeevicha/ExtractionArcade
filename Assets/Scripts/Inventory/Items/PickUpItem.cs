using System;
using Player;
using Plugins.MonoCache;
using UnityEngine;

namespace Inventory.Items
{
    public class PickUpItem : MonoCache, IItem
    {
        [SerializeField] private TypeItem _typeItem;
        
        public event Action<PickUpItem> OnTriggered;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Hero _))
                OnTriggered?.Invoke(this);
        }

        public TypeItem PickUp()
        {
            gameObject.SetActive(false);
            return _typeItem;
        }

        public void ThrowDown(Vector3 newPosition)
        {
            gameObject.SetActive(true);
            transform.position = newPosition;
        }

        public TypeItem Use() =>
            _typeItem;

        public string GetName() =>
            _typeItem.ToString().Replace(InventoryConstants.OldValue, InventoryConstants.NewValue);

        public void SetCurrentType(TypeItem typeItem) =>
            _typeItem = typeItem;

        public void OnActive() =>
            gameObject.SetActive(true);

        public void InActive() =>
            gameObject.SetActive(false);
    }
}