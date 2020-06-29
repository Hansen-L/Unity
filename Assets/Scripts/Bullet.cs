using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public GameObject hitEffect;
	public float massReduction = 0.5f; // How much to reduce mass when hit

	private void OnCollisionEnter2D(Collision2D collision)
	{
		collision.gameObject.GetComponent<Rigidbody2D>().mass -= massReduction; // Reduce mass when hit
		BulletExplode();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		BulletExplode();
	}

	public void BulletExplode()
	{
		GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
		Destroy(effect, 5f);
		Destroy(gameObject);
	}

}
