using UnityEngine;

public class ArenaBoundary : MonoBehaviour 
{

	private void OnTriggerEnter2D(Collider2D collision) // collision is other object
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerController>().Death();
		}
	}
}
