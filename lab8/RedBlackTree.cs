using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    class RedBlackTree<TValue>
    {
        enum Color
        {
            Red,
            Black
        }
        class Node
        {
            public double key;
            public Color color;
            public Node parent;
            public Node leftChild;
            public Node rightChild;

            public Node(Color color, Node parent)
            {
                this.color = color;
                this.parent = parent;
            }
            public Node(double key, Color color)
            {
                this.key = key;
                this.color = color;
            }
            public Node(double key, Color color, Node parent)
            {
                this.key = key;
                this.color = color;
                this.parent = parent;
            }
        }
        private Node root;

        public void Insert(double key)
        {
            if (this.root == null)
            {
                this.root = new Node(key, Color.Black);
                return;
            }
            Node insertedNode = InsertRecursively(key, this.root);
            if (insertedNode.parent.color != Color.Black)
            {
                Console.WriteLine($"Parent of '{insertedNode.key}' is red - need correction");
                DoCorrection(insertedNode);
            }
        }
        private void DoCorrection(Node node)
        {
            if((node == root || node.parent == root) && root.color == Color.Red)
            {
                Console.WriteLine("Root is red - recoloring");
                root.color = Color.Black;
                return;
            }
            
            Node grandparent = node.parent.parent;
            if (grandparent.leftChild == null) // has only right child
            {
                Console.Write($"Grandparent of '{node.key}' has only right child ");
                if (node == node.parent.rightChild) // rigth-right unbalance
                {
                    Console.WriteLine($"and '{node.key}' is right child - doing single turn");
                    SingleTurn(grandparent, "right");
                }
                else // right-left unbalance
                {
                    Console.WriteLine($"and '{node.key}' is left child - doing double turn");
                    DoubleTurn(grandparent, "right-left");
                }
            }
            else if (grandparent.rightChild == null) // has only left child
            {
                Console.Write($"Grandparent of '{node.key}' has only left child ");
                if (node == node.parent.leftChild) // left-left unbalance
                {
                    Console.WriteLine($"and '{node.key}' is left child - doing single turn");
                    SingleTurn(grandparent, "left");
                }
                else // left-right unbalance
                {
                    Console.WriteLine($"and '{node.key}' is right child - doing double turn");
                    DoubleTurn(grandparent, "left-right");
                }
            }
            else // has two childs
            {
                if (grandparent.leftChild.color == Color.Red && grandparent.rightChild.color == Color.Red) // both childs are red
                {
                    Console.WriteLine($"Both childs of grandparent of '{node.key}' are red - recoloring. Recursively going upper.");
                    grandparent.leftChild.color = Color.Black;
                    grandparent.rightChild.color = Color.Black;
                    grandparent.color = Color.Red;
                    DoCorrection(node.parent);
                }
                else
                {
                    Node redParent = node.parent;
                    if (redParent == grandparent.leftChild) // left - ...
                    {
                        Console.Write($"Grandparent of '{node.key}' has two children, left child is red ");
                        if (node == redParent.leftChild) // left - left
                        {
                            Console.WriteLine($"and '{node.key}' is left child - doing single turn");
                            SingleTurn(grandparent, "left");
                        }
                        else // left - right
                        {
                            Console.WriteLine($"and '{node.key}' is right child - doing double turn");
                            DoubleTurn(grandparent, "left-right");
                        }
                    }
                    else // right - ...
                    {
                        Console.Write($"Grandparent of '{node.key}' has two children, right child is red ");
                        if (node == redParent.rightChild) // right - right
                        {
                            Console.WriteLine($"and {node.key} is right child - doing single turn");
                            SingleTurn(grandparent, "right");
                        }
                        else // right - left
                        {
                            Console.WriteLine($"and {node.key} is left child - doing double turn");
                            DoubleTurn(grandparent, "right-left");
                        }
                    }
                }
            }
        }
        private void DoubleTurn(Node node, string side)
        {
            Node A = node;
            Node B;
            Node C;
            if (side == "right-left")
            {
                B = A.rightChild;
                C = B.leftChild;
            }
            else
            {
                B = A.leftChild;
                C = B.rightChild;
            }
            if (A.parent == null) // A - is root
            {
                this.root = C;
                C.parent = null;
            }
            else
            {
                C.parent = A.parent;
                if (A.parent.rightChild == A) // A - is right child
                {
                    C.parent.rightChild = C;
                }
                else // A - is left child
                {
                    C.parent.leftChild = C;
                }
            }
            if (side == "right-left")
            {
                A.rightChild = C.leftChild;
                B.leftChild = C.rightChild;
                C.leftChild = A;
                C.rightChild = B;
                
            }
            else
            {
                B.rightChild = C.leftChild;
                A.leftChild = C.rightChild;
                C.leftChild = B;
                C.rightChild = A;
            }
            A.parent = C;
            B.parent = C;
            A.color = Color.Red;
            C.color = Color.Black;
        }
        private void SingleTurn(Node node, string side)
        {
            Node A = node;
            Node B;
            if (side == "right")
            {
                B = A.rightChild;
            }
            else
            {
                B = A.leftChild;
            }
            if (A.parent == null) // A - root
            {
                this.root = B;
                B.parent = null;
            }
            else // A - not root
            {
                B.parent = A.parent;
                if (A.parent.rightChild == A) // A - is right child
                {
                    B.parent.rightChild = B;
                }
                else // A - is left child
                {
                    B.parent.leftChild = B;
                }
            }
            if (side == "right")
            {
                A.rightChild = B.leftChild;
                B.leftChild = A;
            }
            else
            {
                A.leftChild = B.rightChild;
                B.rightChild = A;
            }
            A.color = Color.Red;
            A.parent = B;
            B.color = Color.Black;
        }
        private Node InsertRecursively(double key, Node node)
        { 
            if (key == node.key)
            {
                throw new Exception("Key is already in tree");
            }
            else if (key < node.key)
            {
                if (node.leftChild == null)
                {
                    node.leftChild = new Node(key, Color.Red, node);
                    return node.leftChild;
                }
                return InsertRecursively(key, node.leftChild);
            }
            else
            {
                if (node.rightChild == null)
                {
                    node.rightChild = new Node(key, Color.Red, node);
                    return node.rightChild;
                }
                return InsertRecursively(key, node.rightChild);
            }
        }
        public void Print()
        {
            PrintRecursively(root, 4);
        }
        private void PrintRecursively(Node node, int padding)
        {
            if (node != null)
            {
                if (node.rightChild != null)
                {
                    PrintRecursively(node.rightChild, padding + 4);
                }
                if (padding > 0)
                {
                    Console.Write(" ".PadLeft(padding));
                }
                if (node.rightChild != null)
                {
                    Console.Write("/\n");
                    Console.Write(" ".PadLeft(padding));
                }
                if (node.color == Color.Black)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(node.key + "\n ");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(node.key + "\n ");
                    Console.ResetColor();
                }
                
                if (node.leftChild != null)
                {
                    Console.Write(" ".PadLeft(padding) + "\\\n");
                    PrintRecursively(node.leftChild, padding + 4);
                }
            }
        }
    }
}
