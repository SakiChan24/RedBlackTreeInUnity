//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;





//public class BinaryTree : BinaryTreeBase
//{
//	public BinaryTreeNode RootNode;

//	public void AddNode(int key)
//	{
//		if (null == this.RootNode)
//		{
//			this.RootNode = new BinaryTreeNode(key);
//			return;
//		}

//		this._AddNode(this.RootNode, key);
//	}


//	public BinaryTreeNode FindNode(int key)
//	{
//		return this._FindNode(this.RootNode, key);
//	}



//	private BinaryTreeNode _FindNode(BinaryTreeNode node, int key)
//	{
//		if (node == null)
//		{
//			return null;
//		}

//		if (node.Key == key)
//		{
//			return node;
//		}

//		if (key < node.Key)
//		{
//			if (null != node.LeftNode)
//			{
//				return this._FindNode(node.LeftNode, key);
//			}
//		}

//		if (key > node.Key)
//		{
//			if (null != node.RightNode)
//			{
//				return this._FindNode(node.RightNode, key);
//			}
//		}

//		return null;
//	}


//	private void _DeleteNode(ref BinaryTreeNode node_pointer, int key)
//	{
//		if (node_pointer == null)
//		{
//			return;
//		}
//		if (key > node_pointer.Key)
//		{
//			this._DeleteNode(ref node_pointer.RightNode, key);
//		}
//		else if (key < node_pointer.Key)
//		{
//			this._DeleteNode(ref node_pointer.LeftNode, key);
//		}
//		else if (node_pointer.LeftNode != null && node_pointer.RightNode != null)
//		{
//			BinaryTreeNode min_node = this._FindMinNode(node_pointer.RightNode);
//			node_pointer.Key = min_node.Key;
//			this._DeleteNode(ref node_pointer.RightNode, node_pointer.Key);
//		}
//		else if (node_pointer.LeftNode != null)
//		{
//			node_pointer = node_pointer.LeftNode;
//		}
//		else if (node_pointer.RightNode != null)
//		{
//			node_pointer = node_pointer.RightNode;
//		}
//		else
//		{
//			node_pointer = null;
//		}
//	}


//	//这个函数从书上抄来的，太取巧了，自己再写一份
//	private BinaryTreeNode _DeleteNode2(BinaryTreeNode node, int key)
//	{
//		if (node == null)
//		{
//			return null;
//		}
		
//		if (key > node.Key)
//		{
//			node.RightNode = this._DeleteNode2(node.RightNode, key);
//		}
//		else if (key < node.Key)
//		{
//			node.LeftNode = this._DeleteNode2(node.LeftNode, key);
//		}
//		else
//		{
//			if (node.LeftNode != null && node.RightNode != null)
//			{
//				BinaryTreeNode min_node = this._FindMinNode(node.RightNode);
//				node.Key = min_node.Key;
//				node.RightNode = this._DeleteNode2(node.RightNode, node.Key);
//			}
//			else
//			{
//				if (node.LeftNode != null)
//				{
//					node = node.LeftNode;
//				}
//				else if (node.RightNode != null)
//				{
//					node = node.RightNode;
//				}
//				else
//				{
//					node = null;
//				}
//			}
//		}

//		return node;
//	}


//	private BinaryTreeNode _FindMinNode(BinaryTreeNode node)
//	{
//		if (node.LeftNode == null)
//		{
//			return node;
//		}
//		else
//		{
//			return this._FindMinNode(node.LeftNode);
//		}
//	}



//	public void DeleteNode(int key)
//	{
//		this._DeleteNode(ref this.RootNode, key);
//	}


//	private void _AddNode(BinaryTreeNode node, int key)
//	{
//		if (key > node.Key)
//		{
//			if (null == node.RightNode)
//			{
//				node.RightNode = new BinaryTreeNode(key);
//			}
//			else
//			{
//				this._AddNode(node.RightNode, key);
//			}
//		}
//		else if (key < node.Key)
//		{
//			if (null == node.LeftNode)
//			{
//				node.LeftNode = new BinaryTreeNode(key);
//			}
//			else
//			{
//				this._AddNode(node.LeftNode, key);
//			}
//		}
//		else
//		{
//			Debug.LogErrorFormat("key {0} exist! ", key);
//		}

//	}

//	public BinaryTreeNode GetRootNode()
//	{
//		return this.RootNode;
//	}
//}
