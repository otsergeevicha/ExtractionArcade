using UnityEngine;

namespace Inventory.Items
{
    public interface IItem
    {
        TypeItem PickUp();
        void ThrowDown(Vector3 newPosition);
        TypeItem Use();
        string GetName();
        void SetCurrentType(TypeItem typeItem);
        void OnActive(Vector3 newPosition);
        void InActive();
    }
}