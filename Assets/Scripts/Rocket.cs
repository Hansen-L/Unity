using UnityEngine;

public class Rocket : MonoBehaviour // Maybe this should inherit from a class common to shot.cs
{
	public GameObject rocketEffect;
	public float massReduction = 2f; // How much to reduce mass when hit
	public float knockbackForce = 1f;
	public float knockbackDuration = 1f;
	public ScreenShake screenShake;
	private bool isExploded = false; // Makes sure we don't explode rocket twice

	private void Awake()
	{
		screenShake = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
		{
			Vector2 knockbackDir = collision.otherRigidbody.velocity.normalized; 
			PlayerController playerControllerInstance = collision.gameObject.GetComponent<PlayerController>();
			StartCoroutine(playerControllerInstance.Knockback(knockbackDuration, knockbackForce, knockbackDir));
		}

		collision.gameObject.GetComponent<Rigidbody2D>().mass -= massReduction;
		RocketExplode();
	}

	public void RocketExplode()
	{
		if (!isExploded)
		{
			StartCoroutine(screenShake.Shake(0.2f, 0.1f));
			GameObject effect = Instantiate(rocketEffect, transform.position, Quaternion.identity);
			GetComponent<SpriteRenderer>().enabled = false; // Disable renderer and rb when collided. We can't delete the bullet right away or the coroutine terminates
			GetComponent<CircleCollider2D>().enabled = false;
			Destroy(effect, 5f);
			Destroy(gameObject, knockbackDuration + 1f);
			isExploded = true;
		}
	}
}

