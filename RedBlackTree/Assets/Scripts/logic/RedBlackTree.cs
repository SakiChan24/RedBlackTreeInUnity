using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum RedBlackTreeNodeColor
{
	Red = 0,
	Black,
}


public class RedBlackTreeNode
{

	public RedBlackTreeNode LeftNode;
	public RedBlackTreeNode RightNode;
	public int Key;

	public RedBlackTreeNode(int key)
	{
		this.Key = key;
	}

	public RedBlackTreeNodeColor Color = RedBlackTreeNodeColor.Red;
	public RedBlackTreeNode ParentNode;



	public bool IsRed()
	{
		return this.Color == RedBlackTreeNodeColor.Red;
	}

	public bool IsBlack()
	{
		return this.Color == RedBlackTreeNodeColor.Black;
	}


	public RedBlackTreeNode GetGrandNode()
	{
		return this.ParentNode != null ? this.ParentNode.ParentNode : null;
	}

}



public class RedBlackTree
{
	public RedBlackTreeNode RootNode;

	public RedBlackTreeNode GetRootNode()
	{
		return this.RootNode;
	}

	public void AddNode(int key)
	{
		this._AddNode(this.RootNode, key);
	}


	public RedBlackTreeNode FindNode(int key)
	{
		return this._FindNode(this.RootNode, key);
	}



	private RedBlackTreeNode _FindNode(RedBlackTreeNode node, int key)
	{
		while (node != null)
		{
			if (key > node.Key)
			{
				node = node.RightNode;
			}
			else if (key < node.Key)
			{
				node = node.LeftNode;
			}
			else
			{
				return node;
			}
		}

		return null;
	}


	//查找后继节点
	private RedBlackTreeNode _GetSuccessorNode(RedBlackTreeNode node)
	{
		RedBlackTreeNode curr_node = node.RightNode;

		while (null != curr_node)
		{
			if (null == curr_node.LeftNode)
			{
				break;
			}
			else
			{
				curr_node = curr_node.LeftNode;
			}
		}

		return curr_node;
	}


	//查找前驱节点
	private RedBlackTreeNode _GetPredecessorNode(RedBlackTreeNode node)
	{
		RedBlackTreeNode curr_node = node.LeftNode;

		while (null != curr_node)
		{
			if (null == curr_node.RightNode)
			{
				break;
			}
			else
			{
				curr_node = curr_node.RightNode;
			}
		}

		return curr_node;
	}



	public void DeleteNode(int key)
	{
		this._DeleteNode(this.RootNode, key);
	}


	private RedBlackTreeNode _DeleteNode(RedBlackTreeNode node, int key)
	{
		RedBlackTreeNode delete_node = this.FindNode(key);
		if (delete_node == null)
		{
			return null;
		}

		if (delete_node.LeftNode != null && delete_node.RightNode != null)
		{
			//RedBlackTreeNode rep_node = this._GetSuccessorNode(delete_node);
			RedBlackTreeNode replace_node = this._GetPredecessorNode(delete_node);
			delete_node.Key = replace_node.Key;
			delete_node = replace_node;
		}

		RedBlackTreeNode child_node = delete_node.LeftNode != null ? delete_node.LeftNode : delete_node.RightNode;

		RedBlackTreeNode parent_node = delete_node.ParentNode;

		//节点有一个子节点
		if (child_node != null)
		{
			child_node.ParentNode = parent_node;
			if (parent_node == null)
			{
				this.RootNode = child_node;
			}
			else if (parent_node.LeftNode == delete_node)
			{
				parent_node.LeftNode = child_node;
			}
			else
			{
				parent_node.RightNode = child_node;
			}

			if (delete_node.IsBlack())
			{
				this._FixAfterDeleteBlackOneChildNode(child_node);
			}
		}
		//删除孤独一个根节点
		else if (parent_node == null)
		{
			this.RootNode = null;
		}
		//叶子节点
		else
		{
			if (delete_node.IsBlack())
			{
				this._FixAfterDeleteBlackLeafNode(delete_node);
			}

			if (parent_node.LeftNode == delete_node)
			{
				parent_node.LeftNode = null;
			}
			else
			{
				parent_node.RightNode = null;
			}

		}

		delete_node.ParentNode = null;
		delete_node.LeftNode = null;
		delete_node.RightNode = null;

		return delete_node;
	}


