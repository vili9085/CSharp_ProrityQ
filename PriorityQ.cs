using System.Collections;
using System.Collections.Generic;
using System;


public class PriorityQ<T> where T : IComparable<T>
{

    private static readonly int DEFAULT_CAPACITY = 20;
    private readonly int numChildren;
    public int Size { get; private set; }
    private T[] _array;

    public PriorityQ()
    {
        numChildren = 2;
        Size = 0;
        _array = new T[DEFAULT_CAPACITY + 1];
    }

    public PriorityQ(int numChildren)
    {
        if (numChildren < 2)
        {
            throw new System.Exception("You do not have enough kids!");
        }
        this.numChildren = numChildren;
        Size = 0;
        _array = new T[DEFAULT_CAPACITY + 1];
    }

    public PriorityQ(T[] elements)
    {
        Size = 0;
        numChildren = 2;
        int sizeOfArgumentArray = elements.Length * 2 + 1;
        _array = new T[sizeOfArgumentArray];
        foreach (T element in elements)
        {
            _array[++Size] = element;
        }
        BuildHeap();
    }


    private void BuildHeap()
    {
        for (int i = Size / 2; i > 0; i--)
        {
            PercolateDown(i);
        }

    }
    public void Insert(T element)
    {
        if (Size == _array.Length - 1)
        {
            EnlargeArray(_array.Length * 2 + 1);
        }

        int hole = ++Size;

        for (_array[0] = element; hole > 1 && element.CompareTo(_array[GetParentIndex(hole)]) < 0; hole = GetParentIndex(hole))
        {
            _array[hole] = _array[GetParentIndex(hole)];
        }

        _array[hole] = element;
    }


    public void EnlargeArray(int newSize)
    {
        T[] tmp = _array;
        _array = new T[newSize];
        for (int i = 0; i < tmp.Length; i++)
        {
            _array[i] = tmp[i];
        }

    }

    public int GetParentIndex(int index)
    {
        if (index <= 1)
        {
            throw new System.Exception("You aint got a parent");
        }
        return (index + (numChildren - 2)) / numChildren;
    }

    public int GetFirstChildIndex(int index)
    {
        if (index < 1)
        {
            throw new System.Exception("No Child");
        }

        return index * numChildren - (numChildren - 2);

    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new System.Exception("Q is Empty");
        }
        return _array[1];
    }
    public T Pop()
    {
        if (IsEmpty())
        {
            throw new System.Exception("Q is Empty");
        }
        T itemToRemove = Peek();
        _array[1] = _array[Size--];
        PercolateDown(1);
        return itemToRemove;
    }

    private void PercolateDown(int hole)
    {
        T current = _array[hole];
        int smallestChild;
        for (; GetFirstChildIndex(hole) <= Size; hole = smallestChild)
        {
            smallestChild = GetFirstChildIndex(hole);
            int testVSsmallestChild = smallestChild + 1;
            for (int i = 0; i < numChildren && testVSsmallestChild <= Size; i++)
            {
                if (_array[testVSsmallestChild].CompareTo(_array[smallestChild]) < 0)
                {
                    smallestChild = testVSsmallestChild;
                }
                testVSsmallestChild++;
            }
            if (_array[smallestChild].CompareTo(current) < 0)
            {
                _array[hole] = _array[smallestChild];

            }
            else
            {
                break;
            }
        }
        _array[hole] = current;
    }

    public bool IsEmpty()
    {
        return Size == 0;
    }

    public void MakeEmpty()
    {
        Size = 0;
        _array = new T[DEFAULT_CAPACITY + 1];
    }


}
