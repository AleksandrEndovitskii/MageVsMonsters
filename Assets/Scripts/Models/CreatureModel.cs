using System;
using MageVsMonsters.Helpers;
using UnityEngine;

namespace MageVsMonsters.Models
{
    public class CreatureModel : IModel
    {
        public event Action<int> MaxHealthChanged = delegate {};
        public int MaxHealth
        {
            get => _maxHealth;
            protected set
            {
                if (_maxHealth == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_maxHealth} -> {value}");
                _maxHealth = value;

                MaxHealthChanged.Invoke(_maxHealth);
            }
        }
        private int _maxHealth;
        public event Action<int> HealthChanged = delegate {};
        public int Health
        {
            get => _health;
            set
            {
                if (_health == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_health} -> {value}");
                _health = value;

                HealthChanged.Invoke(_health);
            }
        }
        private int _health;
        public event Action<bool> IsAliveChanged = delegate {};
        public bool IsAlive
        {
            get => _isAlive;
            protected set
            {
                if (_isAlive == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_isAlive} -> {value}");
                _isAlive = value;

                IsAliveChanged.Invoke(_isAlive);
            }
        }
        private bool _isAlive;

        public CreatureModel()
        {
            MaxHealth = 0;
            Health = MaxHealth;
            IsAlive = true;

            HealthChanged += OnHealthChanged;
        }
        public CreatureModel(int maxHealth) :
            this()
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;
        }
        public CreatureModel(int maxHealth, int currentHealth) :
            this()
        {
            MaxHealth = maxHealth;
            Health = currentHealth;
        }

        private void OnHealthChanged(int health)
        {
            IsAlive = health > 0;
        }
    }
}
