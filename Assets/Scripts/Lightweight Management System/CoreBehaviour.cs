using System;
using System.Collections.Generic;

namespace LightweightManagementSystem
{
    /// <summary>
    /// An interface to allow listening to the management system. Notifications
    /// are sent out to listeners when a manager is added or removed from the
    /// system.
    /// </summary>
    public interface IManagementListener
    {
        void OnManagerRegistered(IManager manager);
        void OnManagerUnregistered(IManager manager);
    }

    /// <summary>
    /// The central behaviour of the system. Only one of these should ever exist in memory and is accessible
    /// through the singleton. Most the functionality is also wrapped into static behaviours to remove the need
    /// for direct access to the instance.
    /// </summary>
    public class CoreBehaviour
    {
        private UniqueStore<IManager> managers;
        private UniqueStore<IManagementListener> listeners;

        private CoreBehaviour()
        {
            managers = new UniqueStore<IManager>();
            listeners = new UniqueStore<IManagementListener>();
        }

        /// <summary>
        /// Add the given listener.
        /// </summary>
        /// <param name="listener"></param>
        /// <returns> Whether the listener was added. </returns>
        public bool AddListener(IManagementListener listener)
        {
            return listeners.Add(listener);
        }

        /// <summary>
        /// Remove the given listener.
        /// </summary>
        /// <param name="listener"></param>
        /// <returns> Whether the listener was removed. </returns>
        public bool RemoveListener(IManagementListener listener)
        {
            return listeners.Remove(listener);
        }

        /// <summary>
        /// Notify all listeners that a manager has been added.
        /// </summary>
        /// <param name="manager"></param>
        private void NotifyManagerAdded(IManager manager)
        {
            foreach (IManagementListener listener in listeners)
            {
                listener.OnManagerRegistered(manager);
            }
        }

        /// <summary>
        /// Notify all listeners that a manager has been removed.
        /// </summary>
        /// <param name="manager"></param>
        private void NotifyManagerRemoved(IManager manager)
        {
            foreach (IManagementListener listener in listeners)
            {
                listener.OnManagerUnregistered(manager);
            }
        }

        /// <summary>
        /// Add a manager to the system.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns> Whether the manager was added. </returns>
        public bool Add(IManager manager)
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

        /// <summary>
        /// Remove a manager from the system.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns> Whether the manager was removed. </returns>
        public bool Remove(IManager manager)
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

        /// <summary>
        /// Get the first instance of a manager matching the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns> The manager, if found, or the default of the given type. </returns>
        private T GetFirst<T>() where T : IManager
        {
            Type tType = typeof(T);

            foreach (IManager manager in managers) // Iterate all managers
            {
                if (manager.GetType() == tType) // Check for a type match
                {
                    return (T)manager;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Get all managers matching the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns> The number of managers added to the given list. </returns>
        private int GetAll<T>(List<T> items) where T : IManager
        {
            int count = 0;

            if (items != null) // Ensure the given list isn't NULL
            {
                Type tType = typeof(T);
                foreach (IManager manager in managers) // Iterate all managers
                {
                    if (manager.GetType() == tType) // Check for a type match
                    {
                        items.Add((T)manager);
                        count++;
                    }
                }
            }

            return count;
        }

        // ------------------------------------------------------------------------------------------
        // Static behaviour
        // ------------------------------------------------------------------------------------------

        private static CoreBehaviour instance;
        public static CoreBehaviour Instance { get { return instance; } }

        /// <summary>
        /// Add a manager to the system.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns> Whether the manager was added. </returns>
        public static bool AddManager(IManager manager)
        {
            if (instance != null) // Ensure the instance exists
            {
                return instance.Add(manager);
            }
            else // Instance doesn't exist, cannot add manager
            {
                return false;
            }
        }

        /// <summary>
        /// Remove a manager from the system.
        /// </summary>
        /// <param name="manager"></param>
        /// <returns> Whether the manager was removed. </returns>
        public static bool RemoveManager(IManager manager)
        {
            if (instance != null) // Ensure the instance exists
            {
                return instance.Remove(manager);
            }
            else // Instance doesn't exist, cannot remove manager
            {
                return false;
            }
        }

        /// <summary>
        /// Create the core behaviour instance, this must be called for the management system to function.
        /// </summary>
        public static void CreateCoreBehaviour()
        {
            if (instance == null) // Create if not existent
            {
                instance = new CoreBehaviour();
            }
        }

        /// <summary>
        /// Get the first instance of a manager matching the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns> The manager, if found, or the default of the given type. </returns>
        public static T GetFirstManager<T>() where T : IManager
        {
            if (instance != null) // Ensure the instance exists
            {
                return instance.GetFirst<T>();
            }
            else // Instance doesn't exist, cannot get manager
            {
                return default(T);
            }
        }

        /// <summary>
        /// Get all managers matching the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns> The number of managers added to the given list. </returns>
        public static int GetAllManagers<T>(List<T> items) where T : IManager
        {
            if (instance != null) // Ensure the instance exists
            {
                return instance.GetAll<T>(items);
            }
            else // Instance doesn't exist, cannot get managers
            {
                return 0;
            }
        }

        /// <summary>
        /// Is the management system compile-time defined? The system requires
        /// the definition of 'LWMS' to function.
        /// </summary>
        /// <returns></returns>
        public static bool IsCompileTimeEnabled()
        {
#if LWMS
            return true;
#else
            return false;
#endif
        }
    }
}