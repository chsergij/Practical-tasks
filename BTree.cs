using System;

namespace ConsoleApp_Practical_tasks
{
    class BTree
    {        
        private int cLeft;
        private int maxLevel = 0;
        private int currentLevel;
        private int TopOfDiagram = 0;

        private void WriteAtPos(int _left, int _top, string text)
        {
            int n = text.Length;
            if (_left < 0)
            {
                _left = Math.Abs(_left);
                if (_left > n) return;
                text = text.Substring(n - _left);
                _left = 0;
            }
            n += _left;
            if (n > Console.LargestWindowWidth)
            {
                if (Console.LargestWindowWidth > n)
                    return;
                else text = text.Substring(n - Console.LargestWindowWidth);
            }
            Console.SetCursorPosition(_left, _top);
            Console.Write(text);            
        }

        private void WriteAtPos(int _left, int _top, string text, ConsoleColor textColor)
        {
            ConsoleColor _currentColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor;
            WriteAtPos(_left, _top, text);
            Console.ForegroundColor = _currentColor;
        }

        private void SetConsoleColors(ConsoleColor _BackgroundColor, ConsoleColor _ForegroundColor)
        {
            Console.BackgroundColor = _BackgroundColor;
            Console.ForegroundColor = _ForegroundColor;
        }

        private void WriteAtPos(int _left, int _top, string text, bool inverted)
        {
            ConsoleColor _BackgroundColor = Console.BackgroundColor;
            ConsoleColor _ForegroundColor = Console.ForegroundColor;
            SetConsoleColors(ConsoleColor.DarkGray, ConsoleColor.White);
            WriteAtPos(_left, _top, text);
            SetConsoleColors(_BackgroundColor, _ForegroundColor);
        }

        public void BuildDiagram(BTNode root)
        {
            if (root == null) return;
            TopOfDiagram = Console.CursorTop;
            int w = (int) (2 * Math.Pow(2, maxLevel));
            ConsoleColor _BackgroundColor = Console.BackgroundColor;
            ConsoleColor _ForegroundColor = Console.ForegroundColor;
            try
            {
                cLeft = w;
                w = 2 * w + 2;
                if (w>Console.WindowWidth) Console.SetWindowSize(w, Console.WindowHeight);                     
                Traverse2(root, 0);
                Console.SetCursorPosition(0, TopOfDiagram + 2 * maxLevel + 2);
            }
            catch
            {
                Console.WriteLine("It's impossible to build diagram in Console: too mach levels");
            }
            finally
            {
                SetConsoleColors(_BackgroundColor, _ForegroundColor);
            }
        }

        private string RepeatChar(char _ch, int n)
        {
            string s = "", s1 = _ch.ToString();
            for (int i = 0; i < n; i++) s += s1;
            return s;
        }
        public void Traverse2(BTNode root, int level)
        {
            if (root == null) return;
            int _top = TopOfDiagram + 2 * level, _top2 = _top + 1;
            int _cLeft = cLeft;
            int cShift = (int) Math.Pow(2,(maxLevel-level));

            level++;            
            cLeft = _cLeft - cShift;  
            Traverse2(root.left, level);
            cLeft = _cLeft + cShift;
            Traverse2(root.right, level);
            cLeft = _cLeft;

            WriteAtPos(cLeft, _top, root.value.ToString(), true);
            string line = RepeatChar('_', cShift - 2);
            if (root.left != null)
            {
                WriteAtPos(cLeft - cShift+1, _top2, "/", ConsoleColor.DarkGray);
                WriteAtPos(cLeft - cShift + 2, _top, line, ConsoleColor.DarkGray);
            }
            if (root.right != null)
            {
                WriteAtPos(cLeft + cShift, _top2, "\\", ConsoleColor.DarkGray);
                WriteAtPos(cLeft+2, _top, line, ConsoleColor.DarkGray);
            }
        }

        public BTNode Insert(BTNode root, int v)
        {
            currentLevel = 0;
            BTNode result;
            result = Insert2(root, v);
            return result;
        }

        private void incCurrentLevel()
        {
            currentLevel++;
            if (currentLevel > maxLevel) maxLevel = currentLevel;
        }

        public BTNode Insert2(BTNode root, int v)
        {
            if (root == null)
            {
                root = new BTNode();
                root.value = v;
            }
            else
            {
                incCurrentLevel();
                if (v < root.value) root.left = Insert2(root.left, v);
                else root.right = Insert2(root.right, v);
            }
            return root;
        }
        public void Traverse(BTNode root)
        {
            if (root == null) return;
            Traverse(root.left);            
            Traverse(root.right);
            Console.Write(root.value.ToString()+"  ");
        }
    }
}
