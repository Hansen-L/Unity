using UnityEngine;

public class Flashing : MonoBehaviour 
{
	//----------FLASH CODE-----------
	public float flashDist = 3f; // How far to flash
	public GameObject flashStartPrefab;
	public GameObject flashEndPrefab;
	public ScreenShake screenShake;

	public void Flash(Transform playerTransform, Vector2 playerDirVector)
	{
		// Shake screen
		StartCoroutine(screenShake.Shake(0.1f, 0.2f));

		// Flash start animation before moving player
		GameObject flashStart = Instantiate(flashStartPrefab, playerTransform.position, playerTransform.rotation);

		// Move player
		Vector3 playerDirVector3D = new Vector3(playerDirVector.x, playerDirVector.y, 0);
		playerTransform.position = transform.position + playerDirVector3D * flashDist;

		// Flash end animation after moving player
		GameObject flashEnd = Instantiate(flashEndPrefab, playerTransform.position, playerTransform.rotation);
		flashEnd.transform.parent = gameObject.transform;

		Destroy(flashStart, 2f);
		Destroy(flashEnd, 2f);
	}
}
