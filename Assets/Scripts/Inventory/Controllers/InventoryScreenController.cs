using Inventory.Views;

namespace Inventory.Controllers
{
    public class InventoryScreenController
    {
        private readonly InventoryService _service;
        private readonly InventoryScreenView _view;

        private InventoryGridController _currentInventoryGridController;
        
        public InventoryScreenController(InventoryService service, InventoryScreenView view)
        {
            _service = service;
            _view = view;
        }

        public void OpenInventory(string ownerId)
        {
            var inventory = _service.GetInventory(ownerId);
            var inventoryView = _view.InventoryView;

            _currentInventoryGridController = new InventoryGridController(inventory, inventoryView);
        }
    }
}