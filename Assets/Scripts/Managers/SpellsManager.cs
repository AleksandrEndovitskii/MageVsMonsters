using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Helpers;
using MageVsMonsters.JsonObjects;
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

        protected string DefinitionPath { get; } = Path.Combine("Definitions", "Spells");
        protected string PrefabsPath { get; } = Path.Combine("Prefabs", "GameObjects", "Spells");

        private List<SpellDefinitionJsonObject> _definitionJsonObjects;
        private Dictionary<string, SpellView> _namePrefabs;
        private List<SpellModel> _spellModels;

        protected override async UniTask Initialize()
        {
            _definitionJsonObjects = DefinitionsHelper.GetDefinitions<SpellDefinitionJsonObject>(DefinitionPath);
            _namePrefabs = PrefabsHelper.LoadPrefabsAsDictionary<SpellView>(PrefabsPath);

            _spellModels = new List<SpellModel>();
            foreach (var definitionJsonObject in _definitionJsonObjects)
            {
                _spellModels.Add(new SpellModel(definitionJsonObject));
            }
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

        public void CastSpell(CreatureView sourceCreatureView = null, CreatureView targetCreatureView = null)
        {
            var spellModel = new SpellModel(CurrentSpell);
            var spellViewPrefab = _namePrefabs[spellModel.Name];
            var spellViewInstance = this.InstantiateElement(spellModel, spellViewPrefab, this.gameObject.transform);
            ProjectilesManager.Instance.SendProjectile(spellViewInstance, sourceCreatureView, targetCreatureView);
        }
    }
}