	//删除的是黑色的叶子节点
	private void _FixAfterDeleteBlackLeafNode(RedBlackTreeNode curr_node)
	{
		while (true)
		{
			RedBlackTreeNode parent_node = curr_node.ParentNode;
			if (parent_node == null)//父节点都没，这个是root节点了,直接变黑了
			{
				curr_node.Color = RedBlackTreeNodeColor.Black;
				break;
			}

			//现在的节点是父节点的左节点
			if (parent_node.LeftNode == curr_node)
			{
				RedBlackTreeNode brother_node = parent_node.RightNode;//红黑树是满的2-3-4树，一定有兄弟节点，不用判null
				if (brother_node.IsRed())//还没有找到兄弟节点
				{
					//绕父节点左转
					brother_node.Color = RedBlackTreeNodeColor.Black;
					parent_node.Color = RedBlackTreeNodeColor.Red;
					this._RotateLeft(parent_node);
					parent_node = curr_node.ParentNode;
					brother_node = parent_node.RightNode;
				}

				//兄弟没得借
				if ((brother_node.LeftNode == null || brother_node.LeftNode.Color == RedBlackTreeNodeColor.Black) &&
					(brother_node.RightNode == null || brother_node.RightNode.Color == RedBlackTreeNodeColor.Black))
				{
					brother_node.Color = RedBlackTreeNodeColor.Red;//兄弟没得借就一起变红，减一层
					curr_node = curr_node.ParentNode;

					//遍历到顶
					if (curr_node == this.RootNode || curr_node.Color == RedBlackTreeNodeColor.Red)
					{
						curr_node.Color = RedBlackTreeNodeColor.Black;
						break;
					}
				}
				//兄弟有得借
				else
				{
					//如果兄弟节点的左节点不为空，则向右转
					if (brother_node.RightNode == null ||brother_node.RightNode.Color == RedBlackTreeNodeColor.Black)
					{
						brother_node.Color = RedBlackTreeNodeColor.Red;
						brother_node.LeftNode.Color = RedBlackTreeNodeColor.Black;
						this._RotateRight(brother_node);
						brother_node = parent_node.RightNode;
					}

					brother_node.Color = parent_node.Color;
					brother_node.RightNode.Color = RedBlackTreeNodeColor.Black;
					parent_node.Color = RedBlackTreeNodeColor.Black;
					this._RotateLeft(parent_node);
					break;
				}
			}
			//现在的节点是父节点的右节点
			else
			{
				RedBlackTreeNode brother_node = parent_node.LeftNode;//红黑树是满的2-3-4树，一定有兄弟节点，不用判null
				if (brother_node.IsRed())//还没有找到兄弟节点
				{
					//绕父节点左转
					brother_node.Color = RedBlackTreeNodeColor.Black;
					parent_node.Color = RedBlackTreeNodeColor.Red;
					this._RotateRight(parent_node);
					parent_node = curr_node.ParentNode;
					brother_node = parent_node.LeftNode;
				}

				//兄弟没得借
				if ((brother_node.LeftNode == null || brother_node.LeftNode.Color == RedBlackTreeNodeColor.Black) &&
					(brother_node.RightNode == null || brother_node.RightNode.Color == RedBlackTreeNodeColor.Black))
				{
					brother_node.Color = RedBlackTreeNodeColor.Red;//兄弟没得借就一起变红，减一层
					curr_node = curr_node.ParentNode;

					//遍历到顶
					if (curr_node == this.RootNode || curr_node.Color == RedBlackTreeNodeColor.Red)
					{
						curr_node.Color = RedBlackTreeNodeColor.Black;
						break;
					}
				}
				//兄弟有得借
				else
				{
					//如果兄弟节点的左节点不为空，则向右转
					if (brother_node.LeftNode == null || brother_node.LeftNode.Color == RedBlackTreeNodeColor.Black)
					{
						brother_node.Color = RedBlackTreeNodeColor.Red;
						brother_node.RightNode.Color = RedBlackTreeNodeColor.Black;
						this._RotateLeft(brother_node);
						brother_node = parent_node.LeftNode;
					}

					brother_node.Color = parent_node.Color;
					brother_node.LeftNode.Color = RedBlackTreeNodeColor.Black;
					parent_node.Color = RedBlackTreeNodeColor.Black;
					this._RotateRight(parent_node);
					break;
				}

			}
		}

		(this.RootNode as RedBlackTreeNode).Color = RedBlackTreeNodeColor.Black;
	}


	//删除的是黑色的，只有一个孩子的节点
	private void _FixAfterDeleteBlackOneChildNode(RedBlackTreeNode curr_node)
	{
		curr_node.Color = RedBlackTreeNodeColor.Black;
	}




	private void _RotateLeft(RedBlackTreeNode node_a)
	{
		RedBlackTreeNode node_b = node_a.RightNode;

		RedBlackTreeNode parent_node = node_a.ParentNode;
		if (parent_node != null)
		{
			if (parent_node.RightNode == node_a)
			{
				parent_node.RightNode = node_b;
			}
			else
			{
				parent_node.LeftNode = node_b;
			}
		}
		else
		{
			this.RootNode = node_b;
		}
		node_b.ParentNode = parent_node;

		RedBlackTreeNode bl_node = node_b.LeftNode;
		node_a.RightNode = bl_node;
		if (bl_node != null)
		{
			bl_node.ParentNode = node_a;
		}

		node_b.LeftNode = node_a;
		node_a.ParentNode = node_b;
	}


