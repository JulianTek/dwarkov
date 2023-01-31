using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace Collisions
{
    public class EnemyCollisionHandler : MonoBehaviour, ICollidable
    {
        public void HandleCollision<T>(T collisionObject) where T : ICollidable
        {
            if (collisionObject is Bullet)
            {
                HandleBulletCollision(collisionObject as Bullet);
            }
        }

        private void HandleBulletCollision(Bullet bullet)
        {
            float damage = bullet.gameObject.GetComponent<BulletHandler>().ammoType.Damage;
            EventChannels.EnemyEvents.OnEnemyTakesDamage?.Invoke(damage);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ICollidable collidable = other.gameObject.GetComponent<ICollidable>();
            if (collidable != null)
            {
                HandleCollision(collidable);
            }
        }
    }
}
