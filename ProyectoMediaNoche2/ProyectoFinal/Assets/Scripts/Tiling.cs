using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;

	public bool hasRightBuddy = false;
	public bool hasLeftBuddy = false;

    public bool reverseScale = false;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform tf;

	void Awake()
	{
		cam = Camera.main;
		tf = transform;
	}

	// Use this for initialization
	void Start () 
	{
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( !hasLeftBuddy || !hasRightBuddy )
		{
			float camHroziontalExtent = cam.orthographicSize * Screen.width/Screen.height;

			float edgeVisiblePosRight = (tf.position.x + spriteWidth/2) - camHroziontalExtent;
			float edgeVisiblePosLeft = (tf.position.x - spriteWidth/2) + camHroziontalExtent;

			if(cam.transform.position.x >= edgeVisiblePosRight - offsetX && !hasRightBuddy)
			{
				makeBuddy(1);
				hasRightBuddy = true;
			}
			else if(cam.transform.position.x <= edgeVisiblePosLeft + offsetX && !hasLeftBuddy)
			{
				makeBuddy(-1);
				hasLeftBuddy = true;
			}
		}
	}

	void makeBuddy(int RoL)
	{


		Vector3 pos = new Vector3(tf.position.x + spriteWidth * RoL, tf.position.y, tf.position.z);
		Transform buddy = (Transform)Instantiate(transform, pos, tf.rotation);

		if(reverseScale)
			buddy.localScale = new Vector3(buddy.localScale.x*-1, buddy.localScale.y, buddy.localScale.z);

		buddy.parent = tf.parent;
		if(RoL > 0)
			buddy.GetComponent<Tiling>().hasLeftBuddy = true;
		
		else
			buddy.GetComponent<Tiling>().hasRightBuddy = true;
	}
}
