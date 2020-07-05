using UnityEngine;

public class Rocket : MonoBehaviour // Maybe this should inherit from a class common to shot.cs
{
	public GameObject rocketEffect;
	public ScreenShake screenShake;
	public float baseKnockback = 30f;
	public float knockbackDuration = 1f;
	public float damageDealt = 20f;
	public float explosionRadius = 3f;
	private bool isExploded = false; // Makes sure we don't explode rocket twice

	private void Awake()
	{
		screenShake = GameObject.Find("Main Camera").GetComponent<ScreenShake>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		RocketExplode();
	}

	public void RocketExplode()
	{
		if (!isExploded)
		{
			GetComponent<CircleCollider2D>().enabled = false;
			GetComponent<SpriteRenderer>().enabled = false; // Disable renderer and rb when collided. We can't delete the bullet right away or the coroutine terminates
			ExplosionRadius();
			StartCoroutine(screenShake.Shake(0.2f, 0.1f));
			GameObject effect = Instantiate(rocketEffect, transform.position, Quaternion.identity);
			Destroy(effect, 5f);
			Destroy(gameObject, knockbackDuration + 1f);
			isExploded = true;
		}
	}


	private void ExplosionRadius() // Destroy or apply knockback to objects in the explosion radius
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

		foreach (Collider2D collider in colliders)
		{
			if (collider.gameObject.tag == "Player")
			{
				Vector2 knockbackDir = (collider.gameObject.transform.position - this.transform.position).normalized; // Get vector between rocket and player
				PlayerController playerControllerInstance = collider.gameObject.GetComponent<PlayerController>();
				collider.gameObject.GetComponent<PlayerController>().damagePercent += damageDealt;
				StartCoroutine(playerControllerInstance.Knockback(knockbackDuration, baseKnockback, knockbackDir));
			}
			else if (collider.gameObject.tag == "Bullet")
			{
				collider.gameObject.GetComponent<Shot>().BulletExplode();
			}
			else if (collider.gameObject.tag == "Rocket")
			{
				collider.gameObject.GetComponent<Rocket>().RocketExplode();
			}
			else if (collider.gameObject.tag == "Shield")
			{
				Destroy(collider.gameObject);
			}
		}
	}

	//void OnDrawGizmos()
	//{
	//	// Draw a yellow sphere at the transform's position
	//	Gizmos.color = Color.yellow;
	//	Gizmos.DrawSphere(transform.position, explosionRadius);
	//}
}

