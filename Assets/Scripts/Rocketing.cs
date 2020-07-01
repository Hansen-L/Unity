using UnityEngine;
using System.Collections;

public class Rocketing : MonoBehaviour 
{
	//---------------ROCKET CODE--------------
	public GameObject rocketPrefab;
	public Transform bulletSpawnpoint;
	public ScreenShake screenShake;
	public float rocketAccel = 10f;
	public float rocketTorque = 20f;
	public float rocketMaxDist = 30f; // How far the rocket can go before exploding
	private float spawnOffsetDist = 0.5f; // How much to offset spawnpoint of rocket

	public void Rocket(Vector2 playerDirVector, int playerNum)
	{
		StartCoroutine(screenShake.Shake(0.3f, 0.01f));
		Vector3 offsetVec = new Vector3(playerDirVector.x, playerDirVector.y, 0) * spawnOffsetDist;
		GameObject rocket = Instantiate(rocketPrefab, bulletSpawnpoint.position + offsetVec, bulletSpawnpoint.rotation);

		// Set different layers for rockets from each player to enable collision between different players' projectiles
		if (playerNum == 1) { rocket.layer = 11; }
		else { rocket.layer = 14; }

		Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>(); // Gets the rigidbody component for the newly spawned prefab

		rb.AddForce(playerDirVector * 3f, ForceMode2D.Impulse);
		rb.AddTorque(rocketTorque);
		StartCoroutine(AccelerateRocket(rb, playerDirVector, rocket));
	}

	IEnumerator AccelerateRocket(Rigidbody2D rb, Vector2 direction, GameObject rocket) // Apply a force each frame to simulate acceleration
	{
		Vector2 initialPos = rb.position;
		while (rb)
		{
			rb.AddForce(direction * rocketAccel, ForceMode2D.Force);

			if ((rb.position - initialPos).magnitude > rocketMaxDist) // Explode rocket if reached max dist
			{
				rocket.GetComponent<Rocket>().RocketExplode();
				StartCoroutine(screenShake.Shake(0.2f, 0.1f)); // Change this in RobotCat.cs too
			}
			yield return null;
		}
	}
}
