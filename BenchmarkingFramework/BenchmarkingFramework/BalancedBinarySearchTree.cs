using System;
using System.Collections;
using System.Collections.Generic;

namespace BenchmarkingFramework
{
    //Enumeration for red, black the two colors nodes can take
    enum RBColor { RED, BLACK };

    /*
    * Red-Black trees have the properties in common with binary search trees
    * that all key values of nodes to the left of a node n are less than the key
    * of node n and all key values of nodes to the right of n are
    * greater than the key of node n.
    * Red-Black trees also satisfy the properties:
    * 1. Every node is either red or black.
    * 2. The root is black.
    * 3. Every leaf (NIL) is black.
    * 4. If a node is red, then both its children are black.
    * 5. For each node, all simple paths from the node to descendant leaves contain the
    *    same number of black nodes.
    */
    public class RedBlackTree<Key, Value>
        where Key : IComparable
    {
        private Node root = NIL.Instance();  //used to keep track of the root of the binary search tree

        /*
         * Node is the class used to hold key/value pairs.
         * It holds references to the left, right, and parent Nodes.
         * It also holds a value for the node's color which can be red or black.
         */
        internal class Node
        {
            internal Key key;
            internal Value val;
            internal Node left, right, parent;
            internal int N;
            internal RBColor color;

            public Node()
            {
            }
        }

        /*
        * NIL uses the singleton pattern to represent the NIL sentinel.
        * All leafs in a red-black tree are NIL.
        * In this implementation we leave all values of the node as null
        * except for the color which is black.
        * */
        private class NIL
        {
            private static Node _instance;

            protected NIL()
            {
            }
            /*
            * Check to see if _instance has already been created, if it has
            * return that otherwise create _instance.
            * As a note this uses lazy initialization and is not thread safe.
            * */
            public static Node Instance()
            {
                if (_instance == null)
                {
                    _instance = new Node();
                    _instance.color = RBColor.BLACK;
                }

                return _instance;
            }
        }

        /*
        * RBInsert and RBDelete modify the tree which may result in a violation of the
        * red-black poperties. In order to fix this we may need to "move around" some of
        * the nodes as well as their colors.  LeftRotate and RightRotate help us do this.
        * */
        private Node LeftRotate(Node x)
        {
            Node y = x.right;
            x.right = y.left;
            if (y.left != NIL.Instance())
                y.left.parent = x;
            y.parent = x.parent;
            if (x.parent == NIL.Instance())
            {
                root = y;
            }
            else if (x == x.parent.left)
            {
                x.parent.left = y;
            }
            else
            {
                x.parent.right = y;
            }
            y.left = x;
            x.parent = y;

            return x;
        }

        private Node RightRotate(Node x)
        {
            Node y = x.left;
            x.left = y.right;
            if (y.right != NIL.Instance())
                y.right.parent = x;
            y.parent = x.parent;
            if (x.parent == NIL.Instance())
            {
                root = y;
            }
            else if (x == x.parent.right)
            {
                x.parent.right = y;
            }
            else
            {
                x.parent.left = y;
            }
            y.right = x;
            x.parent = y;

            return x;
        }

        /*
        *
        * */
        public void RBInsert(Key kz, Value vz)
        {
            //Only add a new node if the key does not already exist
            Node newNode = new Node();
            newNode.key = kz;
            newNode.val = vz;
            newNode.left = NIL.Instance();
            newNode.right = NIL.Instance();
            RBInsert(newNode);
        }

        /*
        * This is similar to insert for a binary search tree with a few differences.  We use NIL.Instance()
        * instead of null.
        * After inserting we call RBInsertFixup incase we've violated one of the red-black properties.
        * */
        private void RBInsert(Node z)
        {
            Node y = NIL.Instance();
            Node x = root;
            int cmp;

            while (x != NIL.Instance())
            {
                y = x;
                cmp = z.key.CompareTo(x.key);
                if (cmp < 0)
                    x = x.left;
                else if (cmp > 0)
                    x = x.right;
                else if (cmp == 0) //This key already exists, exit without adding it
                    return;
            }
            z.parent = y;
            cmp = z.key.CompareTo(y.key);
            if (y == NIL.Instance())
            {
                root = z;
            }
            else if (cmp < 0)
            {
                y.left = z;
            }
            else if (cmp > 0)
            {
                y.right = z;
            }
            else if (cmp == 0)//This key already exists, exit without adding it
            {
                return;
            }
            z.left = NIL.Instance();
            z.right = NIL.Instance();
            z.color = RBColor.RED;
            z = RBInsertFixup(z);
        }

        /*
        * After inserting we need to fix any violations we may have introduced.
        * The violations fall into three cases:
        * Case 1: z's uncle y is red
        * Case 2: z's uncle y is black and z is a right child
        * Case 3: z's uncle y is black and z is a left child
        * */
        private Node RBInsertFixup(Node z)
        {
            Node y = new Node();

            while (z.parent.color == RBColor.RED)
            {
                //The parent is the grandparent's left child.
                if (z.parent == z.parent.parent.left)
                {
                    y = z.parent.parent.right;
                    if (y.color == RBColor.RED)
                    {
                        //Case 1 (above)
                        z.parent.color = RBColor.BLACK;
                        y.color = RBColor.BLACK;
                        z.parent.parent.color = RBColor.RED;
                        z = z.parent.parent;
                    }
                    else
                    {
                        //Case 2 (above)
                        if (z == z.parent.right)
                        {
                            z = z.parent;
                            LeftRotate(z);
                        }
                        //Case 3 (above)
                        z.parent.color = RBColor.BLACK;
                        z.parent.parent.color = RBColor.BLACK;
                        RightRotate(z.parent.parent);
                    }
                }
                else//The parent is the grandparent's right child.
                {
                    //This is the same as the above code with "right" and "left" exchanged
                    y = z.parent.parent.left;
                    if (y.color == RBColor.RED)
                    {
                        //Case 1 (above)
                        z.parent.color = RBColor.BLACK;
                        y.color = RBColor.BLACK;
                        z.parent.parent.color = RBColor.RED;
                        z = z.parent.parent;
                    }
                    else
                    {
                        //Case 2 (above)
                        if (z == z.parent.left)
                        {
                            z = z.parent;
                            RightRotate(z);
                        }
                        //Case 3 (above)
                        z.parent.color = RBColor.BLACK;
                        z.parent.parent.color = RBColor.RED;
                        LeftRotate(z.parent.parent);
                    }
                }
            }
            root.color = RBColor.BLACK;
            return z;
        }

        /*
        * Used to "replace" u with v.  Used when deleting a node and we need to replace
        * the deleted node with an other node from the tree.
        * */
        private void RBTransplant(Node u, Node v)
        {
            if (u.parent == NIL.Instance())
            {
                root = v;
            }
            else if (u == u.parent.left)
            {
                u.parent.left = v;
            }
            else
            {
                u.parent.right = v;
            }
            v.parent = u.parent;
        }

        /*
        * Delete a key from the tree
        * First find the node for the given tree
        * then call the TreeDelete(Node z) function
        * */
        public void RBDelete(Key key)
        {
            Node search = TreeSearch(key);
            RBDelete(search);
        }

        /*
        * Delete node z and replace it with y.  If z has only one child we just replace z with
        * that child.  If z has two children then y should be z's successor.
        * */
        private void RBDelete(Node z)
        {
            Node x;
            //First, set y equal to z
            Node y = z;
            /*
            * We want to store z's original color so when we move a node
            * into z's postion and the value of yOriginalColor is black then we call
            * RBDeleteFixup.
            */
            RBColor yOriginalColor = y.color;

            /*
            * The first two if statements cover when z just has one child.  If
            * so, just replace z with the child
            */
            if (z.left == NIL.Instance())
            {
                x = z.right;
                RBTransplant(z, z.right);
            }
            else if (z.right == NIL.Instance())
            {
                x = z.left;
                RBTransplant(z, z.left);
            }
            else
            {
                /*
                * z has two children.  First find the successor (the smallest key
                * that is larger than z.  Move it into z's position by setting it's
                * parent to z and re-assign child nodes.
                */
                y = RBTreeMinimum(z.right);
                yOriginalColor = y.color;
                x = y.right;
                if (y.parent == z)
                {
                    x.parent = y;
                }
                else
                {
                    RBTransplant(y, y.right);
                    y.right = z.right;
                    y.right.parent = y;
                }
                RBTransplant(z, y);
                y.left = z.left;
                y.left.parent = y;
                y.color = z.color;
            }
            /*
            * If node y was black, several problems may arise which RBDeleteFixup will fix.
            * */
            if (yOriginalColor == RBColor.BLACK)
            {
                RBDeleteFixup(x);
            }
        }

        /*
        * This function is called when y in RBDelete is black.  If y had been the root and a red
        * child of y becomes the new root we have violated property 2.  If both x and x.parent are red then
        * we have violated property 4.  Finally, by moving y within the tree may cause a simple path
        * to contain one fewer black nodes which violates property 5.
        * */
        private void RBDeleteFixup(Node x)
        {
            Node w;
            /*
            * Start with x and move up the tree until we reach a red node or the root
            * */
            while (x != root && x.color == RBColor.BLACK)
            {
                //Check to see if x is the left child
                if (x == x.parent.left)
                {
                    w = x.parent.right;
                    if (w.color == RBColor.RED)
                    {
                        w.color = RBColor.BLACK;
                        x.parent.color = RBColor.RED;
                        LeftRotate(x.parent);
                        w = x.parent.right;
                    }
                    if (w.left.color == RBColor.BLACK && w.right.color == RBColor.BLACK)
                    {
                        w.color = RBColor.RED;
                        x = x.parent;
                    }
                    else
                    {
                        if (w.right.color == RBColor.BLACK)
                        {
                            w.left.color = RBColor.BLACK;
                            w.color = RBColor.RED;
                            RightRotate(w);
                            w = x.parent.right;
                        }
                        w.color = x.parent.color;
                        x.parent.color = RBColor.BLACK;
                        w.right.color = RBColor.BLACK;
                        LeftRotate(x.parent);
                        x = root;
                    }
                }
                else //Same as the above code with right and left exchanged
                {
                    w = x.parent.left;
                    if (w.color == RBColor.RED)
                    {
                        w.color = RBColor.BLACK;
                        x.parent.color = RBColor.RED;
                        RightRotate(x.parent);
                        w = x.parent.left;
                    }
                    if (w.left.color == RBColor.BLACK && w.right.color == RBColor.BLACK)
                    {
                        w.color = RBColor.RED;
                        x = x.parent;
                    }
                    else
                    {
                        if (w.left.color == RBColor.BLACK)
                        {
                            w.right.color = RBColor.BLACK;
                            w.color = RBColor.RED;
                            LeftRotate(w);
                            w = x.parent.left;
                        }
                        w.color = x.parent.color;
                        x.parent.color = RBColor.BLACK;
                        w.right.color = RBColor.BLACK;
                        RightRotate(x.parent);
                        x = root;
                    }
                }
            }
            x.color = RBColor.BLACK;
        }

        /*
        * To find the minimum of the tree from node n (including
        * the root) we just keep going down the left branches until there
        * are no more nodes.
        * */
        private Node RBTreeMinimum(Node x)
        {
            while (x.left != NIL.Instance())
            {
                x = x.left;
            }
            return x;
        }

        /*
        * To find the maximum of the three from node n (including
        * the root) we just keep going down the right braches until there
        * are no more nodes.
        * */
        private Node RBTreeMaximum(Node x)
        {
            while (x.right != NIL.Instance())
            {
                x = x.right;
            }
            return x;
        }

        private Node TreeSearch(Key key)
        {
            return TreeSearch(root, key);
        }

        /*
        * Search the tree for a specific tree starting at a given node.
        * If the search key is equal to the key of Node x go we've found the
        * node.  If it is left go down the left subtree, if greater go down the right subtree.
        * Recursively call this function which will keep comparing and going down the tree to
        * the left or right until the node is found.  If no node is found the result will be null
        * */
        private Node TreeSearch(Node x, Key key)
        {
            if (x == null || key.CompareTo(x.key) == 0)
            {
                return x;
            }
            if (key.CompareTo(x.key) < 0)
            {
                return TreeSearch(x.left, key);
            }
            else
            {
                return TreeSearch(x.right, key);
            }
        }
    }

