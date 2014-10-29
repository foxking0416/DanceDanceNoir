using UnityEngine;
using System.Collections;

public class InfiniteScroll : MonoBehaviour
{
//	public float scrollSpeedX;
//	public float scrollSpeedY;

	public float stepSize;

	private float currentOffset = 0.0f;
	
//	public void FixedUpdate()
//	{
//		float offsetX = Time.time * scrollSpeedX;
//		float offsetY = Time.time * scrollSpeedY;
//		renderer.material.mainTextureOffset = new Vector2( offsetX, offsetY );
//	}

	public void Move()
	{
		currentOffset += stepSize;
		renderer.material.mainTextureOffset = new Vector2( currentOffset, 0.0f );
	}
}