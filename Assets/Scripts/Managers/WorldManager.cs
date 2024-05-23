using System;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Helpers;
using MageVsMonsters.Views;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class WorldManager : BaseManager<WorldManager>
    {
        [SerializeField]
        private WorldBordersView _worldBordersViewPrefab;
        [SerializeField]
        private WorldBordersView _worldBordersViewInstance;

        public event Action<Vector3> WorldSizeChanged = delegate {};
        public Vector3 WorldSize
        {
            get => _worldSize;
            set
            {
                if (_worldSize == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_worldSize} -> {value}");
                _worldSize = value;

                WorldSizeChanged.Invoke(_worldSize);
            }
        }
        private Vector3 _worldSize = Vector3.zero;
        public event Action<Vector3> BorderSizeChanged = delegate {};
        public Vector3 BorderSize
        {
            get => _borderSize;
            set
            {
                if (_borderSize == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                          $"\n{_borderSize} -> {value}");
                _borderSize = value;

                BorderSizeChanged.Invoke(_borderSize);
            }
        }
        private Vector3 _borderSize = Vector3.zero;

        protected override async UniTask Initialize()
        {
            WorldSize = new Vector3(60, 60, 60);
            BorderSize = new Vector3(20, 0, 20);

            _worldBordersViewInstance = Instantiate(_worldBordersViewPrefab, this.gameObject.transform);
            _worldBordersViewInstance.transform.position = new Vector3(
                WorldSize.x / 2,
                0,
                WorldSize.z / 2);

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
    }
}
