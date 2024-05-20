using System;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.BaseComponents;
using MageVsMonsters.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace MageVsMonsters.Views
{
    public class BaseView<T> : InitializableBaseMonoBehaviour, IView<T> where T : IModel
    {
        public event Action<T> ModelChanged = delegate { };
        public T Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (Equals(value, _model))
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{nameof(Model)}" +
                          $"\n{JsonConvert.SerializeObject(_model)} -> {JsonConvert.SerializeObject(value)}");

                _model = value;
                Redraw(_model);

                ModelChanged.Invoke(_model);
            }
        }
        private T _model;

        protected override async UniTask Initialize()
        {
        }

        protected override async UniTask UnInitialize()
        {
        }

        protected override async UniTask Subscribe()
        {
        }

        protected override async UniTask UnSubscribe()
        {
        }

        protected virtual void Redraw(T model)
        {
        }
    }
}
