﻿using System;
using System.Collections.Generic;

namespace TreeLCRS
{
    public class Tree<T> 
    {
        Node<T> root;
        public int Count { get; set; }
        public Tree()
        {
            this.root = null;
            Count = 0;
        }
        public Tree(T data)
        {
            this.root = new Node<T>(data);
            Count = 1;
        }
        /// <summary>
        /// Is used to set the root of the tree
        /// </summary>
        /// <param name="data">Should be unique</param>
        public void Insert(T data)
        {
            if (root != null)
            {
                throw new InvalidOperationException("Tree already has a root");
            }
            root = new Node<T>(data);
            Count++;
        }
        public void Insert(T data, T parentData)
        {
            if (Contains(data))
            {
                throw new ArgumentException("Tree already contains an elemnt with given data");
            }
            Node<T> parent = Find(parentData);
            if (parent == null)
            {
                throw new ArgumentException("Tree doesn't contain an element with given parent's data");
            }
            Insert(data, parent);
            Count++;
        }

        void Insert(T data, Node<T> parent)
        {

            if (parent.Child == null)
            {
                parent.Child = new Node<T>(data);
                return;
            }
            parent = parent.Child;
            while (parent.Next != null)
            {
                parent = parent.Next;
            }
            parent.Next = new Node<T>(data);
        }
        public bool Contains(T data)
        {
            return Find(data) == null ? false : true;
        }

        Node<T> Find(T data)
        {
            Stack<Node<T>> visitNext = new Stack<Node<T>>();
            Node<T> node = root;
            visitNext.Push(node);

            while (visitNext.Count != 0)
            {
                node = visitNext.Pop();
                if (node.Data.Equals(data))
                {
                    return node;
                }
                if (node.Child != null)
                {
                    visitNext.Push(node.Child);
                }
                if (node.Next != null)
                {
                    visitNext.Push(node.Next);
                }
            }

            return null;
        }

        public void Traverse()
        {
            Traverse(root);
        }
        void Traverse(Node<T> root)
        {
            while (root != null)
            {
                Console.Write("{0} ", root.Data);
                if (root.Child != null)
                {
                    Traverse(root.Child);
                }
                root = root.Next;
            }
        }
    }
}

