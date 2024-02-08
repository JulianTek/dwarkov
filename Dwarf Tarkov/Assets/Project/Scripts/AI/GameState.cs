using UnityEngine;
namespace AI
{
    public abstract class GameState
    {
        protected GameObject owner;

        public GameState()
        {
        }

        public GameState(GameObject owner)
        {
            this.owner = owner;
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Stop();

        public GameObject GetOwner() {
            return owner;
        }
    }
}
