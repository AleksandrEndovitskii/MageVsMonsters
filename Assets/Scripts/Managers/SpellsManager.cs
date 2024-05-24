using System;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
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

                _currentSpell = value;

                CurrentSpellChanged.Invoke(_currentSpell);
            }
        }
        private SpellModel _currentSpell;
        
        protected override async UniTask Initialize()
        {
            CurrentSpell = new SpellModel(10);

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
        public void CastSpell(SpellModel spellModel = null, CreatureView sourceCreatureView = null, CreatureView targetCreatureView = null)
        {
            spellModel = new SpellModel(CurrentSpell.Damage);
            var spellViewInstance = this.InstantiateElement(spellModel, _spellViewPrefab, this.gameObject.transform);
            ProjectilesManager.Instance.SendProjectile(spellViewInstance, sourceCreatureView, targetCreatureView);
        }
    }
}
