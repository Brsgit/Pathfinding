using System.Collections.Generic;
using System;

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> _data;

    public int Count => _data.Count;

    public PriorityQueue()
    {
        this._data = new List<T>();
    }

    public void Enqueue(T item)
    {
        _data.Add(item);

        var childIndex = _data.Count - 1;

        while(childIndex > 0)
        {
            var parentIndex = (childIndex - 1) / 2;

            if (_data[childIndex].CompareTo(_data[parentIndex]) >= 0) break;

            T temp = _data[parentIndex];
            _data[parentIndex] = _data[childIndex];
            _data[childIndex] = temp;

            childIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        var lastIndex = _data.Count - 1;

        T frontItem = _data[0];

        _data[0] = _data[lastIndex];

        _data.RemoveAt(lastIndex);
        lastIndex--;

        var parentIndex = 0;

        while (true)
        {
            int childIndex = parentIndex * 2 + 1;

            if (childIndex > lastIndex) break;

            var rightIndex = childIndex + 1;

            if(rightIndex <= lastIndex && _data[rightIndex].CompareTo(_data[childIndex]) < 0)
            {
                childIndex = rightIndex;
            }

            if (_data[parentIndex].CompareTo(_data[childIndex]) <= 0) break;

            T temp = _data[parentIndex];
            _data[parentIndex] = _data[childIndex];
            _data[childIndex] = temp;

            parentIndex = childIndex;
        }

        return frontItem;
    }

    public T Peek()
    {
        return _data[0];
    }

    public bool Contains(T item)
    {
        return _data.Contains(item);
    }

    public List<T> ToList()
    {
        return _data;
    }
}
