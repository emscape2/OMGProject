using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkingFramework
{
    /// <summary>
    /// 
    /// </summary>
    class BalancedBinarySearchTree : TreeAlgorithm
    {
        public static TreeNode empty = new TreeNode(0);
        protected TreeNode root = empty;

        /// <summary>
        /// Creates a new balanced binary search tree
        /// </summary>
        public BalancedBinarySearchTree()
        {
            empty.left = empty;
            empty.right = empty;
            empty.parent = empty;
        }

        public override string GetDataType()
        {
            return "BalancedBinarySearchTree";
        }

        /// <summary>
        /// Performs a left-rotation on a binary tree
        /// </summary>
        /// <param name="node"Node to be rotaded left></param>
        protected void rotateLeft(TreeNode node)
        {
            TreeNode y = node.right;
            node.right = y.left;

            if (y.left != empty)
                y.left.parent = node;
            y.parent = node.parent;

            if (node.parent == empty)
            {
                root = y;
            }
            else if (node == node.parent.left)
            {
                node.parent.left = y;
            }
            else 
            {
                node.parent.right = y;
            }
            y.left = node;
            node.parent = y;
        }

        /// <summary>
        /// Performs a right-rotation on a binary tree
        /// </summary>
        /// <param name="node">Node to be rotated right</param>
        protected void rotateRight(TreeNode node)
        {
            TreeNode y = node.left;
            node.left = y.right;

            if (y.right != empty)
                y.right.parent = node;
            y.parent = node.parent;

            if (node.parent == empty)
            {
                root = y;
            }
            else if (node == node.parent.right)
            {
                node.parent.right = y;
            }
            else
            {
                node.parent.left = y;
            }
            y.right = node;
            node.parent = y;
        }

        public override void Insert(int key)
        {
            this.Insert(new TreeNode(key));   
        }

        public override bool Delete(int key)
        {
            return this.Delete(Search(key));
        }

        /// <summary>
        /// Performs a binary tree insert
        /// </summary>
        /// <param name="node">Node to be inserted</param>
        protected virtual int Insert(TreeNode node)
        {
            TreeNode y = empty;
            TreeNode x = root;
            int height = 0; 

            while (x != empty)
            {
                y = x;

                if (node.key < x.key)
                    x = x.left;
                else
                    x = x.right;
                height++;
            }
            node.parent = y;

            if (y == empty)
                root = node;
            else if (node.key < y.key)
                y.left = node;
            else
                y.right = node;

            node.left = empty;
            node.right = empty;
            node.color = true;

            return height;
        }

        protected virtual bool Delete(TreeNode node)
        {
            return false;
        }

        /// <summary>
        /// Perform regular binary search tree lookup
        /// </summary>
        /// <param name="key">Key to be searched for</param>
        /// <returns>Found node</returns>
        protected TreeNode Search(int key)
        {
            TreeNode x = root;
            while (x.key != key && x != empty)
            {
                if (key < x.key)
                    x = x.left;
                else
                    x = x.right;
            }

            if (x != empty)
                return x;

            // return 0 if key is not found
            return empty;
        }

        public override int Lookup(int key)
        {
            return Search(key).value;
        }

        /// <summary>
        /// Finds node with lowest key in tree
        /// </summary>
        /// <param name="node">Root of tree to be searched</param>
        /// <returns>Found minimum</returns>
        protected TreeNode Minimum(TreeNode node)
        {
            while (node.left != empty)
                node = node.left;
            return node;
        }

        public override void Build(int[] array)
        {
            for (int i = 0; i < array.Length; i++) Insert(array[i]);
        }
    }

    class RBTree : BalancedBinarySearchTree
    {
        /// <summary>
        /// Red-black tree insertion
        /// </summary>
        /// <param name="node">Node to be inserted</param>
        protected override int Insert(TreeNode node)
        {
            int r = base.Insert(node);

            // Inserted node gets the color red
            node.color = true;
            RBInsertFixup(node);

            return r;
        }

        protected override bool Delete(TreeNode node)
        {
            RBDelete(node);
            return false;
        }

        public override string GetDataType()
        {
            return "RBTree";
        }

        /// <summary>
        /// Restore RB tree properties after insertion
        /// </summary>
        /// <param name="node"></param>
        private void RBInsertFixup(TreeNode node)
        {
            while (node.parent.color)
            {
                if (node.parent == node.parent.parent.left)
                {
                    TreeNode y = node.parent.parent.right;
                    if (y.color)
                    {
                        node.parent.color = false;
                        y.color = false;
                        node.parent.parent.color = true;
                        node = node.parent.parent;
                    }
                    else
                    {
                        if (node == node.parent.right)
                        {
                            node = node.parent;
                            rotateLeft(node);
                        }
                        node.parent.color = false;
                        node.parent.parent.color = true;
                        rotateRight(node.parent.parent);
                    }
                }
                else
                {
                    TreeNode y = node.parent.parent.left;
                    if (y.color)
                    {
                        node.parent.color = false;
                        y.color = false;
                        node.parent.parent.color = true;
                        node = node.parent.parent;
                    }
                    else
                    {
                        if (node == node.parent.left)
                        {
                            node = node.parent;
                            rotateRight(node);
                        }
                        node.parent.color = false;
                        node.parent.parent.color = true;
                        rotateLeft(node.parent.parent);
                    }
                }
                empty.color = false;
            }
            root.color = false;
        }

        /// <summary>
        /// Switch node v for node u
        /// </summary>
        /// <param name="u">Node u</param>
        /// <param name="v">Node v</param>
        private void RBTransplant(TreeNode u, TreeNode v)
        {
            if (u.parent == empty)
                root = v;
            else if (u == u.parent.left)
                u.parent.left = v;
            else
            {
                u.parent.right = v;
            }
            v.parent = u.parent;
        }

        /// <summary>
        /// Delete a node from the red-black tree
        /// </summary>
        /// <param name="node">Node to be deleted</param>
        private void RBDelete(TreeNode node)
        {
            TreeNode y = node;
            bool yColor = y.color;
            TreeNode x;

            if (node.left == empty)
            {
                x = node.right;
                RBTransplant(node, node.right);
            }
            else if (node.right == empty)
            {
                x = node.left;
                RBTransplant(node, node.left);
            }
            else
            {
                y = Minimum(node.right);
                yColor = y.color;
                x = y.right;
                if (y.parent == node)
                {
                    x.parent = y;
                }
                else
                {
                    RBTransplant(y, y.right);
                    y.right = node.right;
                    y.right.parent = y;
                }
                RBTransplant(node, y);
                y.left = node.left;
                y.left.parent = y;
                y.color = node.color;
            }
            if (!yColor) RBDeleteFixup(x);
        }

        /// <summary>
        /// Restores red-black properties of tree after deletion of a node
        /// </summary>
        /// <param name="x"></param>
        private void RBDeleteFixup(TreeNode x)
        {
            while (x != root && !x.color)
            {
                if (x == x.parent.left)
                {
                    TreeNode w = x.parent.right;
                    if (w.color)
                    {
                        w.color = false;
                        x.parent.color = true;
                        rotateLeft(x.parent);
                        w = x.parent.right;
                    }
                    if (!w.left.color && !w.right.color)
                    {
                        w.color = true;
                        x = x.parent;
                    }
                    else
                    {
                        if (!w.right.color)
                        {
                            w.left.color = false;
                            w.color = true;
                            rotateRight(w);
                            w = x.parent.right;
                        }
                        w.color = x.parent.color;
                        x.parent.color = false;
                        w.right.color = false;
                        rotateLeft(x.parent);
                        x = root;
                    }
                }
                else
                {
                    TreeNode w = x.parent.left;
                    if (w.color)
                    {
                        w.color = false;
                        x.parent.color = true;
                        rotateRight(x.parent);
                        w = x.parent.left;
                    }
                    if (!w.right.color && !w.left.color)
                    {
                        w.color = true;
                        x = x.parent;
                    }
                    else
                    {
                        if (!w.left.color)
                        {
                            w.right.color = false;
                            w.color = true;
                            rotateLeft(w);
                            w = x.parent.left;
                        }
                        w.color = x.parent.color;
                        x.parent.color = false;
                        w.left.color = false;
                        rotateRight(x.parent);
                        x = root;
                    }
                }
            }
            x.color = false;
        }
    }

    /// <summary>
    /// Binary search tree node
    /// </summary>
    class TreeNode
    {
        public TreeNode left, right, parent;
        public int key, value, height;
        public bool color;

        public TreeNode(int key)
        {
            this.key = key;
            value = height = 0;
            left = right = parent = BalancedBinarySearchTree.empty;
            color = true;
        }
    }
}
