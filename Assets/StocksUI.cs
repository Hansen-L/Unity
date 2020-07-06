using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using System.Collections;

public class StocksUI : MonoBehaviour
{
	public TextMeshProUGUI stockText;
	public PlayerController player;


	void Update()
	{
		stockText.text = player.stocks.ToString();
	}
}
