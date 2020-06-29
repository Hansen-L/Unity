using UnityEngine;

public class Rocket : MonoBehaviour
{
	public GameObject rocketEffect;
	public float massReduction = 2f; // How much to reduce mass when hit

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collision.gameObject.GetComponent<Rigidbody2D>().mass -= massReduction; // Reduce mass when hit
		RocketExplode();
	}

	private void OnTriggerEnter2D(Collider2D collision) // For objects that are triggers, don't show physics of impact (like for RopeToy)
	{
		RocketExplode();
	}

	public void RocketExplode()
	{
		GameObject effect = Instantiate(rocketEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);
		Destroy(gameObject);
	}
}

