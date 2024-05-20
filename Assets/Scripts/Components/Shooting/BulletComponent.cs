using MageVsMonsters.Views;
using UnityEngine;

namespace MageVsMonsters.Components.Shooting
{
    [RequireComponent(typeof(Collider))]
    public class BulletComponent : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            // instantiate effect of hit here
            // destroy effect of hit here

            var creatureView = other.gameObject.GetComponent<CharacterView>();
            if (creatureView != null)
            {
                // do damage to creature
            }

            //Destroy(this.gameObject);
        }
    }
}
