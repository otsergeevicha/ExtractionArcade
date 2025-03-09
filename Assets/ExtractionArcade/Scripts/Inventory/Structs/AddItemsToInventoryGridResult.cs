namespace Inventory.Structs
{
    public readonly struct AddItemsToInventoryGridResult
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsToAddAmount;
        public readonly int ItemsAddedAmount;

        public AddItemsToInventoryGridResult(string inventoryOwnerId, int itemsToAddAmount, int itemsAddedAmount)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsToAddAmount = itemsToAddAmount;
            ItemsAddedAmount = itemsAddedAmount;
        }

        public int ItemsNotAddedAmount =>
            ItemsToAddAmount - ItemsAddedAmount;
    }
}