using System.Collections;
using System.Collections.Generic;

namespace LightweightManagementSystem
{
    /// <summary>
    /// A list wrapper to help prevent duplicate entries which prevents
    /// overall code re-use for the same general behaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UniqueStore<T> : IEnumerable<T>
    {
        private List<T> items;

        public UniqueStore()
        {
            items = new List<T>();
        }

        /// <summary>
        /// Add the given item to the store.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> Whether the item was added. </returns>
        public bool Add(T item)
        {
            if (!items.Contains(item)) // Don't allow duplicates
            {
                items.Add(item);
                return true;
            }
            else // Duplicate entry
            {
                return false;
            }
        }

        /// <summary>
        /// Remove the given item from the store.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> Whether the item was removed. </returns>
        public bool Remove(T item)
        {
            return items.Remove(item);
        }

        /// <summary>
        /// Check whether an item is contained within the store.
        /// </summary>
        /// <param name="item"></param>
        /// <returns> Whether the item was found. </returns>
        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}