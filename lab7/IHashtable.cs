using System;


namespace lab7
{
    interface IHashtable<TKey, TValue>
    {
        void Insert(TKey key, TValue value);
        bool Remove(TKey key);
        TValue Find(TKey key);

        bool Contains(TKey key);
        bool IsEmpty();
        int Size { get; }
        double LoadFactor { get; }
        void Clear();

        void Rehash();
    }
}
