using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Helpers;
using MageVsMonsters.Views;
using Newtonsoft.Json;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Managers
{
    public class ProjectilesManager : BaseManager<ProjectilesManager>
    {
        // TODO: temp solution - get this data from model
#pragma warning disable 0649
        [SerializeField]
        private float _projectileSpeed = 5f;
#pragma warning restore 0649

        public List<FirePointView> Instances = new List<FirePointView>();

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

        // TODO: copy/paste from CreaturesManager.cs
        public void Register(FirePointView viewInstance)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}"
                      + $"\n{nameof(viewInstance)}.{nameof(viewInstance.Model)} == {JsonConvert.SerializeObject(viewInstance.Model)}");

            Instances.Add(viewInstance);
        }
        public void UnRegister(FirePointView viewInstance)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}"
                      + $"\n{nameof(viewInstance)}.{nameof(viewInstance.Model)} == {JsonConvert.SerializeObject(viewInstance.Model)}");

            Instances.Remove(viewInstance);
        }

        public void SendProjectile(MonoBehaviour projectileViewInstance, CreatureView sourceCreatureView, CreatureView targetCreatureView)
        {
            var firePointView = Instances.FirstOrDefault(x =>
                x.Model.CreatureViewModel == sourceCreatureView.Model);
            if (firePointView == null)
            {
                Debug.LogWarning($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}_Aborted"
                                 + $"\n{nameof(firePointView)} == {firePointView}");

                return;
            }

            // set projectile initial position
            projectileViewInstance.gameObject.transform.position = firePointView.gameObject.transform.position;

            // send projectile to target
            var projectileInstanceRigidbody = projectileViewInstance.GetComponent<Rigidbody>();
            var directionFromSourceToTarget = firePointView.gameObject.transform.forward;
            //var directionFromSourceToTarget = (targetCreatureView.transform.position - creatureView.transform.position).normalized;
            projectileInstanceRigidbody.AddForce(directionFromSourceToTarget * _projectileSpeed, ForceMode.Impulse);
        }
    }
}
