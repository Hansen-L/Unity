using UnityEngine;
using System.Collections;

public class Shielding : MonoBehaviour 
{
	//---------------ROCKET CODE--------------
	public GameObject shieldPrefab;
	public Transform bulletSpawnpoint;
	public ScreenShake screenShake;
	public float shieldVelocity = 10f;
	public float shieldMaxDist = 5f; // How far the rocket can go before exploding
	private float spawnOffsetDist = 0.5f; // How much to offset spawnpoint of rocket

	public void Shield(Transform playerTransform, Vector2 playerDirVector)
	{
		// Spawn the rope
		Vector3 offsetVec = new Vector3(playerDirVector.x, playerDirVector.y, 0) * spawnOffsetDist;
		GameObject shield = Instantiate(shieldPrefab, bulletSpawnpoint.position + offsetVec, bulletSpawnpoint.rotation);
		Rigidbody2D rb = shield.GetComponent<Rigidbody2D>(); // Gets the rigidbody component for the newly spawned prefab

		// Move the rope out at a constant velocity
		Vector2 velocityVector = playerDirVector * shieldVelocity;
		rb.velocity = velocityVector;
		StartCoroutine(MoveShield(rb, gameObject));
	}

	IEnumerator MoveShield(Rigidbody2D rb, GameObject shield) // Track shield position so we can stop it at certain distance
	{
		Vector2 initialPos = rb.position;
		while (rb)
		{
			if ((rb.position - initialPos).magnitude > shieldMaxDist)
			{
				rb.velocity = new Vector2(0f, 0f);
			}
			yield return null;
		}
	}
}
