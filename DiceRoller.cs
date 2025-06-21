/*

using System.Collections;
using System.Collections.Generic;
using TMPro;  // For TMP_Text
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Image die1Image;  // Reference to the first dice image
    public Image die2Image;  // Reference to the second dice image
    public Button rollButton;  // Reference to the roll button
    public TMP_Text CurrencyText;  // Reference to the TMP_Text showing currency (use TMP_Text for TextMeshPro)

    public Sprite[] diceFaces; // Array to hold the dice face images
    public int currency = 100; // Starting currency

    // Chip-related variables
    public Image chip10k;  // Reference to the 10k chip image
    public Image chip25k;  // Reference to the 25k chip image
    public Image chip50k;  // Reference to the 50k chip image
    public Image chip100k; // Reference to the 100k chip image

    public TMP_Text chip10kText;  // Text to display the number of 10k chips
    public TMP_Text chip25kText;  // Text to display the number of 25k chips
    public TMP_Text chip50kText;  // Text to display the number of 50k chips
    public TMP_Text chip100kText; // Text to display the number of 100k chips

    // New variable to display the win/loss amount
    public TMP_Text winText;  // Text to display how much the player won or lost in the roll

    // Betting-related UI elements
    public TMP_InputField betAmountInputField;  // Reference to the Input Field for betting
    public TMP_Text currentBetText;  // Text to display the current bet amount

    public Button passLineButton; // Reference to the Pass Line button
    public Button dontPassLineButton; // Reference to the Don't Pass Line button


    // Start method
    void Start()
    {
        // Set up button click listener
        rollButton.onClick.AddListener(RollDice);

        // Display initial currency
        UpdateCurrencyDisplay();
        UpdateChipsDisplay();

        // Set winText to be empty initially
        winText.text = "";

        // Set the current bet text to be empty initially
        currentBetText.text = "Current Bet: $0";
    }

    // Roll dice method
    void RollDice()
    {
        // Roll two dice
        int die1Value = Random.Range(0, 6);  // Random number between 0 and 5
        int die2Value = Random.Range(0, 6);  // Random number between 0 and 5

        // Update the dice images to show the rolled faces
        die1Image.sprite = diceFaces[die1Value];
        die2Image.sprite = diceFaces[die2Value];

        // Calculate total dice roll (adding 1 because dice values are 1-6)
        int totalRoll = die1Value + die2Value + 2;

        // Amount won or lost based on the roll
        int winAmount = 0;

        // Adjust currency based on the roll
        if (totalRoll == 7) // Winning condition
        {
            winAmount = Random.Range(10, 50); // Random win between $10 and $50
            currency += winAmount; // Add winAmount to currency
            winText.text = "You won $" + winAmount; // Show the win amount
            Debug.Log("You won $" + winAmount + "!");
        }
        else // Losing condition
        {
            winAmount = -Random.Range(5, 20); // Random loss between $5 and $20
            currency += winAmount; // Subtract the loss from currency
            winText.text = "You lost $" + Mathf.Abs(winAmount); // Show the loss amount
            Debug.Log("You lost $" + Mathf.Abs(winAmount) + "!");
        }

        // Update the currency display
        UpdateCurrencyDisplay();
        UpdateChipsDisplay();

        // Check if the player is out of money
        if (currency <= 0)
        {
            Debug.Log("Game Over! You have no money left.");
            rollButton.interactable = false; // Disable the roll button
        }
    }

    // Update the currency display
    void UpdateCurrencyDisplay()
    {
        // Update the currency text to show the current amount
        CurrencyText.text = "Money: $" + currency;
    }

    // Update the chips display (chip denominations)
    void UpdateChipsDisplay()
    {
        int remainingCurrency = currency;

        // Calculate the number of each chip
        int num100k = remainingCurrency / 100000;
        remainingCurrency %= 100000;

        int num50k = remainingCurrency / 50000;
        remainingCurrency %= 50000;

        int num25k = remainingCurrency / 25000;
        remainingCurrency %= 25000;

        int num10k = remainingCurrency / 10000;

        // Update the chip count text
        chip100kText.text = "x" + num100k;
        chip50kText.text = "x" + num50k;
        chip25kText.text = "x" + num25k;
        chip10kText.text = "x" + num10k;

        // Optionally hide chips with 0 count
        chip100k.gameObject.SetActive(num100k > 0);
        chip50k.gameObject.SetActive(num50k > 0);
        chip25k.gameObject.SetActive(num25k > 0);
        chip10k.gameObject.SetActive(num10k > 0);
    }

    // Method to handle Pass Line bet placement
    public void OnPassLineButtonClicked()
    {
        // Get the bet amount from the input field
        int betAmount = int.Parse(betAmountInputField.text);

        // Ensure the player has enough currency for the bet
        if (currency >= betAmount)
        {
            PlacePassLineBet(betAmount);  // Call the PlacePassLineBet method
            UpdateCurrentBetText(betAmount);
        }
        else
        {
            Debug.Log("Not enough money for this bet!");
        }
    }

    // Method to handle Don't Pass Line bet placement
    public void OnDontPassLineButtonClicked()
    {
        // Get the bet amount from the input field
        int betAmount = int.Parse(betAmountInputField.text);

        // Ensure the player has enough currency for the bet
        if (currency >= betAmount)
        {
            PlaceDontPassBet(betAmount);  // Call the PlaceDontPassBet method
            UpdateCurrentBetText(betAmount);
        }
        else
        {
            Debug.Log("Not enough money for this bet!");
        }
    }

    // Method to update the displayed bet amount
    void UpdateCurrentBetText(int betAmount)
    {
        currentBetText.text = "Current Bet: $" + betAmount;
    }

    // Method to handle the Pass Line bet logic
    void PlacePassLineBet(int betAmount)
    {
        // Subtract the bet amount from the currency
        currency -= betAmount;

        // Implement Pass Line bet logic (for now, just win/loss based on the dice roll)
        int totalRoll = Random.Range(2, 12);  // Simulate the dice roll (adjust if needed)

        if (totalRoll == 7 || totalRoll == 11) // Win on Pass Line
        {
            currency += betAmount * 2;  // Double the bet amount on win
            Debug.Log("Pass Line Win! You won $" + betAmount * 2);
        }
        else if (totalRoll == 2 || totalRoll == 3 || totalRoll == 12) // Loss on Pass Line
        {
            Debug.Log("Pass Line Loss! You lost $" + betAmount);
        }
    }

    // Method to handle the Don't Pass Line bet logic
    void PlaceDontPassBet(int betAmount)
    {
        // Subtract the bet amount from the currency
        currency -= betAmount;

        // Implement Don't Pass Line bet logic (for now, just win/loss based on the dice roll)
        int totalRoll = Random.Range(2, 12);  // Simulate the dice roll (adjust if needed)

        if (totalRoll == 2 || totalRoll == 3) // Win on Don't Pass Line
        {
            currency += betAmount * 2;  // Double the bet amount on win
            Debug.Log("Don't Pass Line Win! You won $" + betAmount * 2);
        }
        else if (totalRoll == 7 || totalRoll == 11 || totalRoll == 12) // Loss on Don't Pass Line
        {
            Debug.Log("Don't Pass Line Loss! You lost $" + betAmount);
        }
    }
}
*/



