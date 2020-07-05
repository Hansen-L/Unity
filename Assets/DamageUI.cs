using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using System.Collections;

public class DamageUI : MonoBehaviour 
{
	public TextMeshProUGUI damageText;
	public PlayerController player;

	private float originalSize;
	private Color originalColor;

	void Awake()
	{
		originalSize = damageText.fontSize;
		originalColor = damageText.color;
	}

	private string prevDamage = "0%";

	void Update()
	{
		damageText.text = player.damagePercent + "%";
		if (damageText.text != prevDamage) // Track previous damage so that we know to update when it changes. Could have this code called from playercontroller, but this seems cleaner
		{
			StartCoroutine(updateDamageAnim());
			prevDamage = damageText.text;
		}
	}

	private IEnumerator updateDamageAnim() // Animate the damage update
	{
		float duration = 0.2f; // Animation total duration
		float step = 0.02f; // Step between animation frames

		for (int i = 0; i <= duration/step; i++) {
			damageText.fontSize = originalSize + i/2 + Random.Range(-2, 2);
			damageText.color = new Color(0.95f + Random.Range(-0.8f, 0.05f), 0f, 0f);
			yield return new WaitForSeconds(step);
		}

		damageText.fontSize = originalSize;
		damageText.color = originalColor;
	}
}
