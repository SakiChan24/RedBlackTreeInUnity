using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITreeLinkWidget : MonoBehaviour
{
	[SerializeField]private Image image;

	public void Draw(Vector3 pos, Vector3 des)
	{
		RectTransform rect_trans = image.GetComponent<RectTransform>();
		float len = (des - pos).magnitude;
		rect_trans.sizeDelta = new Vector2(5f, len);
		transform.localPosition = pos;
		Vector3 dir = des - pos;
		float angle = -Vector3.SignedAngle(dir, Vector3.up, Vector3.forward) + 180f;
		rect_trans.localEulerAngles = new Vector3(0f, 0f, angle);

	}
}
