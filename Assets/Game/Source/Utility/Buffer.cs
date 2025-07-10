using System;
using Game.Gameplay.EntitiesCore;

namespace Game.Utility
{
    public class Buffer<T> where T : class
    {
        public T[] Items;
        public int Count;

        public Buffer(int initialSize)
        {
            Items = new T[initialSize];
            Count = 0;
        }

        public void Add(T item)
        {
            Items[Count] = item;
            Count++;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
                if (Items[i] == item)
                    return true;
            
            return false;
        }
        
        public bool TryRemove(T item)
        {
            int indexToRemove = -1;

            for (int i = 0; i < Count; i++)
            {
                if (Items[i].Equals(item))
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove >= 0)
            {
                for (int i = indexToRemove; i < Count - 1; i++)
                {
                    Items[i] = Items[i + 1];
                }

                Count--;

                return true;
            }
            
            return false;
        }
    }
}