using System;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Managers;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters
{
    public class InputManager : BaseManager<InputManager>
    {
        public event Action<KeyCode> KeyPressed = delegate {};

        protected override async UniTask Initialize()
        {
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

        // TODO: temp solution - reimplement through InputSystem
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                KeyPressed.Invoke(KeyCode.X);
            }
        }
    }
}
