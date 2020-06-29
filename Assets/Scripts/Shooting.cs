using UnityEngine;

public class Shooting : MonoBehaviour 
{

	//---------------BULLET CODE--------------
	public Transform bulletSpawnpoint;
	public GameObject bulletPrefab;
	public float bulletForce = 8f;
	public float bulletTorque = 5f;

	public void Shoot(Vector2 playerDirVector)
	{
		SpawnBullet(playerDirVector);
		SpawnBullet(Quaternion.Euler(0f, 0f, 5f) * playerDirVector); // Rotates the vector by 10 degrees
		SpawnBullet(Quaternion.Euler(0f, 0f, -5f) * playerDirVector);
	}

	public void SpawnBullet(Vector2 bulletDir)
	{
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Gets the rigidbody component for the newly spawned prefab

		rb.AddForce(bulletDir * bulletForce, ForceMode2D.Impulse);
		rb.AddTorque(bulletTorque);
	}

}
