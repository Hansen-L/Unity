using UnityEngine;

public class ArenaBoundary : MonoBehaviour 
{

	private void OnTriggerEnter2D(Collider2D collision) // collision is other object
	{
		Destroy(collision.gameObject);
	}
}
