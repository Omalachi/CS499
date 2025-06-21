using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text winText, currentBetText;

    public void UpdateWinText(string message)
    {
        winText.text = message;
    }

    public void UpdateBetText(int bet)
    {
        currentBetText.text = "Current Bet: $" + bet;
    }
    public void UpdateBetDisplay(int betAmount)
    {
        currentBetText.text = "Current Bet: $" + betAmount;
    }

}
