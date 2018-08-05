using System;
using System.Collections.Generic;
using System.Linq;


namespace ConsoleApp_Practical_tasks

{
    class Program
    {
        static void PrintArray<T>(ref T[] arr)
        {
            Array.ForEach(arr, item => Console.Write($"{item.ToString()}  "));
            Console.Write("\n");
        }

        static void PrintLinkedList<T>(ref LinkedList<T> _list)
        {
            foreach( T item in _list.ToArray()) Console.Write($"{item.ToString()}=>");
            Console.Write(" null\n");
        }

        static void DeleteSeqOfElByMovingOfTail<T>(ref T[] _arr, int startPos, int finPos)
        {  
            int amountForDel = finPos - startPos + 1;
            int arrSize = _arr.Length;
            int newSize = arrSize - amountForDel;
            int tailSize = arrSize - finPos - 1;
            int amountForMoving;

            if (tailSize > 0)
            {
                amountForMoving = (tailSize < amountForDel ? tailSize : amountForDel);
                int startPosForMoving = arrSize - amountForDel;
                Array.Copy(_arr, startPosForMoving, _arr, startPos, amountForMoving);
            }
            Array.Resize(ref _arr, newSize);
        }

        static void Practical_task_1()
        {
            //How do you effectively delete sequence of elements from the middle of array?
            Console.WriteLine("Practical task # 1");
            Console.WriteLine("Let's us suppose we have an array:");
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };            
            PrintArray(ref arr);

            int startPosForDel = 1;
            int finPosForDel = 3;

            Console.WriteLine($"and we need to delete elements from position {startPosForDel} to position {finPosForDel}");

            Console.WriteLine("If the array is not sorted we can simply to move a part of an array's tail into this range and then to reduce the array's size:");
            int[] arr1 = arr.ToArray();
            DeleteSeqOfElByMovingOfTail(ref arr1, startPosForDel, finPosForDel);
            PrintArray(ref arr1);

            Console.WriteLine("If the array is sorted, we can take elements before the range, which we need to delete, and concatinate they with elements that are after this range:");
            int[] arr2 = arr.ToArray();
            arr2 = arr2.Take(startPosForDel)
                       .Concat(arr2.Skip(finPosForDel + 1))
                       .ToArray();
            PrintArray(ref arr2);
        }

        private static int FindRemovedElementInSortedArr(ref int[] _arr) {
            int m,  n = _arr.Count();
            for (int i = 1; i < n - 1; i++)
            {
                m = i + 1;
                if (_arr[i + 1] - _arr[i] > 1) return m;
            }
            return -1;
        }

        private static int FindRemovedElementInUnsortedArr(ref int[] _arr)
        {
            int n = _arr.Count();
            int m = n + 1;
            bool[] map = new bool[m];

            foreach (int item in _arr) map[item] = true;

            return Array.FindIndex(map, item => item == false);
            // or
            //for (int i = 1; i < m; i++)
            //    if (!map[i]) return i;
            //return -1;
        }

        static void Practical_task_2()
        {
            //Given an array with 100 elements (numbers from 0 to 99). One element has been removed from the array.
            // How would you find the removed element? How would you solve this if the array is sorted, 
            // or the array is not sorted ?
            Console.WriteLine("Practical task # 2");
            Console.WriteLine("Let's us suppose we have an array with 100 elements (numbers from 0 to 99).");
            int n_max = 100;
            int[] arr = new int[n_max];
            for (int i = 0; i < n_max; i++) arr[i] = i;

            int removedElement = 55;
            Console.WriteLine($"For example, the element #{removedElement} has been removed from the array.");
            arr = arr.Take(removedElement)
                       .Concat(arr.Skip(removedElement + 1))
                       .ToArray();
            

            int n;
            Console.WriteLine("If the array is sorted,");
            n = FindRemovedElementInSortedArr(ref arr);
            Console.WriteLine($"removed element #{n}");

            Console.WriteLine("If the array is not sorted,");
            n = FindRemovedElementInUnsortedArr(ref arr);
            Console.WriteLine($"removed element #{n}");
        }


        struct Duplicate<T>
        {
            public T item;
            public int amount;

            public Duplicate(T i, int a)
            {
                item = i;
                amount = a;
            }

            new public string ToString()
            {
                string s = (amount == 0 ? "no" : amount.ToString());
                return $"{item} has {s} duplicate{(amount > 1 || amount == 0 ? "s" : "")}";
            }
        }

        static void PrintDuplicates<T>(ref T[] _arr) {
            T[] distinct = _arr.Distinct().ToArray();
            Console.WriteLine("duplicates:");

            //we can use foreach only:
            //foreach (T item in distinct) Console.WriteLine($"{item} has {_arr.Where(x => x.Equals(item)).Count() - 1} duplicate(s)");

            //or fill array of structures and print results via the ToString method of the structure
            Duplicate<T>[] duplicates = new Duplicate<T>[distinct.Count()];
            int i = -1;
            foreach (T item in distinct)
                duplicates[++i] = new Duplicate<T>(item, _arr.Where(x => x.Equals(item)).Count() - 1);
            foreach (Duplicate<T> d in duplicates) Console.WriteLine(d.ToString());
        }

