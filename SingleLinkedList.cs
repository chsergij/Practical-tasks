
namespace ConsoleApp_Practical_tasks
{
    public class SingleLinkedList<T>
    {
        public SingleLinkedListNode<T> Head { get; private set; }
        protected SingleLinkedListNode<T> Last;

        public T GetMiddleElementInOnePass() {
            SingleLinkedListNode<T> leftPointer = Head;
            SingleLinkedListNode<T> rightPointer = Head;

            leftPointer = Head;
            while (rightPointer != null && rightPointer.Next != null)
            {
                rightPointer = rightPointer.Next.Next;
                leftPointer = leftPointer.Next;
            }
            return leftPointer.Value;
        }

        public void AddNode(T value)
        {
            SingleLinkedListNode<T> oldLast = Last;
            Last = new SingleLinkedListNode<T>(value);

            if (Head == null) Head = Last;
            else oldLast.Next = Last;
        }

        new public string ToString()
        {
            SingleLinkedListNode<T> pointer = Head;
            string s = "";
            while (pointer != null)
            {
                s += pointer.Value.ToString() + "=>";
                pointer = pointer.Next;
            }
            s += "null";
            return s;
        }
    }
}
