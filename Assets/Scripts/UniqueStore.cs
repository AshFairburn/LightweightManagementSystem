using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueStore<T> : IEnumerable<T>
{
    private List<T> items;

    public UniqueStore()
    {
        items = new List<T>();
    }

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

    public bool Remove(T item)
    {
        return items.Remove(item);
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