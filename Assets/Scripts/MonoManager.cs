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

        private void Awake()
        {
            coreBehaviour = CoreBehaviour.Instance;
            if(coreBehaviour != null)
            {
                coreBehaviour.AddManager(this);
            }
            else // Core behaviour doesn't exist yet
            {
                Debug.LogError("Core behaviour doesn't exist!");
            }
        }

        private void OnDestroy()
        {
            if(coreBehaviour != null) // Ensure the core behaviour is registered
            {
                coreBehaviour.RemoveManager(this);
            }
        }
    }
}