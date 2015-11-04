using System;
using System.Collections;
using System.Collections.Generic;

namespace Task3.Queue {

    public class CustomQueue<T> : IEnumerable<T> {
        #region Private Fields
        private T[] m_Array;
        private int m_Head;
        private int m_Tail;
        private int m_Version;

        private const int m_DefaultCapacity = 4;
        #endregion

        #region Constructors
        public CustomQueue() {
            m_Array = new T[0];
        }

        public CustomQueue(int capacity) {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Need non-negative capacity required");

            m_Array = new T[capacity];
            m_Head = 0;
            m_Tail = 0;
            Count = 0;
        }

        public CustomQueue(IEnumerable<T> collection) {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            m_Array = new T[m_DefaultCapacity];
            Count = 0;
            m_Version = 0;

            using (IEnumerator<T> en = collection.GetEnumerator()) {
                while (en.MoveNext()) {
                    Enqueue(en.Current);
                }
            }
        }
        #endregion

        public int Count { get; private set; }

        #region Public Methods
        /// <summary>Removes all objects from the queue</summary>
        public void Clear() {
            if (m_Head < m_Tail)
                Array.Clear(m_Array, m_Head, Count);
            else {
                Array.Clear(m_Array, m_Head, m_Array.Length - m_Head);
                Array.Clear(m_Array, 0, m_Tail);
            }

            m_Head = 0;
            m_Tail = 0; 
            Count = 0;
            m_Version++;
        }
        
        public IEnumerator<T> GetEnumerator() => new Enumerator(this);

        /// <summary>Adds item to the tail of the queue</summary>
        /// <param name="item">Item for add</param>
        public void Enqueue(T item) {
            if (Count == m_Array.Length) {
                int newcapacity = m_Array.Length * 2;
                SetCapacity(newcapacity);
            }

            m_Array[m_Tail] = item;
            m_Tail = (m_Tail + 1) % m_Array.Length;
            Count++;
            m_Version++;
        }

        /// <summary>Removes the item from the head of the queue ant returns it</summary>
        /// <returns>The dequeued item</returns>
        public T Dequeue() {
            if (Count == 0)
                throw new InvalidOperationException("CustomQueue is empty");

            T removed = m_Array[m_Head];
            m_Array[m_Head] = default(T);
            m_Head = (m_Head + 1) % m_Array.Length;
            Count--;
            m_Version++;
            return removed;
        }

        /// <summary>Returns the item at the head of queue</summary>
        /// <returns>The item at the head</returns>
        public T Peek() {
            if (Count == 0)
                throw new InvalidOperationException("CustomQueue is empty");

            return m_Array[m_Head];
        }

        internal T GetElement(int i) {
            return m_Array[(m_Head + i) % m_Array.Length];
        }
        #endregion

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        private void SetCapacity(int capacity) {
            T[] newarray = new T[capacity];
            if (Count > 0) {
                if (m_Head < m_Tail) {
                    Array.Copy(m_Array, m_Head, newarray, 0, Count);
                } else {
                    Array.Copy(m_Array, m_Head, newarray, 0, m_Array.Length - m_Head);
                    Array.Copy(m_Array, 0, newarray, m_Array.Length - m_Head, m_Tail);
                }
            }

            m_Array = newarray;
            m_Head = 0;
            m_Tail = (Count == capacity) ? 0 : Count;
            m_Version++;
        }
        
        /// <summary>
        /// The enumerator checks that no modifications are made to the list while
        /// an enumeration is in progress.
        /// </summary>
        public struct Enumerator : IEnumerator<T> {
            private readonly CustomQueue<T> m_Q;
            private int m_Index;   // -1 = not started, -2 = ended/disposed
            private readonly int m_Version;
            private T m_CurrentElement;

            internal Enumerator(CustomQueue<T> q) {
                m_Q = q;
                m_Version = m_Q.m_Version;
                m_Index = -1;
                m_CurrentElement = default(T);
            }

            public void Dispose() {
                m_Index = -2;
                m_CurrentElement = default(T);
            }

            public bool MoveNext() {
                if (m_Version != m_Q.m_Version)
                    throw new InvalidOperationException("Enumerator failed version");

                if (m_Index == -2)
                    return false;

                m_Index++;

                if (m_Index == m_Q.Count) {
                    m_Index = -2;
                    m_CurrentElement = default(T);
                    return false;
                }

                m_CurrentElement = m_Q.GetElement(m_Index);
                return true;
            }

            public T Current {
                get {
                    if(m_Index >= 0)
                        return m_CurrentElement;
                    if (m_Index == -1)
                        throw new InvalidOperationException("Enumerator not started");
                    throw new InvalidOperationException("Enumerator ended");
                }
            }

            public void Reset() {
                if (m_Version != m_Q.m_Version)
                    throw new InvalidOperationException("Enumerator failed version");
                m_Index = -1;
                m_CurrentElement = default(T);
            }

            object IEnumerator.Current => Current;
        }
    }
}