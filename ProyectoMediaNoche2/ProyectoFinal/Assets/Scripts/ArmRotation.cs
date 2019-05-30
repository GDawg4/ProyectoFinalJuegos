using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {

	public int rotationOffset = 0;


	void Update () 
		{
		Vector3 mousePos = Input.mousePosition;
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos = mousePos - pos;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - rotationOffset));
	}
}
