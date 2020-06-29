using System;
using System.Collections;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
	public float moveSpeed = 5f;
	public Rigidbody2D rb; // RigidBody component is what allows us to move our player
	public Animator animator;
	public SpriteRenderer playerRenderer;
	private String playerDirCardinal = "E";
	private String prevDir; // Store direction from previous frame
	public Transform playerTransform;
	public float shootRate = 0.2f; // How many seconds between shots
	private float shootCooldown = 0f; // Tracks the time at which we can shoot again
	public float rocketRate = 1f;
	private float rocketCooldown = 0f;
	private Vector2 playerDirVector; // Tracks direction of player each frame

	Vector2 movement;

	private void Update() 
	// Updates once per frame
	{
		prevDir = playerDirCardinal;
		playerDirCardinal = Utils.PlayerUtils.GetPlayerDir(movement); // Store current player direction
		if (playerDirCardinal == null) { playerDirCardinal = prevDir; } // Use prevDir if player is not moving
		playerDirVector = Utils.PlayerUtils.GetPlayerDirVector(playerDirCardinal);

		if (Input.GetKey("j") && Time.time > shootCooldown) { // GetKey detects key holds, GetKeyDown does not
			Shoot();
			shootCooldown = Time.time + shootRate; // Set the next time that we're allowed to shoot
		}
		if (Input.GetKeyDown("k") && Time.time > rocketCooldown)
		{
			Rocket();
			rocketCooldown = Time.time + rocketRate;
		}

		if (Input.GetKeyDown("l"))
		{
			Flash();
		}

		ProcessMovement(); // Animate and normalize movement
	}

	void FixedUpdate() 
	// Called 50 times per second by default, used for physics updates
	{
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Moves player based on movement vector
	}

	void ProcessMovement() 
	// Sets the proper animation for the movement and normalizes movement vector
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		if (movement.x != 0 || movement.y != 0)
		{
			// Passing non normalized vector into animator
			animator.SetFloat("horizontal", movement.x);
			animator.SetFloat("vertical", movement.y);

			// Normalize movement vector
			movement.x = movement.x / movement.magnitude;
			movement.y = movement.y / movement.magnitude;

			animator.SetBool("isMoving", true);
		}
		else // If not moving
		{
			animator.SetBool("isMoving", false);
		}
		FlipSprite();
	}

	void FlipSprite() 
	// Flips animations for different directions
	{
		// Flip sprite as needed
		switch (playerDirCardinal)
		{
			case "E":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = false;
				break;
			case "N":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = false;
				break;
			case "W":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				playerRenderer.flipX = true;
				playerRenderer.flipY = false;
				break;
			case "S":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = true;
				break;
			case "NE":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = false;
				break;
			case "NW":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				playerRenderer.flipX = true;
				playerRenderer.flipY = false;
				break;
			case "SW":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 60f);
				playerRenderer.flipX = true;
				playerRenderer.flipY = false;
				break;
			case "SE":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, -60f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = false;
				break;
		}
	}

	//---------------BULLET CODE--------------
	public Transform bulletSpawnpoint;
	public GameObject bulletPrefab;
	public float bulletForce = 10f;
	public float bulletTorque = 20f;

	public void Shoot()
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

	//---------------ROCKET CODE--------------
	public GameObject rocketPrefab;
	public float rocketAccel = 10f;
	public float rocketTorque = 20f;
	public float rocketMaxDist = 30f; // How far the rocket can go before exploding
	private float spawnOffsetDist = 0.5f; // How much to offset spawnpoint of rocket

	public void Rocket()
	{
		StartCoroutine(screenShake.Shake(0.3f, 0.01f));
		Vector3 offsetVec = new Vector3(playerDirVector.x, playerDirVector.y, 0) * spawnOffsetDist;
		GameObject rocket = Instantiate(rocketPrefab, bulletSpawnpoint.position + offsetVec, bulletSpawnpoint.rotation);
		Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>(); // Gets the rigidbody component for the newly spawned prefab

		rb.AddForce(playerDirVector * 3f, ForceMode2D.Impulse);
		rb.AddTorque(bulletTorque);
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


	//----------FLASH CODE-----------
	public float flashDist = 3f; // How far to flash
	public GameObject flashStartPrefab;
	public GameObject flashEndPrefab;
	public ScreenShake screenShake;

	public void Flash()
	{
		// Shake screen
		StartCoroutine(screenShake.Shake(0.1f, 0.2f));

		// Flash start animation before moving player
		GameObject flashStart = Instantiate(flashStartPrefab, playerTransform.position, playerTransform.rotation);

		// Move player
		Vector3 playerDirVector3D = new Vector3(playerDirVector.x, playerDirVector.y, 0);
		playerTransform.position = transform.position + playerDirVector3D * flashDist;

		// Flash end animation after moving player
		GameObject flashEnd = Instantiate(flashEndPrefab, playerTransform.position, playerTransform.rotation);
		flashEnd.transform.parent = gameObject.transform;

		Destroy(flashStart, 2f);
		Destroy(flashEnd, 2f);
	}

}
