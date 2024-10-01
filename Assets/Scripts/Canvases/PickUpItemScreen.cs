using System;
using Plugins.MonoCache;
using TMPro;
using UnityEngine;

namespace Canvases
{
    public class PickUpItemScreen : MonoCache
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _labelItem;
        
        public event Action OnUsed;
        public event Action OnTakeInventory;
        public event Action OnCancellation;
        
        public void OnActive(string getName)
        {
            _canvas.enabled = true;
            _labelItem.text = getName;
        }

        public void InActive() => 
            _canvas.enabled = false;

        public void Use() => 
            OnUsed?.Invoke();

        public void Take() => 
            OnTakeInventory?.Invoke();

        public void Cancellation() => 
            OnCancellation?.Invoke();
    }
}