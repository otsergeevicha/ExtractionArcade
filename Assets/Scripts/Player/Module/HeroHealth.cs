using System;
using R3;
using UnityEngine;

namespace Player.Module
{
    public class HeroHealth
    {
        public readonly int MaxHealth;
        private readonly ReactiveProperty<int> _currentHealth;

        public HeroHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            _currentHealth = new ReactiveProperty<int>(maxHealth);
        }

        public event Action Died;
        
        public Observable<int> Health =>
            _currentHealth;

        public void ApplyDamage(int damage)
        {
            _currentHealth.Value -= Mathf.Clamp(damage, Constants.MinHealth, MaxHealth);
            
            if (_currentHealth.Value <= Constants.MinHealth)
            {
                _currentHealth.Value = Constants.MinHealth;
                Died?.Invoke();
            }
        }
        
        public void ApplyHeal(int heal)
        {
            _currentHealth.Value += Mathf.Clamp(heal, Constants.MinHealth, MaxHealth);
            
            if (_currentHealth.Value >= MaxHealth) 
                _currentHealth.Value = MaxHealth;
        }
    }
}