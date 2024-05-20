using System;
using System.Collections;
using MageVsMonsters.Models;
using MageVsMonsters.Views;
using MageVsMonsters.Views.Extensions;
using UnityEngine;

namespace MageVsMonsters.Components.Shooting
{
    public class FirePointComponent : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private ProjectileView _projectileViewPrefab;
#pragma warning restore 0649

        private float _projectileSpeed;

        private float _secondsCount;

        private Coroutine _shootingCoroutine;

        private void Awake()
        {
            _projectileSpeed = 5f;

            _secondsCount = 1f;

            StartShooting();
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
            projectileViewInstance.Model = projectileModel;

            var bulletInstanceRigidbody = projectileViewInstance.GetComponent<Rigidbody>();
            bulletInstanceRigidbody.AddForce(
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
