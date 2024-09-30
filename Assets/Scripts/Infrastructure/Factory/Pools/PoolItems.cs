using System.Collections.Generic;
using Inventory.Items;
using Services.Factory;

namespace Infrastructure.Factory.Pools
{
    public class PoolItems
    {
        private readonly List<PickUpItem> _items = new ();

        public PoolItems(IGameFactory factory, TypeItem[] typeItems, int countItems)
        {
            for (int i = 0; i < typeItems.Length; i++)
            {
                for (int j = 0; j < countItems; j++)
                {
                    PickUpItem pickUpItem = factory.CreateItem();
                    pickUpItem.SetCurrentType(typeItems[i]);
                    pickUpItem.InActive();
                    _items.Add(pickUpItem);
                }
            }
        }
        
        public IReadOnlyList<PickUpItem> Items =>
            _items.AsReadOnly();
    }
}