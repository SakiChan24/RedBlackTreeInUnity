//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class AVLTreeNode: BinaryTreeNode
//{
//	public int Height;

//	public AVLTreeNode(int key):base(key)
//	{
//	}
//}



//public class AVLTree : BinaryTreeBase
//{
//	public BinaryTreeNode RootNode;

//	public void AddNode(int key)
//	{
//		if (null == this.RootNode)
//		{
//			this.RootNode = new AVLTreeNode(key);
//			return;
//		}

//		this._AddNode(ref this.RootNode, key);
//	}
//	private void _AddNode(ref BinaryTreeNode node, int key)
//	{
//		if (node == null)
//		{
//			node = new AVLTreeNode(key);
//		}
//		else if(key > node.Key)
//		{
//			this._AddNode(ref node.RightNode, key);
//		}
//		else if (key < node.Key)
//		{
//			this._AddNode(ref node.LeftNode, key);
//		}
//		else
//		{
//			Debug.LogErrorFormat("key {0} exist! ", key);
//		}

//		if (node != null)
//		{
//			this.CalcHeight(node);
//			this.BalanceTree(ref node);
//		}
//	}


//	public BinaryTreeNode FindNode(int key)
//	{
//		return this._FindNode(this.RootNode, key);
//	}


//	public void RotateRight(ref BinaryTreeNode node_a)
//	{
//		BinaryTreeNode node_b = node_a.LeftNode;

//		node_a.LeftNode = node_b.RightNode;
//		node_b.RightNode = node_a;

//		CalcHeight(node_a);
//		CalcHeight(node_b);

//		node_a = node_b;
//	}


//	public void RotateLeft(ref BinaryTreeNode node_a)
//	{
//		BinaryTreeNode node_b = node_a.RightNode;

//		node_a.RightNode = node_b.LeftNode;
//		node_b.LeftNode = node_a;

//		CalcHeight(node_a);
//		CalcHeight(node_b);

//		node_a = node_b;
//	}


//	public int CalcHeight(BinaryTreeNode node)
//	{
//		if (node == null)
//		{
//			return 0;
//		}

//		AVLTreeNode n = node as AVLTreeNode;
//		n.Height = Mathf.Max(CalcHeight(n.LeftNode), CalcHeight(n.RightNode)) + 1;
//		return n.Height;
//	}


//	public void BalanceTree(ref BinaryTreeNode in_node)
//	{
//		AVLTreeNode node = in_node as AVLTreeNode;
//		int factor = this.GetBalanceFactor(node);

//		if (factor > 1 && this.GetBalanceFactor(node.LeftNode) > 0) //LL
//		{
//			Debug.LogFormat("LL");
//			RotateRight(ref in_node);
//		}
//		else if (factor < -1 && this.GetBalanceFactor(node.RightNode) <= 0) //RR
//		{
//			Debug.LogFormat("RR");
//			RotateLeft(ref in_node);
//		}
//		else if (factor > 1 && this.GetBalanceFactor(node.RightNode) <= 0) //LR
//		{
//			Debug.LogFormat("LR");
//			RotateLeft(ref in_node.LeftNode);
//			RotateRight(ref in_node);
//		}
//		else if (factor < -1 && this.GetBalanceFactor(node.LeftNode) > 0) //RL
//		{
//			Debug.LogFormat("RL");
//			RotateRight(ref in_node.RightNode);
//			RotateLeft(ref in_node);
//		}
//	}


//	public int GetBalanceFactor(BinaryTreeNode node)
//	{
//		if (node == null)
//		{
//			return 0;
//		}
//		else
//		{
//			AVLTreeNode left_node = node.LeftNode as AVLTreeNode;
//			AVLTreeNode right_node = node.RightNode as AVLTreeNode;
//			int left_height = left_node != null ? left_node.Height : 0;
//			int right_height = right_node != null ? right_node.Height : 0;
//			return left_height - right_height;
//		}
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


