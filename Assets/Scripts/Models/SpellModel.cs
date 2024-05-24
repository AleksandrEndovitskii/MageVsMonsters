using System;
using MageVsMonsters.Helpers;
using MageVsMonsters.JsonObjects;
using UnityEngine;

namespace MageVsMonsters.Models
{
    public class SpellModel : IModel
    {
        public event Action<string> NameChanged = delegate {};
        public string Name
        {
            get => _name;
            protected set
            {
                if (_name == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_name} -> {value}");
                _name = value;

                NameChanged.Invoke(_name);
            }
        }
        private string _name;
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

        public SpellModel()
        {
        }
        public SpellModel(SpellDefinitionJsonObject definitionJsonObject)
            : this()
        {
            Name = definitionJsonObject.Name;
            Damage = definitionJsonObject.Damage;
        }
        public SpellModel(SpellModel spellModel)
            : this()
        {
            Name = spellModel.Name;
            Damage = spellModel.Damage;
        }
    }
}
