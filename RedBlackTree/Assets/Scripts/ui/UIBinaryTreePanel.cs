using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBinaryTreePanel : MonoBehaviour
{
	[SerializeField] private GameObject m_tree_node_prefab;
	[SerializeField] private GameObject m_tree_link_prefab;

	[SerializeField] private InputField m_key_input;
	[SerializeField] private Button m_add_button;
	[SerializeField] private GameObject m_draw_node;
	[SerializeField] private GameObject m_draw_nodes_node;
	[SerializeField] private GameObject m_draw_links_node;

	private const float FOOT_HEIGHT = 75f;
	private const float FOOT_OFFSET = 480f;
	private const float SCALE_NODE_POS_Y = -FOOT_HEIGHT * 6f;

	private RedBlackTree m_tree = new RedBlackTree();

	private void Start()
	{
		m_add_button.onClick.AddListener(() =>
		{
			this._OnClickAddButton();
		});

		// test code 
		//m_tree.AddNode(4);
		//m_tree.AddNode(2);
		//m_tree.AddNode(6);
		//m_tree.AddNode(1);
		//m_tree.AddNode(3);
		//m_tree.AddNode(5);
		//m_tree.AddNode(9);
		//m_tree.AddNode(0);
		//m_tree.AddNode(8);
		//m_tree.AddNode(11);
		//m_tree.AddNode(7);
		//m_tree.AddNode(10);

		//for (int i = 1; i <= 10; ++i)
		//{
		//	m_tree.AddNode(i);
		//}


		this.DrawTree();

	}


	private void Update()
	{
	}


	private void DrawTree()
	{
		this._ClearTree();

		this._DrawTree(m_tree.GetRootNode(), Vector3.zero, FOOT_OFFSET, Vector3.zero, false);
	}

	private void _OnClickAddButton()
	{
		int key_value = int.Parse(m_key_input.text);
		m_tree.AddNode(key_value);
		this.DrawTree();
	}



	private void _ClearTree()
	{
		this._ClearNode(m_draw_nodes_node);
		this._ClearNode(m_draw_links_node);
	}

	private void _ClearNode(GameObject node)
	{
		int child_count = node.transform.childCount;
		for (int i = child_count - 1; i >= 0; --i)
		{
			Transform trans = node.transform.GetChild(i);
			GameObject go = trans.gameObject;
			GameObject.Destroy(go);
		}
	}


	private void _DrawTree(RedBlackTreeNode node, Vector3 center_point, float offset_x, Vector3 parent_pos, bool is_draw_link)
	{
		if (node == null)
		{
			return;
		}

		GameObject node_go = GameObject.Instantiate<GameObject>(m_tree_node_prefab, m_draw_nodes_node.transform);
		UITreeNodeWidget node_widget = node_go.GetComponent<UITreeNodeWidget>();
		node_widget.SetNode(node);
		node_widget.OnClickCallback = (int key) =>
		{
			m_tree.DeleteNode(key);

			this.DrawTree();
		};


		Vector3 next_center_point = new Vector3(center_point.x, center_point.y - FOOT_HEIGHT, center_point.z);
		Vector3 node_pos = new Vector3(next_center_point.x, next_center_point.y, next_center_point.z);


		if (is_draw_link)
		{
			GameObject link_go = GameObject.Instantiate<GameObject>(m_tree_link_prefab, m_draw_links_node.transform);
			UITreeLinkWidget link_widget = link_go.GetComponent<UITreeLinkWidget>();
			link_widget.Draw(parent_pos, node_pos);
		}

		if (node_pos.y < SCALE_NODE_POS_Y)
		{
			node_go.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			node_go.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		}

		node_go.transform.localPosition = node_pos;


		if (null != node.LeftNode)
		{
			Vector3 left_center_point = next_center_point;
			left_center_point.x -= offset_x;
			this._DrawTree(node.LeftNode, left_center_point, offset_x / 2f, node_pos, true);
		}

		if (null != node.RightNode)
		{
			Vector3 right_center_point = next_center_point;
			right_center_point.x += offset_x;
			this._DrawTree(node.RightNode, right_center_point, offset_x / 2f, node_pos, true);
		}
	}

}
