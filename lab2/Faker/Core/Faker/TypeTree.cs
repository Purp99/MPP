using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class TypeTree
    {
        public Node Root { get; set; } = null;
        public Node Current { get; set; } = null;

        public TypeTree()
        {
            Root = new Node();
            Current = Root;
        }

        public void Clear()
        {
            Root = null;
            Current = null;
        }
    }

    public class Node
    {
        public Type Type { get; set; } = null;
        public Node Parent { get; set; } = null;
        public List<Node> Childs { get; set; }

        public Node(Type type = null)
        {
            Type = type;
            Childs = new List<Node>();
        }

        public Node AddChild(Node node)
        {
            Childs.Add(node);
            return this;
        }

        public int GetRepetitions(Type type)
        {
            int repetitions = 0;
            Node currentNode = this;

            while (currentNode.Type != null)
            {
                if (currentNode.Type == type)
                {
                    repetitions++;
                }

                currentNode = currentNode.Parent;
            }

            return repetitions;
        }

    }
}
