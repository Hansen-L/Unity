using System;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
	public float moveSpeed = 5f;
	public Rigidbody2D rb; // RigidBody component is what allows us to move our player
	public Animator animator;
	public SpriteRenderer playerRenderer;
	private String playerDir = "E";
	private String prevDir; // Store direction from previous frame
	public Transform playerTransform;
	public float fireRate = 0.2f; // How many seconds between shots
	private float shootCooldown = 0f; // Tracks the time at which we can shoot again

	Vector2 movement;

	private void Update() 
	// Updates once per frame
	{
		prevDir = playerDir;
		playerDir = Utils.Utils.GetPlayerDir(movement); // Store current player direction
		if (playerDir == null) { playerDir = prevDir; } // Use prevDir if player is not moving

		if (Input.GetKey("j") && Time.time > shootCooldown) { // GetKey detects key holds, GetKeyDowen does not
			Shoot();
			shootCooldown = Time.time + fireRate; // Set the next time that we're allowed to shoot
		}

		if (Input.GetKeyDown("k"))
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
		switch (playerDir)
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

	public Vector2 GetPlayerDirVector()
	// Return a unit vector in the direction the player is facing
	{
		Vector2 vec = new Vector2(0, 0);

		switch (playerDir)
		{
			case "E":
				vec.Set(1, 0);
				break;
			case "N":
				vec.Set(0, 1);
				break;
			case "W":
				vec.Set(-1, 0);
				break;
			case "S":
				vec.Set(0, -1);
				break;
			case "NE":
				vec.Set(1, 1);
				break;
			case "NW":
				vec.Set(-1, 1);
				break;
			case "SW":
				vec.Set(-1, -1);
				break;
			case "SE":
				vec.Set(1, -1);
				break;
		}

		vec = vec.normalized; // Normalize so diagonal directions are scaled
		return vec;
	}

	//---------------BULLET CODE--------------
	public Transform bulletSpawnpoint;
	public GameObject bulletPrefab;
	public float bulletForce = 10f;
	public float bulletTorque = 20f;

	public void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Gets the rigidbody component for the newly spawned prefab

		rb.AddForce(GetPlayerDirVector() * bulletForce, ForceMode2D.Impulse);
		rb.AddTorque(bulletTorque);


	}


	//----------Flash Code-----------
	public float flashDist = 5f; // How far to flash

	public void Flash()
	{
		Vector3 playerDirVector3D = new Vector3(GetPlayerDirVector().x, GetPlayerDirVector().y, 0);
		playerTransform.position = transform.position + playerDirVector3D * flashDist;
	}

}
