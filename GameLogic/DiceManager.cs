using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public Image die1Image;
    public Image die2Image;
    public Sprite[] diceFaces;

    public int RollDice()
    {
        int die1Value = Random.Range(0, 6);
        int die2Value = Random.Range(0, 6);
        die1Image.sprite = diceFaces[die1Value];
        die2Image.sprite = diceFaces[die2Value];
        return die1Value + die2Value + 2;
    }
}
