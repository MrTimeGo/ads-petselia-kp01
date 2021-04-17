using System;


namespace lab7
{
    class Hashtable<TKey, TValue> : IHashtable<TKey, TValue>
    {
        class Entry
        {
            public TKey key;
            public TValue value;
            public bool isDeleted;
            
            public Entry(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
                this.isDeleted = false;
            }
        }

        private Entry[] table;
        private int size;
        private double loadness;

        public Hashtable()
        {
            this.table = new Entry[7];
            this.size = 0;
            this.loadness = 0;
        }
        
        private int GetHash(TKey key)
        {
            int hash1 = GetPrimaryHash(key);
            int hash2 = GetSecondaryHash(key);

            int i = 1;
            int hash;
            do
            {
                hash = (hash1 + i * hash2) % table.Length;
                i++;
            } 
            while (this.table[hash] != null && !this.table[hash].isDeleted && !this.table[hash].key.Equals(key));
            return hash;
        }
        private int GetPrimaryHash(TKey key)
        {
            int hashCode = key.GetHashCode();
            return hashCode % table.Length;
        }
        private int GetSecondaryHash(TKey key)
        {
            int hashCode = key.GetHashCode();
            return hashCode % (table.Length - 1) + 1;
        }
        public int Size
        {
            get { return this.size; }
        }

        public double LoadFactor
        {
            get { return this.loadness; }
        }

        public void Clear()
        {
            int capacity = this.table.Length;
            this.table = new Entry[capacity];
            this.size = 0;
            this.loadness = 0;
        }

        public bool Contains(TKey key)
        {
            return Find(key) != null; 
        }

        public TValue Find(TKey key)
        {
            if (IsEmpty())
            {
                return default;
            }

            int hash1 = GetPrimaryHash(key);
            int hash2 = GetSecondaryHash(key);

            int i = 1;
            int hash;
            do
            {
                hash = (hash1 + i * hash2) % table.Length;
                i++;
            } 
            while (this.table[hash] != null && !this.table[hash].key.Equals(key));

            if (this.table[hash] == null)
            {
                return default;
            }
            else
            {
                return this.table[hash].value;
            }
        }

        public void Insert(TKey key, TValue value)
        {
            int hash = GetHash(key);

            this.table[hash] = new Entry(key, value);
            this.size++;
            this.loadness = this.size / (double)this.table.Length;
        }

        public bool IsEmpty()
        {
            return this.size == 0;
        }

        public bool Remove(TKey key)
        {
            if (IsEmpty())
            {
                return false;
            }

            int hash = GetHash(key);

            if (this.table[hash] == null || this.table[hash].isDeleted)
            {
                return false;
            }
            else
            {
                this.table[hash].isDeleted = true;
                this.size--;
                this.loadness = this.size / (double)this.table.Length;
                return true;
            }
        }

        public void Rehash()
        {
            Entry[] oldTable = new Entry[this.table.Length];
            Array.Copy(this.table, oldTable, this.table.Length);

            this.table = new Entry[oldTable.Length * 2];
            this.size = 0;
            this.loadness = 0;
            for (int i = 0; i < oldTable.Length; i++)
            {
                if (oldTable[i] != null && !oldTable[i].isDeleted)
                {
                    Insert(oldTable[i].key, oldTable[i].value);
                }
            }
        }
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null && !table[i].isDeleted)
                { 
                    output += table[i].key + " - " + table[i].value + '\n';
                }
            }
            return output;
        }
    }
}
