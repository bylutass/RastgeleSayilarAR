using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldControl : MonoBehaviour
{
    public TMP_InputField inputField; 
    public TextMeshProUGUI placeholderText; 

    void Start()
    {
        inputField.onSelect.AddListener(OnInputFieldSelected);
        inputField.onDeselect.AddListener(OnInputFieldDeselected);
    }

    void OnInputFieldSelected(string text)
    {
        placeholderText.gameObject.SetActive(false);
    }

    void OnInputFieldDeselected(string text)
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            placeholderText.gameObject.SetActive(true);
        }
    }
}

