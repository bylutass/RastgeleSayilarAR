using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SayilariPuanla : MonoBehaviour
{
    public TMP_Text[] sayilarTexts; // Sayılar için TMP referansları
    public TMP_Text[] puanlarTexts; // Puanlar için TMP referansları

    void Start()
    {
        // Puan TMP'lerini başlangıçta sıfırla
        for (int i = 0; i < puanlarTexts.Length; i++)
        {
            puanlarTexts[i].text = "Puan:";
        }
    }

    public void Puanla()
    {
        for (int i = 0; i < sayilarTexts.Length; i++)
        {
            string girilenSayi = sayilarTexts[i].text.Trim(); // TMP'den metni al

            // Eğer sayı atanmışsa ve geçerli bir sayıysa
            if (!string.IsNullOrEmpty(girilenSayi) && !girilenSayi.StartsWith("SAYI") && IsNumeric(girilenSayi))
            {
                int puan = HesaplaRastgelelikPuani(girilenSayi);
                puanlarTexts[i].text = "Puan: " + puan;
            }
            else
            {
                // Eğer sayı atanmadıysa
                puanlarTexts[i].text = "Puan:";
            }
        }
    }

    public void ResetPuanlar()
    {
        // Puan TMP'lerini sıfırla
        for (int i = 0; i < puanlarTexts.Length; i++)
        {
            puanlarTexts[i].text = "Puan:";
        }
    }

    private int HesaplaRastgelelikPuani(string sayi)
    {
        HashSet<char> benzersizRakamlar = new HashSet<char>(sayi);
        int tekrarSayisi = sayi.Length - benzersizRakamlar.Count;

        int puan = 10; // Başlangıç puanı

        // Tekrar eden rakamlar için ceza
        puan -= tekrarSayisi;

        // Sıralı düzen kontrolü
        if (IsSirali(sayi))
        {
            puan -= 3; // Sıralıysa ceza uygula
        }

        // Çok tekrar eden rakamlar için ek ceza
        if (tekrarSayisi > 2)
        {
            puan -= tekrarSayisi - 2; // Fazladan tekrar eden rakamlar için ek ceza
        }

        // Benzersiz rakam sayısı azsa ek ceza
        if (benzersizRakamlar.Count < 3)
        {
            puan -= 2; // Benzersizlik düşükse ceza uygula
        }

        // Puanı 0 ile 10 arasında sınırla
        return Mathf.Clamp(puan, 0, 10);
    }

    private bool IsSirali(string sayi)
    {
        bool artan = true;
        bool azalan = true;

        for (int i = 1; i < sayi.Length; i++)
        {
            if (sayi[i] != sayi[i - 1] + 1)
                artan = false;
            if (sayi[i] != sayi[i - 1] - 1)
                azalan = false;
        }

        return artan || azalan;
    }

    private bool IsNumeric(string text)
    {
        // Girilen metnin tamamen rakamlardan oluşup oluşmadığını kontrol eder
        foreach (char c in text)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }
}
