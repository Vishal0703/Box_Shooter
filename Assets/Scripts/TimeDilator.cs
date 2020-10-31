using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TimeDilator : MonoBehaviour
{
	public float timescale = 0.3f;
	public GameObject explosionPrefab;
	// respond on collisions
	void OnCollisionEnter(Collision newCollision)
	{
		if (GameManager.gm)
		{
			if (GameManager.gm.gameIsOver)
				return;
		}
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile")
		{
			// call the RestartGame function in the game manager
			if (explosionPrefab)
			{
				// Instantiate an explosion effect at the gameObjects position and rotation
				Instantiate(explosionPrefab, transform.position, transform.rotation);
			}
			GameManager.gm.DilateTime(timescale);

			Destroy(newCollision.gameObject);

			// destroy self
			Destroy(gameObject);
		}

	}
}
