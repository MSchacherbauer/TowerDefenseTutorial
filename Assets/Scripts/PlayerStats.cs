using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public int startLives = 20;
    public static int Lives;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
}