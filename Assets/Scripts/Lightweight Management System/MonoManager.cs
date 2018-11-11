using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightweightManagementSystem
{
    public abstract class MonoManager : MonoBehaviour, IManager
    {
        public virtual void OnManagerRegistered(CoreBehaviour coreBehaviour) { }
        public virtual void OnManagerUnregistered() { }

        protected void Awake()
        {
            if (CheckCompileStatus()) // Check if the system is enabled
            {
                if (!CoreBehaviour.AddManager(this)) // Catch failure to add manager
                {
                    Debug.LogError("Unable to register manager " + GetType());
                }
            }

            // Continue calls to derived object
            PostAwake();
        }
        protected void OnDestroy()
        {
            if (CheckCompileStatus()) // Check if the system is enabled
            {
                if (!CoreBehaviour.RemoveManager(this)) // Catch failrue to remove manager
                {
                    Debug.LogError("Unable to remove manager " + GetType());
                }
            }

            // Continue calls to derived object
            PostDestroy();
        }

        private bool CheckCompileStatus()
        {
            if(CoreBehaviour.IsCompileTimeEnabled()) // Ensure the system is enabled
            {
                return true;
            }
            else // System is disabled
            {
                Debug.LogError("Lightweight management system is disabled!");
                return false;
            }
        }

        protected virtual void PostAwake() { }
        protected virtual void PostDestroy() { }
    }
}