using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITreeNodeWidget : MonoBehaviour
{
	[SerializeField] private Text m_text;
	[SerializeField] private Button m_button;
	[SerializeField] private Image m_button_image;

	public System.Action<int> OnClickCallback;

	private int m_key;

	void Start()
	{
		m_button.onClick.AddListener(() =>
		{
			if (this.OnClickCallback != null)
			{
				this.OnClickCallback(m_key);
			}
		});
	}

	void Update()
	{
	}


	public void SetNode(RedBlackTreeNode node)
	{
		m_key = node.Key;
		m_text.text = m_key.ToString();

		if (node.Color == RedBlackTreeNodeColor.Red)
		{
			m_button_image.color = Color.red;
		}
		else
		{
			m_button_image.color = Color.black;
		}
	}

}