	private void _RotateRight(RedBlackTreeNode node_a)
	{
		RedBlackTreeNode node_b = node_a.LeftNode;

		RedBlackTreeNode parent_node = node_a.ParentNode;
		if (parent_node != null)
		{
			if (parent_node.RightNode == node_a)
			{
				parent_node.RightNode = node_b;
			}
			else
			{
				parent_node.LeftNode = node_b;
			}
		}
		else
		{
			this.RootNode = node_b;
		}
		node_b.ParentNode = parent_node;

		RedBlackTreeNode br_node = node_b.RightNode;
		node_a.LeftNode = br_node;
		if (br_node != null)
		{
			br_node.ParentNode = node_a;
		}

		node_b.RightNode = node_a;
		node_a.ParentNode = node_b;
	}



	private void _AddNode(RedBlackTreeNode node, int key)
	{
		RedBlackTreeNode curr_node = node;

		if (curr_node == null)
		{
			if (this.RootNode == null)
			{
				this.RootNode = new RedBlackTreeNode(key);
				curr_node = this.RootNode as RedBlackTreeNode;
				curr_node.Color = RedBlackTreeNodeColor.Black;
				curr_node.ParentNode = null;
				return;
			}
		}

		while (true)
		{
			if (key < curr_node.Key)
			{
				if (curr_node.LeftNode == null)
				{
					curr_node.LeftNode = new RedBlackTreeNode(key);
					curr_node.LeftNode.ParentNode = curr_node;
					curr_node = curr_node.LeftNode;
					break;
				}
				else
				{
					curr_node = curr_node.LeftNode;
				}
			}
			else if (key > curr_node.Key)
			{
				if (curr_node.RightNode == null)
				{
					curr_node.RightNode = new RedBlackTreeNode(key);
					curr_node.RightNode.ParentNode = curr_node;
					curr_node = curr_node.RightNode;
					break;
				}
				else
				{
					curr_node = curr_node.RightNode;
				}
			}
			else
			{
				Debug.LogErrorFormat("key {0} exist! ", key);
				return;
			}
		}

		if (curr_node != null)
		{
			this._FixAfterAddNode(curr_node);
		}

	}


	//插入节点后，调整红黑树平衡
	private void _FixAfterAddNode(RedBlackTreeNode curr_node)
	{
		curr_node.Color = RedBlackTreeNodeColor.Red;

		while (true)
		{
			RedBlackTreeNode parent_node = curr_node.ParentNode;
			if (parent_node != null)
			{
				//父节点是黑色，直接挂上去，不用处理
				if (parent_node.IsBlack())
				{
					break;
				}
				//父节点是红色
				else
				{
					RedBlackTreeNode grand_node = curr_node.GetGrandNode();
					if (grand_node != null)
					{
						//爷爷节点是黑色，叔叔是红色，则变色，不用旋转
						if (grand_node.LeftNode != null && grand_node.LeftNode.IsRed() &&
							grand_node.RightNode != null && grand_node.RightNode.IsRed())
						{
							grand_node.LeftNode.Color = RedBlackTreeNodeColor.Black;
							grand_node.RightNode.Color = RedBlackTreeNodeColor.Black;
							grand_node.Color = RedBlackTreeNodeColor.Red;
							curr_node = grand_node;//递归
						}
						//LL型二连红节点，右旋转
						else if (grand_node.LeftNode == parent_node && parent_node.LeftNode == curr_node)
						{
							parent_node.Color = RedBlackTreeNodeColor.Black;
							grand_node.Color = RedBlackTreeNodeColor.Red;
							this._RotateRight(grand_node);
							break;
						}
						//RR型二连红节点，左旋转
						else if (grand_node.RightNode == parent_node && parent_node.RightNode == curr_node)
						{
							parent_node.Color = RedBlackTreeNodeColor.Black;
							grand_node.Color = RedBlackTreeNodeColor.Red;
							this._RotateLeft(grand_node);
							break;
						}
						//LR型二连红节点，左旋后右旋
						else if (grand_node.LeftNode == parent_node && parent_node.RightNode == curr_node)
						{
							parent_node.Color = RedBlackTreeNodeColor.Red;
							grand_node.Color = RedBlackTreeNodeColor.Red;
							curr_node.Color = RedBlackTreeNodeColor.Black;
							this._RotateLeft(parent_node);
							this._RotateRight(grand_node);
							break;
						}
						//RL型二连红节点，右旋后左旋
						else if (grand_node.RightNode == parent_node && parent_node.LeftNode == curr_node)
						{
							parent_node.Color = RedBlackTreeNodeColor.Red;
							grand_node.Color = RedBlackTreeNodeColor.Red;
							curr_node.Color = RedBlackTreeNodeColor.Black;
							this._RotateRight(parent_node);
							this._RotateLeft(grand_node);
							break;
						}
					}
					//没有爷爷节点，好像没有可能
					else
					{
						break;
					}
				}
			}
			else
			{
				break;
			}
		}

		//1.第一个节点，不用处理,肯定黑色
		if (curr_node == this.RootNode)
		{
			curr_node.Color = RedBlackTreeNodeColor.Black;
		}
	}


}
