using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightweightManagementSystem
{
    public abstract class VirtualManager : IManager
    {
        public VirtualManager()
        {

        }

        /// <summary>
        /// Initialize the manager.
        /// </summary>
        /// <returns> Whether the manager was initialized successfully. </returns>
        public bool Initialize()
        {
            if (CheckStatus()) // Check if the system is enabled
            {
                if (CoreBehaviour.AddManager(this)) // Ensure the manager was added
                {
                    return true;
                }
                else // Catch failure to add manager
                {
                    Debug.LogError("Unable to add manager " + GetType());
                }
            }

            return false;
        }

        /// <summary>
        /// Deinitialize the manager.
        /// </summary>
        /// <returns> Whether the manager was deinitialized successfully. </returns>
        public bool Deinitialize()
        {
            if (CheckStatus()) // Check if the system is enabled
            {
                if (CoreBehaviour.RemoveManager(this)) // Ensure the manager was removed
                {
                    return true;
                }
                else // Catch failrue to remove manager
                {
                    Debug.LogError("Unable to remove manager " + GetType());
                }
            }

            return false;
        }

        /// <summary>
        /// Check the status to see whether the lightweight management system is enabled or not.
        /// </summary>
        /// <returns></returns>
        private bool CheckStatus()
        {
            if (CoreBehaviour.IsCompileTimeEnabled()) // Ensure the system is enabled
            {
                return true;
            }
            else // System is disabled
            {
                Debug.LogError("Lightweight management system is disabled!");
                return false;
            }
        }

        public virtual void OnManagerRegistered(CoreBehaviour coreBehaviour) { }
        public virtual void OnManagerUnregistered() { }
    }
}