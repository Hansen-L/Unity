using System;
using UnityEngine;
using Utils;

public class RobotCat : MonoBehaviour 
{
    public ScreenShake screenShake;

    private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Bullet")
		{
			StartCoroutine(screenShake.Shake(0.05f, 0.05f)); // Maybe this should live on the bullet, but it doesn't work right now
		}
		else if (collision.collider.tag == "Rocket")
		{
			StartCoroutine(screenShake.Shake(0.3f, 0.3f));
		}
	}
}
