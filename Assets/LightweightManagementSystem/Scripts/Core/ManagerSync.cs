using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LightweightManagementSystem
{
    // TODO: Add dependencies, what if a manager requires another manager to exist? Consider how this will work using
    // the manager sync behaviour.

    public interface ISyncListener
    {
        void OnRequiredManagersSychronized();
    }

    public class ManagerSync : IManagementListener
    {
        private UniqueStore<Type> targetManagers;
        private UniqueStore<IManager> currentManagers;
        private UniqueStore<ISyncListener> listeners;

        public ManagerSync()
        {
            targetManagers = new UniqueStore<Type>();
            currentManagers = new UniqueStore<IManager>();
            listeners = new UniqueStore<ISyncListener>();
        }

        public void Initialize()
        {
            CoreBehaviour.AddCoreListener(this);
        }

        public void Deinitialize()
        {
            CoreBehaviour.RemoveCoreListener(this);
        }

        public bool AddListener(ISyncListener listener)
        {
            return listeners.Add(listener);
        }

        public bool RemoveListener(ISyncListener listener)
        {
            return listeners.Remove(listener);
        }

        public bool AddTarget<T>() where T : IManager
        {
            Type type = typeof(T);
            return targetManagers.Add(type);
        }

        public bool RemoveTarget<T>() where T : IManager
        {
            Type type = typeof(T);
            return targetManagers.Remove(type);
        }

        public void OnManagerRegistered(IManager manager)
        {
            Type type = manager.GetType();
            if (targetManagers.Contains(type)) // Ensure the given manager is of a type required by the search
            {
                if (!currentManagers.Contains(manager)) // Check if the manager isn't currently contained
                {
                    currentManagers.Add(manager);
                    CheckStatus();
                }
            }
        }

        public void OnManagerUnregistered(IManager manager)
        {
            Type type = manager.GetType();
            if (currentManagers.Remove(manager))
            {
                CheckStatus();
            }
        }

        private void CheckStatus()
        {
            foreach(Type type in targetManagers)
            {
                bool flag = false;

                foreach(IManager manager in currentManagers)
                {
                    if(manager.GetType() == type)
                    {
                        flag = true;
                    }
                }

                if(!flag)
                {
                    return;
                }
            }

            // All managers match the current ones, notify!
            foreach (ISyncListener listener in listeners)
            {
                listener.OnRequiredManagersSychronized();
            }

            // Auto-deinitialize to prevent multiple messages being conveyed
            Deinitialize();
        }
    }
}