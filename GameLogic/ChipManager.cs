using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChipManager : MonoBehaviour
{
    public TMP_Text chip10kText, chip25kText, chip50kText, chip100kText;
    public Image chip10k, chip25k, chip50k, chip100k;
    public TMP_Text currencyText;

    public void UpdateChips(int currency)
    {
        int remaining = currency;

        int num100k = remaining / 100000;
        remaining %= 100000;
        int num50k = remaining / 50000;
        remaining %= 50000;
        int num25k = remaining / 25000;
        remaining %= 25000;
        int num10k = remaining / 10000;

        chip100kText.text = "x" + num100k;
        chip50kText.text = "x" + num50k;
        chip25kText.text = "x" + num25k;
        chip10kText.text = "x" + num10k;

        chip100k.gameObject.SetActive(num100k > 0);
        chip50k.gameObject.SetActive(num50k > 0);
        chip25k.gameObject.SetActive(num25k > 0);
        chip10k.gameObject.SetActive(num10k > 0);

        currencyText.text = "Money: $" + currency;
    }
}
