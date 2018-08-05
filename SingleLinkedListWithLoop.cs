
namespace ConsoleApp_Practical_tasks
{
    class SingleLinkedListWithLoop<T> : SingleLinkedList<T>
    {
        public void MakeLoop()
        {
            this.Last.Next = this.Head;
        }

        public bool HasLoop()
        {
            SingleLinkedListNode<T> tempNode = Head;
            SingleLinkedListNode<T> tempNode1 = Head.Next;
            while (tempNode != null && tempNode1 != null)
            {
                if (tempNode.Equals(tempNode1)) return true;

                if ((tempNode1.Next != null) && (tempNode.Next != null))
                {
                    tempNode1 = tempNode1.Next.Next;
                    tempNode = tempNode.Next;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
