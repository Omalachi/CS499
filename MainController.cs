using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private int currency = 100;      // Starting money
    private int currentBet = 0;      // Current bet placed by player

    public DataManager dataManager;  // Add reference to your DataManager

    // Called by MainController at game start
    public void Initialize()
    {
        // Initialization logic (expand later if needed)
    }

    // Place a bet if valid and player has enough money
    public bool PlaceBet(int amount)
    {
        if (amount > 0 && amount <= currency)
        {
            currentBet = amount;
            return true;
        }
        return false;
    }

    // Check if player is allowed to roll
    public bool CanRoll()
    {
        return currentBet > 0;
    }

    // Handle the result of the dice roll and log it
    public string ProcessRollResult(int die1, int die2)
    {
        int rollTotal = die1 + die2;
        string resultMessage = "";

        if (rollTotal == 7 || rollTotal == 11)
        {
            currency += currentBet;
            resultMessage = "You won $" + currentBet + "!";
        }
        else if (rollTotal == 2 || rollTotal == 3 || rollTotal == 12)
        {
            currency -= currentBet;
            resultMessage = "You lost $" + currentBet + "!";
        }
        else
        {
            resultMessage = "No win or loss. Roll again!";
        }

        // Log result to file/database
        if (dataManager != null)
        {
            dataManager.LogGameResult(die1, die2, rollTotal, resultMessage, currentBet);
        }

        currentBet = 0; // Reset the bet after roll
        return resultMessage;
    }

    // Return current money
    public int GetCurrency()
    {
        return currency;
    }

    // Return current bet
    public int GetCurrentBet()
    {
        return currentBet;
    }
}
