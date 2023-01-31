using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidable
{
    void HandleCollision<T>(T collisionObject) where T : ICollidable;
}
