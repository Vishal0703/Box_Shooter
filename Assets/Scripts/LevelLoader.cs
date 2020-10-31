using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	// Start is called before the first frame update
	public string level = "Level1";
	void OnCollisionEnter(Collision newCollision)
	{
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile")
		{
			// call the RestartGame function in the game manager
			GameManager.gm.LevelLoad(level);
		}
	}
}
