using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TicketGame : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text[] ticketTMPs; 
    public GameObject[] ticketCubes; 
    public TMP_Text resultText; 
    public Button showResultsButton;
    public Button restartButton;

    private int[] enteredNumbers = new int[5];
    private int[,] ticketNumbers = new int[5, 5]; 

    private void Start()
    {
        InitializeTickets();

        inputField.onSelect.AddListener((_) => MoveCursorToEnd());
        inputField.characterValidation = TMP_InputField.CharacterValidation.Integer;
        inputField.onValueChanged.AddListener(ValidateInput);

        showResultsButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        resultText.text = ""; 

        showResultsButton.onClick.AddListener(CalculateScores);
        restartButton.onClick.AddListener(RestartGame);
    }

    private void InitializeTickets()
    {
        for (int i = 0; i < ticketTMPs.Length; i++)
        {
            string ticketText = "";
            for (int j = 0; j < 5; j++)
            {
                ticketNumbers[i, j] = Random.Range(1, 7);
                ticketText += ticketNumbers[i, j] + " ";
            }
            ticketTMPs[i].text = ticketText.Trim();
            ticketCubes[i].GetComponent<Renderer>().material.color = Color.white; 
        }
    }

    private void ValidateInput(string input)
    {
        if (input.Length > 5)
        {
            inputField.text = input.Substring(0, 5); 
            return;
        }

        foreach (char c in input)
        {
            if (!"123456".Contains(c))
            {
                inputField.text = input.Remove(input.IndexOf(c), 1); 
                return;
            }
        }

        if (input.Length == 5)
        {
            showResultsButton.gameObject.SetActive(true);
        }
    }

    private void ParseInput(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            enteredNumbers[i] = int.Parse(input[i].ToString());
        }
    }

    private void CalculateScores()
    {
        ParseInput(inputField.text);

        int[] scores = new int[5];
        int maxScore = 0;
        int bestTicketIndex = -1;

        for (int i = 0; i < ticketTMPs.Length; i++)
        {
            int score = 0;
            for (int j = 0; j < 5; j++)
            {
                if (ticketNumbers[i, j] == enteredNumbers[j])
                {
                    score++;
                }
            }

            scores[i] = score;

            if (score > maxScore)
            {
                maxScore = score;
                bestTicketIndex = i;
            }

            ticketCubes[i].GetComponent<Renderer>().material.color = Color.white; 
        }

        if (bestTicketIndex != -1)
        {
            ticketCubes[bestTicketIndex].GetComponent<Renderer>().material.color = Color.green;
        }

        resultText.text = $"Scores: {string.Join(", ", scores)}";

        showResultsButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        inputField.text = "";
        inputField.caretPosition = 0;
        inputField.DeactivateInputField();

        InitializeTickets();
        resultText.text = ""; 
        restartButton.gameObject.SetActive(false);
        showResultsButton.gameObject.SetActive(false);
    }

    private void MoveCursorToEnd()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            inputField.caretPosition = inputField.text.Length; 
        }
    }
}
