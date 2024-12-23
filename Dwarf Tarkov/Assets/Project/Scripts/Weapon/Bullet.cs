using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, ICollidable
{
    public void HandleCollision<T>(T collisionObject) where T : ICollidable
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.GetComponent<PlayerInputHandler>() == null && other.GetComponent<BulletHandler>() == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerInputHandler>() == null && other.gameObject.GetComponent<BulletHandler>() == null)
            Destroy(gameObject);
    }
}
