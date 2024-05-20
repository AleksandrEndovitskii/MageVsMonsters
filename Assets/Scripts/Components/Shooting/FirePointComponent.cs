using System;
using System.Collections;
using UnityEngine;

namespace MageVsMonsters.Components.Shooting
{
    public class FirePointComponent : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private GameObject bulletPrefab;
#pragma warning restore 0649

        private float _bulletForce;

        private float _secondsCount;

        private Coroutine _shootingCoroutine;

        private void Awake()
        {
            _bulletForce = 5f;

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
            var bulletInstance = Instantiate(
                bulletPrefab,
                this.gameObject.transform.position,
                this.gameObject.transform.rotation);

            var bulletInstanceRigidbody = bulletInstance.GetComponent<Rigidbody>();

            bulletInstanceRigidbody.AddForce(
                this.gameObject.transform.forward * _bulletForce,
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
