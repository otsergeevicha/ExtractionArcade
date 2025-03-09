using Plugins.MonoCache;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Views
{
    public class InventorySlotView : MonoCache
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private TMP_Text _textAmount;
        
        public Sprite Icon
        {
            get => 
                _image.sprite;
            set =>
                _image.sprite = value;
        }
        
        public string Title
        {
            get => 
                _textTitle.text;
            set => 
                _textTitle.text = value;
        }

        public int Amount
        {
            get
            {
                int.TryParse(_textAmount.text, out var currentValue);
                return currentValue;
            }
            set => 
                _textAmount.text = value == 0 
                    ? string.Empty
                    : value.ToString();
        }
    }
}