using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SayilariGir : MonoBehaviour
{
    public TMP_InputField inputField; 
    public Button sayiGirButton; 
    public Button yenidenBaslaButton; 
    public Button geriButton; 
    public TMP_Text[] girilenSayilarTexts; 
    private string[] girilenSayilar = new string[10]; 
    private int sayiIndex = 0; 

    void Start()
    {
        sayiGirButton.onClick.AddListener(SayiGir);
        yenidenBaslaButton.onClick.AddListener(YenidenBasla);
        geriButton.onClick.AddListener(GeriGit);
        yenidenBaslaButton.gameObject.SetActive(false);
        inputField.contentType = TMP_InputField.ContentType.Custom;
        inputField.onValueChanged.AddListener(delegate { SadeceRakam(); });
    }

    void SayiGir()
    {
        if (sayiIndex < 10)
        {
            string inputText = inputField.text.Trim();

            if (!string.IsNullOrEmpty(inputText))
            {
                string displayedNumber = inputText.Length > 10 ? inputText.Substring(0, 10) : inputText;
                girilenSayilar[sayiIndex] = displayedNumber;
                girilenSayilarTexts[sayiIndex].text = displayedNumber;
                sayiIndex++;

                if (sayiIndex == 10)
                {
                    sayiGirButton.gameObject.SetActive(false); 
                    yenidenBaslaButton.gameObject.SetActive(true); 
                }

                inputField.text = "";
            }
        }
    }

    void YenidenBasla()
    {
        sayiIndex = 0;
        for (int i = 0; i < 10; i++)
        {
            girilenSayilar[i] = ""; 
            girilenSayilarTexts[i].text = "SAYI " + (i + 1); 
        }

        sayiGirButton.gameObject.SetActive(true); 
        yenidenBaslaButton.gameObject.SetActive(false); 
    }

    void GeriGit()
    {
        this.gameObject.SetActive(false); 
    }

    void SadeceRakam()
    {
        string filteredText = "";
        foreach (char c in inputField.text)
        {
            if (char.IsDigit(c))
            {
                filteredText += c;
            }
        }
        inputField.text = filteredText;
    }
}