    public class AvlTree
    {
        private IComparer<int> _comparer;
        private AvlNode _root;

        public AvlTree(IComparer<int> comparer)
        {
            _comparer = comparer;
        }

        public AvlTree()
            : this(Comparer<int>.Default)
        {

        }

        public void Clear()
        {
            _root = null;
        }

        public bool Search(int key, out int value)
        {
            AvlNode node = _root;

            while (node != null)
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (_comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    value = node.Value;

                    return true;
                }
            }

            value = default(int);

            return false;
        }

        public void Insert(int key, int value)
        {
            if (_root == null)
            {
                _root = new AvlNode { Key = key, Value = value };
            }
            else
            {
                AvlNode node = _root;

                while (node != null)
                {
                    int compare = _comparer.Compare(key, node.Key);

                    if (compare < 0)
                    {
                        AvlNode left = node.Left;

                        if (left == null)
                        {
                            node.Left = new AvlNode { Key = key, Value = value, Parent = node };

                            InsertBalance(node, 1);

                            return;
                        }
                        else
                        {
                            node = left;
                        }
                    }
                    else if (compare > 0)
                    {
                        AvlNode right = node.Right;

                        if (right == null)
                        {
                            node.Right = new AvlNode { Key = key, Value = value, Parent = node };

                            InsertBalance(node, -1);

                            return;
                        }
                        else
                        {
                            node = right;
                        }
                    }
                    else
                    {
                        node.Value = value;

                        return;
                    }
                }
            }
        }

