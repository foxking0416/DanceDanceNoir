using UnityEngine;
using System.Collections;

public class InfiniteScroll : MonoBehaviour
{
	public float scrollSpeedX;
	public float scrollSpeedY;
	
	public void FixedUpdate()
	{
		float offsetX = Time.time * scrollSpeedX;
		float offsetY = Time.time * scrollSpeedY;
		renderer.material.mainTextureOffset = new Vector2( offsetX, offsetY );
	}
}