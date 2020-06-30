using UnityEngine;
using System.Collections;
using System;

public class Shielding : MonoBehaviour 
{
	//---------------SHIELD CODE--------------
	public GameObject shieldPrefab;
	public Transform bulletSpawnpoint;
	public ScreenShake screenShake;
	public float shieldVelocity = 10f;
	public float shieldMaxDist = 5f;
	private float spawnOffsetDist = 0.5f;
	private Vector3 scaleIncrement = new Vector3(0.1f, 0.1f, 0.1f);

	public void Shield(Transform playerTransform, Vector2 playerDirVector)
	{
		// Spawn the rope
		Vector3 offsetVec = new Vector3(playerDirVector.x, playerDirVector.y, 0) * spawnOffsetDist;
		GameObject shield = Instantiate(shieldPrefab, bulletSpawnpoint.position + offsetVec, bulletSpawnpoint.rotation * Quaternion.Euler(0f, 0f, 90f));
		Rigidbody2D rb = shield.GetComponent<Rigidbody2D>(); // Gets the rigidbody component for the newly spawned prefab
		shield.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // Start small and grow

		// Move the rope out at a constant velocity
		Vector2 velocityVector = playerDirVector * shieldVelocity;
		rb.velocity = velocityVector;
		StartCoroutine(MoveShield(rb, shield));
	}

	IEnumerator MoveShield(Rigidbody2D rb, GameObject shield) // Track shield position so we can stop it at certain distance
	{
		Vector2 initialPos = rb.position;
		while (rb)
		{
			if (shield.transform.localScale.x < 1)
			{
				shield.transform.localScale += scaleIncrement;
			}

			if ((rb.position - initialPos).magnitude > shieldMaxDist)
			{
				rb.velocity = new Vector2(0f, 0f);
			}
			yield return null;
		}
	}
}
