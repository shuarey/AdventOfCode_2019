using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode_2019
{
    public class Node<T> 
    {
        public Node<T> Parent { get; set; }
        public List<Node<T>> Children { get; set; } = new List<Node<T>>();
        public T Value { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }
}