        private void InsertBalance(AvlNode node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)
                {
                    if (node.Left.Balance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }

                    return;
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }

                    return;
                }

                AvlNode parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? 1 : -1;
                }

                node = parent;
            }
        }

        public bool Delete(int key)
        {
            AvlNode node = _root;

            while (node != null)
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (_comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    AvlNode left = node.Left;
                    AvlNode right = node.Right;

                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == _root)
                            {
                                _root = null;
                            }
                            else
                            {
                                AvlNode parent = node.Parent;

                                if (parent.Left == node)
                                {
                                    parent.Left = null;

                                    DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.Right = null;

                                    DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            Replace(node, right);

                            DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        Replace(node, left);

                        DeleteBalance(node, 0);
                    }
                    else
                    {
                        AvlNode successor = right;

                        if (successor.Left == null)
                        {
                            AvlNode parent = node.Parent;

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == _root)
                            {
                                _root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.Left != null)
                            {
                                successor = successor.Left;
                            }

                            AvlNode parent = node.Parent;
                            AvlNode successorParent = successor.Parent;
                            AvlNode successorRight = successor.Right;

                            if (successorParent.Left == successor)
                            {
                                successorParent.Left = successorRight;
                            }
                            else
                            {
                                successorParent.Right = successorRight;
                            }

                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            successor.Right = right;
                            right.Parent = successor;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == _root)
                            {
                                _root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            DeleteBalance(successorParent, -1);
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        private void DeleteBalance(AvlNode node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 2)
                {
                    if (node.Left.Balance >= 0)
                    {
                        node = RotateRight(node);

                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance <= 0)
                    {
                        node = RotateLeft(node);

                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                AvlNode parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? -1 : 1;
                }

                node = parent;
            }
        }

        private AvlNode RotateLeft(AvlNode node)
        {
            AvlNode right = node.Right;
            AvlNode rightLeft = right.Left;
            AvlNode parent = node.Parent;

            right.Parent = parent;
            right.Left = node;
            node.Right = rightLeft;
            node.Parent = right;

            if (rightLeft != null)
            {
                rightLeft.Parent = node;
            }

            if (node == _root)
            {
                _root = right;
            }
            else if (parent.Right == node)
            {
                parent.Right = right;
            }
            else
            {
                parent.Left = right;
            }

            right.Balance++;
            node.Balance = -right.Balance;

            return right;
        }

        private AvlNode RotateRight(AvlNode node)
        {
            AvlNode left = node.Left;
            AvlNode leftRight = left.Right;
            AvlNode parent = node.Parent;

            left.Parent = parent;
            left.Right = node;
            node.Left = leftRight;
            node.Parent = left;

            if (leftRight != null)
            {
                leftRight.Parent = node;
            }

            if (node == _root)
            {
                _root = left;
            }
            else if (parent.Left == node)
            {
                parent.Left = left;
            }
            else
            {
                parent.Right = left;
            }

            left.Balance--;
            node.Balance = -left.Balance;

            return left;
        }

        private AvlNode RotateLeftRight(AvlNode node)
        {
            AvlNode left = node.Left;
            AvlNode leftRight = left.Right;
            AvlNode parent = node.Parent;
            AvlNode leftRightRight = leftRight.Right;
            AvlNode leftRightLeft = leftRight.Left;

            leftRight.Parent = parent;
            node.Left = leftRightRight;
            left.Right = leftRightLeft;
            leftRight.Left = left;
            leftRight.Right = node;
            left.Parent = leftRight;
            node.Parent = leftRight;

            if (leftRightRight != null)
            {
                leftRightRight.Parent = node;
            }

            if (leftRightLeft != null)
            {
                leftRightLeft.Parent = left;
            }

            if (node == _root)
            {
                _root = leftRight;
            }
            else if (parent.Left == node)
            {
                parent.Left = leftRight;
            }
            else
            {
                parent.Right = leftRight;
            }

            if (leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if (leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }

            leftRight.Balance = 0;

            return leftRight;
        }

        private AvlNode RotateRightLeft(AvlNode node)
        {
            AvlNode right = node.Right;
            AvlNode rightLeft = right.Left;
            AvlNode parent = node.Parent;
            AvlNode rightLeftLeft = rightLeft.Left;
            AvlNode rightLeftRight = rightLeft.Right;

            rightLeft.Parent = parent;
            node.Right = rightLeftLeft;
            right.Left = rightLeftRight;
            rightLeft.Right = right;
            rightLeft.Left = node;
            right.Parent = rightLeft;
            node.Parent = rightLeft;

            if (rightLeftLeft != null)
            {
                rightLeftLeft.Parent = node;
            }

            if (rightLeftRight != null)
            {
                rightLeftRight.Parent = right;
            }

            if (node == _root)
            {
                _root = rightLeft;
            }
            else if (parent.Right == node)
            {
                parent.Right = rightLeft;
            }
            else
            {
                parent.Left = rightLeft;
            }

            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }

            rightLeft.Balance = 0;

            return rightLeft;
        }

        private static void Replace(AvlNode target, AvlNode source)
        {
            AvlNode left = source.Left;
            AvlNode right = source.Right;

            target.Balance = source.Balance;
            target.Key = source.Key;
            target.Value = source.Value;
            target.Left = left;
            target.Right = right;

            if (left != null)
            {
                left.Parent = target;
            }

            if (right != null)
            {
                right.Parent = target;
            }
        }

        sealed class AvlNode
        {
            public AvlNode Parent;
            public AvlNode Left;
            public AvlNode Right;
            public int Key;
            public int Value;
            public int Balance;
        }
    }

    // Interface class to link the RB tree implementation to the framework
    class RBTree : TreeAlgorithm
    {
        private RedBlackTree<int, int> tree;

        public RBTree()
        {
            tree = new RedBlackTree<int, int>();
        }

        public override void Build(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                tree.RBInsert(array[i], 28);
            }
        }

        public override void Insert(int key)
        {
            tree.RBInsert(key, 28);
        }

        public override int Lookup(int key)
        {
            return 0;
        }

        public override bool Delete(int key)
        {
            tree.RBDelete(key);
            return true;
        }

        public override object Clone()
        {
            return new RBTree();
        }

        public override string GetDataType()
        {
            return "Red-black Tree";
        }
    }

    // Interface class to link the AVL tree implementation to the framework
    class AVLTree : TreeAlgorithm
    {
        private AvlTree tree;

        public AVLTree()
        {
            tree = new AvlTree();
        }

        public override void Build(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                tree.Insert(array[i], 28);
            }
        }

        public override void Insert(int key)
        {
            tree.Insert(key, 28);
        }

        public override int Lookup(int key)
        {
            int value;
            tree.Search(key, out value);
            return value;
        }

        public override bool Delete(int key)
        {
            tree.Delete(key);
            return true;
        }

        public override object Clone()
        {
            return new AVLTree();
        }

        public override string GetDataType()
        {
            return "AVL Tree";
        }
    }
}
