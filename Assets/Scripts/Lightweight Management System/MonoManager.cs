using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightweightManagementSystem
{
    public abstract class MonoManager : MonoBehaviour, IManager
    {
        private CoreBehaviour coreBehaviour;

        public virtual void OnManagerRegistered(CoreBehaviour coreBehaviour) { }
        public virtual void OnManagerUnregistered() { }

        protected void Awake()
        {
            coreBehaviour = CoreBehaviour.Instance;
            if (coreBehaviour != null) // If the core behaviour exists
            {
                coreBehaviour.AddManager(this);
            }
            else // Core behaviour doesn't exist yet
            {
                Debug.LogError("Unable to register, Core behaviour doesn't exist!");
            }

            // Continue calls to derived object
            PostAwake();
        }
        protected void OnDestroy()
        {
            if (coreBehaviour != null) // Ensure the core behaviour is registered
            {
                coreBehaviour.RemoveManager(this);
            }

            // Continue calls to derived object
            PostDestroy();
        }

        protected virtual void PostAwake() { }
        protected virtual void PostDestroy() { }
    }
}