using System;
using System.Collections.Generic;

namespace LightweightManagementSystem
{
    public interface IManagementListener
    {
        void OnManagerRegistered(IManager manager);
        void OnManagerUnregistered(IManager manager);
    }

    public class CoreBehaviour
    {
        private UniqueStore<IManager> managers;
        private UniqueStore<IManagementListener> listeners;

        private CoreBehaviour()
        {
            managers = new UniqueStore<IManager>();
            listeners = new UniqueStore<IManagementListener>();
        }

        public bool AddListener(IManagementListener listener)
        {
            return listeners.Add(listener);
        }

        public bool RemoveListener(IManagementListener listener)
        {
            return listeners.Remove(listener);
        }

        private void NotifyManagerAdded(IManager manager)
        {
            foreach (IManagementListener listener in listeners)
            {
                listener.OnManagerRegistered(manager);
            }
        }

        private void NotifyManagerRemoved(IManager manager)
        {
            foreach (IManagementListener listener in listeners)
            {
                listener.OnManagerUnregistered(manager);
            }
        }

        public bool AddManager(IManager manager)
        {
            if (manager != null) // Entry is not null
            {
                if (managers.Add(manager)) // Was addition successful?
                {
                    manager.OnManagerRegistered(this);
                    NotifyManagerAdded(manager);
                    return true;
                }
                else // Manager wasn't added
                {
                    return false;
                }
            }
            else // Null entry
            {
                return false;
            }
        }

        public bool RemoveManager(IManager manager)
        {
            if (managers.Remove(manager)) // Was removal successful?
            {
                manager.OnManagerUnregistered();
                NotifyManagerRemoved(manager);
                return true;
            }
            else // Manager wasn't removed
            {
                return false;
            }
        }

        public T GetFirst<T>() where T : IManager
        {
            Type tType = typeof(T);

            foreach (IManager manager in managers)
            {
                if (manager.GetType() == tType)
                {
                    return (T)manager;
                }
            }

            return default(T);
        }

        public int GetAll<T>(List<T> items) where T : IManager
        {
            Type tType = typeof(T);
            int count = 0;

            foreach (IManager manager in managers)
            {
                if (manager.GetType() == tType)
                {
                    items.Add((T)manager);
                    count++;
                }
            }

            return count;
        }

        // Static behaviour

        private static CoreBehaviour instance;
        public static CoreBehaviour Instance { get { return instance; } }

        public static void CreateCoreBehaviour()
        {
            if (instance == null) // Create if not existent
            {
                instance = new CoreBehaviour();
            }
        }
    }
}