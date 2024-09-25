using System;
using Plugins.MonoCache;
using TMPro;
using UnityEngine;

namespace Inventory.Views
{
    public class InventorySlotView : MonoCache
    {
        [SerializeField] private TMP_Text _textTitle;
        [SerializeField] private TMP_Text _textAmount;

        public string Title
        {
            get => 
                _textTitle.text;
            set => 
                _textAmount.text = value;
        }

        public int Amount
        {
            get => 
                Convert.ToInt32(_textAmount.text);
            set => 
                _textAmount.text = value == 0 ? "" : value.ToString();
        }
    }
}