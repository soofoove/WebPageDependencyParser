using System;
using System.Collections;
using System.Collections.Generic;

namespace WebPageDependencyParser
{
    public class TreeNode<T>
    {
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();
        public T Value { get; set; }

        public TreeNode(T value)
        {
            Value = value;
        }
        public void AddChild(TreeNode<T> node)
        {
            Children.Add(node);
        }

        public static void Print(TreeNode<T> tree)
        {
            PrintWithSpaces(tree, 0);
        }

        private static void PrintWithSpaces(TreeNode<T> node, int level)
        {
            for (int i = 0; i < level - 1; i++)
                Console.Write("   ");
            if (level > 0)
                System.Console.WriteLine("∟--" + node.Value);
            else
                System.Console.WriteLine(node.Value);
            foreach (var child in node.Children)
                PrintWithSpaces(child, level + 1);
        }

        public bool IsContain(T value)
        {
            if (Value.Equals(value)) return true;
            foreach (var child in Children)
                if (child.IsContain(value))
                    return true;

            return false;
        }

    }
}