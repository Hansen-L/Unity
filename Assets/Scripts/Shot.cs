using UnityEngine;

public class Shot : MonoBehaviour
{
	public GameObject hitEffect;
	public float massReduction = 0.1f; // How much to reduce mass when hit
	public float knockbackForce = 1f;
	public float knockbackDuration = 1f;
	public ScreenShake screenShake;

	private void Awake()
	{
		screenShake = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
	}
	private void OnCollisionEnter2D(Collision2D collision) // Collision refers to the object being collided with
	{
		if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
		{
			Vector2 knockbackDir = collision.otherRigidbody.velocity.normalized; // Use velocity of bullet to determine knockback
			PlayerController playerControllerInstance = collision.gameObject.GetComponent<PlayerController>(); // Get access to the script on the player component
			StartCoroutine(playerControllerInstance.Knockback(knockbackDuration, knockbackForce, knockbackDir));
		}

		collision.gameObject.GetComponent<Rigidbody2D>().mass -= massReduction; // Reduce mass when hit
		BulletExplode();

	}

	public void BulletExplode()
	{
		StartCoroutine(screenShake.Shake(0.1f, 0.1f));
		GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
		GetComponent<SpriteRenderer>().enabled = false; // Disable renderer and rb when collided. We can't delete the bullet right away or the coroutine terminates
		GetComponent<BoxCollider2D>().enabled = false;
		Destroy(effect, 5f);
		Destroy(gameObject, knockbackDuration + 1f);
	}

}
