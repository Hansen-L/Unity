using UnityEngine;

public class Shooting : MonoBehaviour 
{

	//---------------BULLET CODE--------------
	public Transform bulletSpawnpoint;
	public GameObject bulletPrefab;
	public float bulletForce = 8f;
	public float bulletTorque = 5f;

	public void Shoot(Vector2 playerDirVector, int playerNum)
	{
		SpawnBullet(playerDirVector, playerNum);
		SpawnBullet(Quaternion.Euler(0f, 0f, 5f) * playerDirVector, playerNum); // Rotates the vector by 10 degrees
		SpawnBullet(Quaternion.Euler(0f, 0f, -5f) * playerDirVector, playerNum);
	}

	public void SpawnBullet(Vector2 bulletDir, int playerNum)
	{
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Gets the rigidbody component for the newly spawned prefab

		if (playerNum == 1) { bullet.layer = 10; }
		else { bullet.layer = 13; }

		rb.AddForce(bulletDir * bulletForce, ForceMode2D.Impulse);
		rb.AddTorque(bulletTorque);
	}

}
