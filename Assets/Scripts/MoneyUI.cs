using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI wavecountdownText;

    private void Update()
    {
        wavecountdownText.text = "$"+PlayerStats.Money.ToString();
    }
}

