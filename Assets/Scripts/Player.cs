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

	Vector2 movement;

	private void Update() // Updates once per frame
	{
		if (Input.GetKeyDown("j")) { Shoot(); }

		prevDir = playerDir;
		playerDir = Utils.Utils.GetPlayerDir(movement); // Store current player direction
		if (playerDir == null) { playerDir = prevDir; }

		processMovement(); // Animate and normalize movement
	}

	void FixedUpdate() // Called 50 times per second by default, used for physics updates
	{
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Moves player based on movement vector
	}

	void processMovement() // Sets the proper animation for the movement and normalizes movement vector
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
		flipSprite();
	}

	void flipSprite() // Flips animations for different directions
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

	//---------------BULLET CODE--------------
	public Transform bulletSpawnpoint;
	public GameObject bulletPrefab;
	public float bulletForce = 10f;
	public float bulletTorque = 20f;

	public void Shoot()
	{
		Vector2 bulletDirection = new Vector2(0, 0);

		GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Gets the rigidbody component for the newly spawned prefab

		// Assign bullet direction based on player direction
		switch (playerDir)
		{
			case "E":
				bulletDirection.Set(1, 0);
				break;
			case "N":
				bulletDirection.Set(0, 1);
				break;
			case "W":
				bulletDirection.Set(-1, 0);
				break;
			case "S":
				bulletDirection.Set(0, -1);
				break;
			case "NE":
				bulletDirection.Set(1, 1);
				break;
			case "NW":
				bulletDirection.Set(-1, 1);
				break;
			case "SW":
				bulletDirection.Set(-1, -1);
				break;
			case "SE":
				bulletDirection.Set(1, -1);
				break;
		}

		bulletDirection = bulletDirection.normalized;
		Debug.Log(bulletDirection);
		Debug.Log(playerDir);

		rb.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);
		rb.AddTorque(bulletTorque);


	}

}
