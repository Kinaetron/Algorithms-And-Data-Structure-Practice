using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data_Structures
{
    public class DoubleLinkedList<T>
    {
        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;


        public DoubleLinkedNode<T> Head { get; private set; }

        public DoubleLinkedNode<T> Tail { get; private set; }

        private void AddFirst(DoubleLinkedNode<T> node)
        {
            var temp = Head;

            Head.Next = temp;

            Head = node;

            if (IsEmpty) {
                Tail = Head;
            }
            else {
                // need to check on this line, too tired to do it now
                Head.Previous = temp;
            }

            Count++;
        }
    }
}
