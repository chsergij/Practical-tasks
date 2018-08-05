
namespace ConsoleApp_Practical_tasks
{
    public class SingleLinkedListNode<T>
    {
        public T Value { get; private set; }
        public SingleLinkedListNode<T> Next { get; set; }

        public SingleLinkedListNode(T _value)
        {
            Value = _value;
            //Next = null;
        }
    }
}
