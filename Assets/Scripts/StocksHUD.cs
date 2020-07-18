using UnityEngine;

public class StocksHUD : MonoBehaviour 
{
    public PlayerController player;
    public GameObject[] stocksArray;
    public GameObject stockEffect;

    private Vector3[] stockPositions;
    private int curStocks;

    void Start()
    {
        curStocks = player.stocks;

        stockPositions = new Vector3[stocksArray.Length];
        for (int i = 0; i < stocksArray.Length; i++)
        {
            stockPositions[i] = Camera.main.ScreenToWorldPoint(stocksArray[i].transform.position);
        }
    }

    void Update() 
    {
        if (player.stocks == 2 && curStocks != 2)
        {
            curStocks = 2;
            LoseStock(0);
        }
        else if (player.stocks == 1 && curStocks != 1)
        {
            curStocks = 1;
            LoseStock(1);
        }
        else if (player.stocks == 0 && curStocks != 0)
        {
            curStocks = 0;
            LoseStock(2);
        }
    }

    private void LoseStock(int i)
    {
        Vector3 effectPosition = new Vector3(stockPositions[i].x, stockPositions[i].y, 0);
        GameObject effect = Instantiate(stockEffect, effectPosition, Quaternion.identity);
        Destroy(stocksArray[i]);
    }
}
