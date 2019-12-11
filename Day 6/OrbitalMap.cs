using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode_2019
{
    public class OrbitalMap
    {
        public List<Node<string>> Nodes { get; set; } = new List<Node<string>>();
        public Node<string> Tree { get; set; }
        public int OrbitCount { get; set; }
        public List<Node<string>> TempNodeChildren { get; set; }

        private string orbitalList = @"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\OrbitalRelations.txt";
        private const string Root = "COM";

        public OrbitalMap()
        {
            orbitalList = @"C:\Users\joshu\source\repos\AdventOfCode_2019\Resources\OrbitalListSample.txt";
            using (StreamReader reader = new StreamReader(orbitalList))
            {
                while (!reader.EndOfStream)
                {
                    var orbitalPair = reader.ReadLine().Split(')');

                    Node<string> parentNode = new Node<string>(orbitalPair[0]);
                    parentNode.Children.Add(new Node<string>(orbitalPair[1]));
                    if (parentNode.Value == Root)
                    {
                        Tree = parentNode;
                    }
                    else
                    {
                        Nodes.Add(parentNode);
                    }
                }
                FillTree();
            }
        }

        private void FillTree()
        {

        }

        public bool BuildNodeList(Node<string> node, Node<string> parent, Node<string> child)
        {
            if (node == null) { return false; }

            for (int i = 0; i < node.Children.Count; i++)
            {
                if (node.Value == parent.Value)
                {
                    if (node.Children.Contains(child))
                    {
                        return true;
                    }
                    node.Children.Add(child);
                    return true;
                }
                else if (node.Children[i].Value == child.Value)
                {
                    node.Parent = parent;
                    parent.Children.Add(node);
                    return true;
                }
                BuildNodeList(node.Children[i], parent, child);
            }
            return false;
        }
    }
}
