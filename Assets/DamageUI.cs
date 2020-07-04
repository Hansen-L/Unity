using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DamageUI : MonoBehaviour 
{
	public TextMeshProUGUI damageText;
	public PlayerController player;

	void Update()
	{
		damageText.text = player.damagePercent + "%";
	}
}