//	private void _DeleteNode(ref BinaryTreeNode curr_node, int key)
//	{
//		if (curr_node == null)
//		{
//			return;
//		}
//		if (key > curr_node.Key)
//		{
//			this._DeleteNode(ref curr_node.RightNode, key);
//		}
//		else if (key < curr_node.Key)
//		{
//			this._DeleteNode(ref curr_node.LeftNode, key);
//		}
//		else if (curr_node.LeftNode != null && curr_node.RightNode != null)
//		{
//			BinaryTreeNode min_node = this._FindMinNode(curr_node.RightNode);
//			curr_node.Key = min_node.Key;
//			this._DeleteNode(ref curr_node.RightNode, curr_node.Key);
//		}
//		else if (curr_node.LeftNode != null)
//		{
//			curr_node = curr_node.LeftNode;
//		}
//		else if (curr_node.RightNode != null)
//		{
//			curr_node = curr_node.RightNode;
//		}
//		else
//		{
//			curr_node = null;
//		}

//		if (null != curr_node)
//		{
//			this.CalcHeight(curr_node);
//			this.BalanceTree(ref curr_node);
//		}

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



//	public BinaryTreeNode GetRootNode()
//	{
//		return this.RootNode;
//	}




//	//public void TestRotateLeft(BinaryTreeNode node, int key)
//	//{
//	//	if (node == null)
//	//	{
//	//		return;
//	//	}

//	//	if (key > node.Key)
//	//	{
//	//		TestRotateLeft(node.RightNode, key);
//	//	}
//	//	else if (key < node.Key)
//	//	{
//	//		TestRotateLeft(node.LeftNode, key);
//	//	}
//	//	else
//	//	{
//	//		RotateLeft(ref this.RootNode);
//	//	}
//	//}

//	//public void TestRotateRight(BinaryTreeNode node, int key)
//	//{
//	//	if (node == null)
//	//	{
//	//		return;
//	//	}

//	//	if (key > node.Key)
//	//	{
//	//		TestRotateRight(node.RightNode, key);
//	//	}
//	//	else if (key < node.Key)
//	//	{
//	//		TestRotateRight(node.LeftNode, key);
//	//	}
//	//	else
//	//	{
//	//		RotateRight(ref this.RootNode);
//	//	}
//	//}


//	//public int TestGetBalanceFactor(BinaryTreeNode node)
//	//{

//	//	AVLTreeNode left_node = node.LeftNode as AVLTreeNode;
//	//	AVLTreeNode right_node = node.RightNode as AVLTreeNode;

//	//	int left_height = 0;
//	//	if (null != left_node)
//	//	{
//	//		left_height = CalcHeight(left_node);
//	//	}
//	//	int right_height = 0;
//	//	if (null != right_node)
//	//	{
//	//		right_height = CalcHeight(right_node);
//	//	}

//	//	return left_height - right_height;
//	//}
//	//public void _TestBalanceTree2(BinaryTreeNode node)
//	//{
//	//	if (node == null)
//	//	{
//	//		return;
//	//	}

//	//	this.CalcHeight(node);

//	//	this._TestBalanceTree2(node.LeftNode);
//	//	this._TestBalanceTree2(node.RightNode);
//	//}

//	//public void _TestBalanceTree1(ref BinaryTreeNode node, int key)
//	//{

//	//	if (node == null)
//	//	{
//	//		return;
//	//	}

//	//	if (key > node.Key)
//	//	{
//	//		this._TestBalanceTree1(ref node.RightNode, key);
//	//	}
//	//	else if (key < node.Key)
//	//	{
//	//		this._TestBalanceTree1(ref node.LeftNode, key);
//	//	}
//	//	else
//	//	{
//	//		this._TestBalanceTree2(node);

//	//		BalanceTree(ref node);
//	//	}

//	//}


//	//public void TestBalanceTree(int key)
//	//{
//	//	_TestBalanceTree1(ref this.RootNode, key);
//	//}
//}
