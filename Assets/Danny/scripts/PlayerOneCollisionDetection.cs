using UnityEngine;
using System.Collections;

public class PlayerOneCollisionDetection : MonoBehaviour
{
	// Collision detection.
	void OnTriggerEnter( Collider triggerCollider )
	{
		Debug.Log( "OnTriggerEnter()" );
	}
	void OnCollisionEnter2D( Collision2D collider )
	{
		Debug.Log( "OnCollisionEnter2D()" );
	}
}