using System.Collections;
using System.Collections.Generic;
using TMPro;  // For TMP_Text
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public Image die1Image;  // Reference to the first dice image
    public Image die2Image;  // Reference to the second dice image
    public Button rollButton;  // Reference to the roll button
    public TMP_Text CurrencyText;  // Reference to the TMP_Text showing currency (use TMP_Text for TextMeshPro)

    public Sprite[] diceFaces; // Array to hold the dice face images
    public int currency = 100; // Starting currency

    // Chip-related variables
    public Image chip10k;  // Reference to the 10k chip image
    public Image chip25k;  // Reference to the 25k chip image
    public Image chip50k;  // Reference to the 50k chip image
    public Image chip100k; // Reference to the 100k chip image

    public TMP_Text chip10kText;  // Text to display the number of 10k chips
    public TMP_Text chip25kText;  // Text to display the number of 25k chips
    public TMP_Text chip50kText;  // Text to display the number of 50k chips
    public TMP_Text chip100kText; // Text to display the number of 100k chips

    // New variable to display the win/loss amount
    public TMP_Text winText;  // Text to display how much the player won or lost in the roll

    // Betting-related UI elements
    public TMP_InputField betAmountInputField;  // Reference to the Input Field for betting
    public TMP_Text currentBetText;  // Text to display the current bet amount

    public Button passLineButton; // Reference to the Pass Line button
    public Button dontPassLineButton; // Reference to the Don't Pass Line button

    private int currentBet = 0; // Tracks the current bet amount

    void Start()
    {
        rollButton.onClick.AddListener(RollDice);

        // Initialize UI elements
        UpdateCurrencyDisplay();
        UpdateChipsDisplay();

        winText.text = "";
        currentBetText.text = "Current Bet: $0";

        // Connect button methods
        passLineButton.onClick.AddListener(OnPassLineButtonClicked);
        dontPassLineButton.onClick.AddListener(OnDontPassLineButtonClicked);
    }

    void RollDice()
    {
        if (currentBet <= 0)
        {
            Debug.Log("Please place a valid bet before rolling the dice.");
            winText.text = "Place a bet to play!";
            return;
        }

        // Roll two dice
        int die1Value = Random.Range(0, 6);
        int die2Value = Random.Range(0, 6);

        // Update the dice images
        die1Image.sprite = diceFaces[die1Value];
        die2Image.sprite = diceFaces[die2Value];

        int totalRoll = die1Value + die2Value + 2;
        string resultMessage = "";

        // Determine outcome based on current bet type (Pass Line or Don't Pass Line)
        if (totalRoll == 7 || totalRoll == 11)
        {
            // Win on Pass Line
            currency += currentBet;
            resultMessage = "You won $" + currentBet + "!";
        }
        else if (totalRoll == 2 || totalRoll == 3 || totalRoll == 12)
        {
            // Loss on Pass Line
            currency -= currentBet;
            resultMessage = "You lost $" + currentBet + "!";
        }
        else
        {
            // No immediate win/loss, placeholder for advanced logic
            resultMessage = "No win or loss. Roll again!";
        }

        winText.text = resultMessage;
        UpdateCurrencyDisplay();
        UpdateChipsDisplay();

        if (currency <= 0)
        {
            rollButton.interactable = false;
            Debug.Log("Game Over! Out of money.");
        }

        // Reset current bet after each roll
        currentBet = 0;
        currentBetText.text = "Current Bet: $0";
    }

    void UpdateCurrencyDisplay()
    {
        CurrencyText.text = "Money: $" + currency;
    }

    void UpdateChipsDisplay()
    {
        int remainingCurrency = currency;

        int num100k = remainingCurrency / 100000;
        remainingCurrency %= 100000;

        int num50k = remainingCurrency / 50000;
        remainingCurrency %= 50000;

        int num25k = remainingCurrency / 25000;
        remainingCurrency %= 25000;

        int num10k = remainingCurrency / 10000;

        chip100kText.text = "x" + num100k;
        chip50kText.text = "x" + num50k;
        chip25kText.text = "x" + num25k;
        chip10kText.text = "x" + num10k;

        chip100k.gameObject.SetActive(num100k > 0);
        chip50k.gameObject.SetActive(num50k > 0);
        chip25k.gameObject.SetActive(num25k > 0);
        chip10k.gameObject.SetActive(num10k > 0);
    }

    public void OnPassLineButtonClicked()
    {
        PlaceBet();
    }

    public void OnDontPassLineButtonClicked()
    {
        PlaceBet();
    }

    void PlaceBet()
    {
        if (int.TryParse(betAmountInputField.text, out int betAmount) && betAmount > 0)
        {
            if (currency >= betAmount)
            {
                currentBet = betAmount;
                currentBetText.text = "Current Bet: $" + currentBet;
                winText.text = ""; // Clear win text for new bet
            }
            else
            {
                Debug.Log("Not enough money for this bet!");
                winText.text = "Not enough money!";
            }
        }
        else
        {
            Debug.Log("Invalid bet amount entered.");
            winText.text = "Enter a valid bet amount!";
        }
    }
}
