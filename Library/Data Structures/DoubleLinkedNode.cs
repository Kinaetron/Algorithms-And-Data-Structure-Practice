namespace Library.Data_Structures
{
    public class DoubleLinkedNode<T>
    {
        public DoubleLinkedNode<T> Next { get; internal set; }

        public DoubleLinkedNode<T> Previous { get; internal set; }

        public T Value { get; set; }

        public DoubleLinkedNode(T value) {
            Value = value;
        }
    }
}
