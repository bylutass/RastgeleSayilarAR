using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TicketGame : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text[] ticketTMPs; 
    public GameObject[] ticketCubes; 
    public TMP_Text resultText; 
    public GameObject infoCube; 
    public TMP_Text infoText; 
    public Button restartButton;

    private int[] enteredNumbers = new int[5];
    private int[,] ticketNumbers = new int[5, 5]; 
    private int bestTicketIndex = -1;

    private void Start()
    {
        InitializeTickets();

        inputField.onSelect.AddListener((_) => MoveCursorToEnd());
        inputField.characterValidation = TMP_InputField.CharacterValidation.Integer;
        inputField.onValueChanged.AddListener(ValidateInput);

        infoCube.SetActive(false);
        restartButton.gameObject.SetActive(false);
        resultText.text = ""; 

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
            CalculateScores();
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

        infoCube.SetActive(true);
        infoText.text = "En yüksek skorlu bilete dokunun.";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                for (int i = 0; i < ticketCubes.Length; i++)
                {
                    if (hit.transform.gameObject == ticketCubes[i])
                    {
                        if (i == bestTicketIndex)
                        {
                            ticketCubes[i].GetComponent<Renderer>().material.color = Color.green;
                            infoText.text = $"Bildiniz. Skorlar: {string.Join(", ", CalculateAllScores())}";
                            restartButton.gameObject.SetActive(true);
                        }
                        else
                        {
                            ticketCubes[i].GetComponent<Renderer>().material.color = Color.red;
                            infoText.text = "Bilemediniz tekrar deneyin.";
                        }
                    }
                }
            }
        }
    }

    private int[] CalculateAllScores()
    {
        int[] scores = new int[5];
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
        }
        return scores;
    }

    private void RestartGame()
    {
        inputField.text = "";
        inputField.caretPosition = 0;
        inputField.DeactivateInputField();

        InitializeTickets();
        resultText.text = ""; 
        restartButton.gameObject.SetActive(false);
        infoCube.SetActive(false);
    }

    private void MoveCursorToEnd()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            inputField.caretPosition = inputField.text.Length; 
        }
    }
}
