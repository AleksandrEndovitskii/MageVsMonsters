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
        public event Action<int> DamageChanged = delegate {};
        public int Damage
        {
            get => _damage;
            protected set
            {
                if (_damage == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_damage} -> {value}");
                _damage = value;

                DamageChanged.Invoke(_damage);
            }
        }
        private int _damage;
        public event Action<int> DefenseChanged = delegate {};
        public int Defense
        {
            get => _defense;
            protected set
            {
                if (_defense == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_defense} -> {value}");
                _defense = value;

                DefenseChanged.Invoke(_defense);
            }
        }
        private int _defense;
        public event Action<float> MovementSpeedChanged = delegate {};
        public float MovementSpeed
        {
            get => _movementSpeed;
            protected set
            {
                if (Math.Abs(_movementSpeed - value) < float.Epsilon)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_movementSpeed} -> {value}");
                _movementSpeed = value;

                MovementSpeedChanged.Invoke(_movementSpeed);
            }
        }
        private float _movementSpeed;

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

        private CreatureModel()
        {
            HealthChanged += OnHealthChanged;
            OnHealthChanged(Health);
        }
        private CreatureModel(int maxHealth) :
            this()
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;
        }
        public CreatureModel(int maxHealth, int damage, int defense, float movementSpeed) :
            this(maxHealth)
        {
            Damage = damage;
            Defense = defense;
            MovementSpeed = movementSpeed;
        }

        private void OnHealthChanged(int health)
        {
            IsAlive = health > 0;
        }
    }
}
