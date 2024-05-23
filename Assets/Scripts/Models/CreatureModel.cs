using System;
using MageVsMonsters.Helpers;
using MageVsMonsters.JsonObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
            protected set
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
        public event Action<Rarity> RarityChanged = delegate {};
        [JsonConverter(typeof(StringEnumConverter))]
        public Rarity Rarity
        {
            get => _rarity;
            protected set
            {
                if (_rarity == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_rarity} -> {value}");
                _rarity = value;

                RarityChanged.Invoke(_rarity);
            }
        }
        private Rarity _rarity;

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
            HealthChanged += OnHealthChanged;
            OnHealthChanged(Health);
        }
        public CreatureModel(int maxHealth) :
            this()
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;
        }
        public CreatureModel(CreatureDefinitionJsonObject creatureDefinitionJsonObject) :
            this(creatureDefinitionJsonObject.MaxHealth)
        {
            Damage = creatureDefinitionJsonObject.Damage;
            Defense = creatureDefinitionJsonObject.Defense;
            MovementSpeed = creatureDefinitionJsonObject.MovementSpeed;
            Rarity = creatureDefinitionJsonObject.Rarity;
        }

        public void DoDamage(int damage)
        {
            // health=health-damage*defense(0...1)
            Health -= (int)(damage * (1 - Defense / 100f));
        }

        private void OnHealthChanged(int health)
        {
            IsAlive = health > 0;
        }
    }
}
