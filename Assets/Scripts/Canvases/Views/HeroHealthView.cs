using System.Collections;
using Player.Module;
using R3;
using Reflex;
using UnityEngine;
using UnityEngine.UI;

namespace Canvases.Views
{
    public class HeroHealthView
    {
        private readonly HeroHealth _heroHealth;
        private readonly Slider _sliderView;
        private readonly Coroutines _coroutine;
        
        private Coroutine _cashCoroutine;

        public HeroHealthView(Coroutines coroutine, Slider sliderView, HeroHealth heroHealth)
        {
            _sliderView = sliderView;
            _heroHealth = heroHealth;
            _coroutine = coroutine;

            _heroHealth.Health.Subscribe(UpdateHeroHealth);
        }

        private void UpdateHeroHealth(int value)    
        {
            float targetValue = (float)value / _heroHealth.MaxHealth;

            if (_cashCoroutine != null)
                _coroutine.StopCoroutine(_cashCoroutine);
            
            _cashCoroutine = _coroutine.StartCoroutine(UpdateView(targetValue));
        }
        
        private IEnumerator UpdateView(float targetValue)   
        {
            while (!Mathf.Approximately(_sliderView.value, targetValue))
            {
                _sliderView.value = Mathf.MoveTowards(_sliderView.value, targetValue, .1f);
                yield return null;
            }
        }
    }
}