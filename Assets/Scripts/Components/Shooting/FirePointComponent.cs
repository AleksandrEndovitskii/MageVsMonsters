using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using MageVsMonsters.Components.BaseComponents;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using UnityEngine;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace MageVsMonsters.Components.Shooting
{
    public class FirePointComponent : BaseComponent
    {
#pragma warning disable 0649
        [SerializeField]
        private ProjectileView _projectileViewPrefab;
#pragma warning restore 0649

        private float _projectileSpeed;

        private float _secondsCount;

        private Coroutine _shootingCoroutine;

        protected override async UniTask Initialize()
        {
            _projectileSpeed = 5f;

            _secondsCount = 1f;

            StartShooting();
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

        public void StartShooting()
        {
            if (_shootingCoroutine == null) // not started shooting yet - start it
            {
                _shootingCoroutine = StartCoroutine(RepeatActionEverySecondsCoroutine(
                    _secondsCount,
                    () => { Shoot(); }));
            }
        }
        public void StopShooting()
        {
            if (_shootingCoroutine != null) // started shooting - stop it
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
        }

        public void Shoot()
        {
            var projectileModel = new ProjectileModel();
            var projectileViewInstance = Instantiate(_projectileViewPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            //var projectileViewInstance = Instantiate(_projectileViewPrefab); // TODO: set parent to ProjectilesManager.Instance.gameObject.transform
            projectileViewInstance.Model = projectileModel;

            var projectileInstanceRigidbody = projectileViewInstance.GetComponent<Rigidbody>();
            projectileInstanceRigidbody.AddForce(
                this.gameObject.transform.forward * _projectileSpeed,
                ForceMode.Impulse);
        }

        private IEnumerator RepeatActionEverySecondsCoroutine(float secondsCount, Action action)
        {
            while (enabled)
            {
                yield return new WaitForSeconds(secondsCount);

                action.Invoke();
            }
        }
    }
}
