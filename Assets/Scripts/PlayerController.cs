using System;
using System.Collections;
using UnityEngine;
using Utils;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;
	public Rigidbody2D rb; // RigidBody component is what allows us to move our player
	public Animator animator;
	public SpriteRenderer playerRenderer;
	public Transform bulletSpawnpoint;
	private String playerDirCardinal = "E";
	private String prevDir; // Store direction from previous frame
	public Transform playerTransform;
	public float shootRate = 0.2f; // How many seconds between shots
	private float shootCooldown = 0f; // Tracks the time at which we can shoot again
	public float rocketRate = 1f;
	private float rocketCooldown = 0f;
	public float flashRate = 1f;
	private float flashCooldown = 0f;
	public float shieldRate = 1f;
	private float shieldCooldown = 0f;
	private Vector2 playerDirVector; // Tracks direction of player each frame

	Vector2 movement;

	private void Update() 
	// Updates once per frame
	{
		// Make controls variables? So that can reassign through menus

		prevDir = playerDirCardinal;
		playerDirCardinal = Utils.PlayerUtils.GetPlayerDir(movement); // Store current player direction
		if (playerDirCardinal == null) { playerDirCardinal = prevDir; } // Use prevDir if player is not moving
		playerDirVector = Utils.PlayerUtils.GetPlayerDirVector(playerDirCardinal);


		//============PLAYER CONTROLS============================
		if (Input.GetKey("j") && Time.time > shootCooldown) { // GetKey detects key holds, GetKeyDown does not
			GetComponent<Shooting>().Shoot(playerDirVector); // Get the ShootLogic script
			shootCooldown = Time.time + shootRate; // Set the next time that we're allowed to shoot
		}
		if (Input.GetKeyDown("k") && Time.time > rocketCooldown)
		{
			GetComponent<Rocketing>().Rocket(playerDirVector);
			rocketCooldown = Time.time + rocketRate;
		}
		if (Input.GetKeyDown("i") && Time.time > flashCooldown)
		{
			GetComponent<Flashing>().Flash(playerTransform, playerDirVector);
			flashCooldown = Time.time + flashRate;
		}
		if (Input.GetKeyDown("l") && Time.time > shieldCooldown)
		{
			GetComponent<Shielding>().Shield(playerTransform, playerDirVector);
			shieldCooldown = Time.time + shieldRate;
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
		// Maybe should only flip sprite, not the whole player to avoid hit box issues
		switch (playerDirCardinal)
		{
			case "E":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				bulletSpawnpoint.rotation = Quaternion.Euler(0f, 0f, 0f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = false;
				break;
			case "N":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				bulletSpawnpoint.rotation = Quaternion.Euler(0f, 0f, 90f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = false;
				break;
			case "W":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				bulletSpawnpoint.rotation = Quaternion.Euler(0f, 0f, 180f);
				playerRenderer.flipX = true;
				playerRenderer.flipY = false;
				break;
			case "S":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				bulletSpawnpoint.rotation = Quaternion.Euler(0f, 0f, 270f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = true;
				break;
			case "NE":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				bulletSpawnpoint.rotation = Quaternion.Euler(0f, 0f, 45f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = false;
				break;
			case "NW":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
				bulletSpawnpoint.rotation = Quaternion.Euler(0f, 0f, 135f);
				playerRenderer.flipX = true;
				playerRenderer.flipY = false;
				break;
			case "SW":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, 60f);
				bulletSpawnpoint.rotation = Quaternion.Euler(0f, 0f, 225f);
				playerRenderer.flipX = true;
				playerRenderer.flipY = false;
				break;
			case "SE":
				playerTransform.rotation = Quaternion.Euler(0f, 0f, -60f);
				bulletSpawnpoint.rotation = Quaternion.Euler(0f, 0f, -45f);
				playerRenderer.flipX = false;
				playerRenderer.flipY = false;
				break;
		}
	}
}