        static void Practical_task_3()
        {
            //How do you find duplicates in array? How would you solve it for the array of chars?
            Console.WriteLine("Practical task # 3");
            Console.WriteLine("Let's us suppose we have an array of int:");
            int[] arr = { 0, 1, 1, 1, 2, 3, 2, 4, 5, 3, 6, 7, 7, 8 };
            PrintArray(ref arr);
            PrintDuplicates(ref arr);

            Console.WriteLine("And now let's us suppose we have an array of chars:");
            char[] _chars = { 'a','a', 'a', 'b', 'c', 'c', 'd', 'c', 'a', 'f', 'b', 'g'};
            PrintArray(ref _chars);
            PrintDuplicates(ref _chars);
        }

        static void Practical_task_4()
        {
            //How do you find the middle element of a singly linked list in one pass?
            Console.WriteLine("Practical task # 4");

            Console.WriteLine("Let's us suppose we have a singly linked list:");
            SingleLinkedList<int> mySingleLinkedList = new SingleLinkedList<int>();
            for (int i = 1; i < 6; i++) mySingleLinkedList.AddNode(i);
            Console.WriteLine(mySingleLinkedList.ToString());
            Console.WriteLine("We can find the middle element of this singly linked list in one pass in such way:");
            Console.WriteLine(mySingleLinkedList.GetMiddleElementInOnePass());

                //but in real life we can use class LinkedList, which is embeded into .Net ...
            //LinkedList<int> list1 = new LinkedList<int>();
            //for (int i = 1; i < 6; i++) list1.AddLast(i);
            //PrintLinkedList(ref list1);
            //Console.WriteLine("We can find the middle element of this list in such way:");
            //int middleElement = list1.ElementAt(list1.Count/2);
            //Console.WriteLine(middleElement);
        }

        static void Practical_task_5()
        {
            //How do you detect a loop in a singly linked list?
            Console.WriteLine("Practical task # 5");

            Console.WriteLine("Let's us suppose we have a singly linked list:");
            SingleLinkedListWithLoop<int> mySingleLinkedList = new SingleLinkedListWithLoop<int>();
            for (int i = 1; i < 6; i++) mySingleLinkedList.AddNode(i);
            Console.WriteLine(mySingleLinkedList.ToString());

            string hasLoop = (mySingleLinkedList.HasLoop() ? "": "no ");
            Console.WriteLine("Initially, it has {0}loop.", hasLoop);

            mySingleLinkedList.MakeLoop();
            hasLoop = (mySingleLinkedList.HasLoop() ? "yes, it has a loop" : "no, it has no a loop.");
            Console.WriteLine("After making of the loop, we check its' state once more:");
            Console.WriteLine(hasLoop);
        }

        static void Practical_task_6()
        {
            //Given an array of numbers: 20, 17, 30, 21, 45, 2, 18. Form a sorted binary tree diagram
            Console.WriteLine("Practical task # 6");

            BTNode root = null;
            BTree bst = new BTree();

            Console.WriteLine("Let's us suppose we have an array of int:");
            int[] arr = { 20, 17, 30, 21, 45, 2, 18 }; 
            PrintArray(ref arr);

            for (int i = 0; i < arr.Count(); i++)
                root = bst.Insert(root, arr[i]);
            Console.WriteLine("Binary tree is created:\n");

            bst.BuildDiagram(root);
        }


        static void Practical_task_7()
        {
            //Provide a recursive solution for the tree traversal algorithms for a binary tree
            Console.WriteLine("Practical task # 7");

            BTNode root = null;
            BTree bst = new BTree();

            Console.WriteLine("Let's us suppose we have an array of int:");
            int[] arr =  { 20, 17, 30, 21, 45, 2, 18}; 
            PrintArray(ref arr);

            for (int i = 0; i < arr.Count(); i++)
                root = bst.Insert(root, arr[i]);
            Console.WriteLine("Binary tree is created:\n");

            bst.BuildDiagram(root);

            Console.Write("Traversing...: ");
            bst.Traverse(root);
            Console.Write("\n\n");
        }

        static void Main(string[] args)
        {
            int task;
            string answ;
            do
            {
                Console.WriteLine("Input task's number, please (1-7) or 0 to exit");                
                answ = Console.ReadLine();
                Console.Clear();
                if (!int.TryParse(answ, out task)) continue;
                switch (task) {
                    case 0:
                        return ;
                    case 1:
                        Practical_task_1();
                        break;
                    case 2:
                        Practical_task_2();
                        break;
                    case 3:
                        Practical_task_3();
                        break;
                    case 4:
                        Practical_task_4();
                        break;
                    case 5:
                        Practical_task_5();
                        break;
                    case 6:
                        Practical_task_6();
                        break;
                    case 7:
                        Practical_task_7();
                        break;                    
                    default:
                        Console.WriteLine("It's an incorrect number! Repeat, please");
                        break;
                }

            } while (true);
        }
    }
}
