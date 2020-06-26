using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public GameObject hitEffect;
	public float massReduction = 0.5f; // How much to reduce mass when hit

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Prevent self collision of bullets
		if (collision.collider.tag == "Bullet")
		{
			Debug.Log(collision.gameObject.tag);
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
		else
		{
			collision.gameObject.GetComponent<Rigidbody2D>().mass -= massReduction; // Reduce mass when hit
			GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
			Destroy(effect, 5f);
			Destroy(gameObject);
		}
	}
}
