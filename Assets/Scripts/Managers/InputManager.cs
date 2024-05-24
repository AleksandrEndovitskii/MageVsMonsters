using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
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
            // TODO: temp solution - reimplement through new InputSystem
            if (Input.GetKeyDown(KeyCode.X))
            {
                KeyPressed.Invoke(KeyCode.X);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                KeyPressed.Invoke(KeyCode.E);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                KeyPressed.Invoke(KeyCode.Q);
            }
        }
    }
}
