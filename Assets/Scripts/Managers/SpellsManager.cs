using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Helpers;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
using Newtonsoft.Json;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class SpellsManager : BaseManager<SpellsManager>
    {
        [SerializeField]
        private SpellView _spellViewPrefab;

        public event Action<SpellModel> CurrentSpellChanged = delegate { };
        public SpellModel CurrentSpell
        {
            get => _currentSpell;
            private set
            {
                if (value == _currentSpell)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_currentSpell} -> {JsonConvert.SerializeObject(value)}");
                _currentSpell = value;

                CurrentSpellChanged.Invoke(_currentSpell);
            }
        }
        private SpellModel _currentSpell;

        private List<SpellModel> _spellModels;

        protected override async UniTask Initialize()
        {
            // TODO: temp solution - use definitions from JSON
            _spellModels = new List<SpellModel>
            {
                new SpellModel("FireBall", 20),
                new SpellModel("FrostBolt", 10),
                new SpellModel("LightningBolt", 5),
            };
            CurrentSpell = _spellModels[0];

            IsInitialized = true;
        }
        protected override async UniTask UnInitialize()
        {
            IsInitialized = false;
        }

        protected override async UniTask Subscribe()
        {
        }
        protected override async UniTask UnSubscribe()
        {
        }

        public void SwitchToNextSpell()
        {
            var currentSpellIndex = _spellModels.IndexOf(CurrentSpell);
            var nextSpellIndex = currentSpellIndex + 1;
            if (nextSpellIndex >= _spellModels.Count)
            {
                nextSpellIndex = 0;
            }
            CurrentSpell = _spellModels[nextSpellIndex];
        }
        public void SwitchToPreviousSpell()
        {
            var currentSpellIndex = _spellModels.IndexOf(CurrentSpell);
            var previousSpellIndex = currentSpellIndex - 1;
            if (previousSpellIndex < 0)
            {
                previousSpellIndex = _spellModels.Count - 1;
            }
            CurrentSpell = _spellModels[previousSpellIndex];
        }

        public void CastSpell(SpellModel spellModel = null, CreatureView sourceCreatureView = null, CreatureView targetCreatureView = null)
        {
            spellModel = new SpellModel(CurrentSpell.Name, CurrentSpell.Damage);
            var spellViewInstance = this.InstantiateElement(spellModel, _spellViewPrefab, this.gameObject.transform);
            ProjectilesManager.Instance.SendProjectile(spellViewInstance, sourceCreatureView, targetCreatureView);
        }
    }
}
