using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBaloon;
    public TextMesh playerBaloonText;

    private int loopCounter;
    private int totalLoopCounter;

    public GameObject pausePanel;
    public GameObject exitPanel;
    public GameObject finalPanel;
    public Text totalLoopCounterText;

    // Timer handler
    private float timeRemaining = 59; 
    private bool timerIsRunning = false;
    public Text timerText;

    // Cartel panels
    public GameObject cartelHomePanel;
    public GameObject cartelOnePanel;
    public GameObject cartelTwoPanel;
    public GameObject cartelThreePanel;

    private FencesManager fencesManager;

    // Witch starting message to the player
    public GameObject witchBaloon;
    public TextMesh witchBaloonText;
    public GameObject witchBaloonButton;
    private bool witchIsTalking;
    private bool witchIsTyping;

    private static string[] startingMessage =
    {
        "Ah ah ah, \nso you are here to \ndestroy my spell...",
        "Well, \nit will be \nreally difficult!",
        "You will never \nbe able to solve \nmy enigmas.",
        "If I'm not here, \nbad news for you! \nSome areas will \nbe blocked."
    };
    private int startingMessageCounter;
    private static string witchMessage = "Ah, you found me!\nAlright, map is all \nclear now... puff!";

    private static string[] witchQuotes =
    {
        "Oh, here already?",
        "Here we go again.",
        "Welcome back.",
        "Is it really so hard? \nAh ah ah"
    };

    private static string playerQuote = "I should find \nthe witch.";

    private bool witchIsToBeFound;

    private int cartelsGotRightCounter;
    
    void Start()
    {
        loopCounter = 0;
        totalLoopCounter = 0;
        cartelsGotRightCounter = 0;

        fencesManager = GameObject.FindObjectOfType(typeof(FencesManager)) as FencesManager;

        witchIsTalking = true;
        witchIsToBeFound = false;
        witchIsTyping = false;

        player.GetComponent<PlayerManager>().enabled = false;
        
        startingMessageCounter = 1;
        StartCoroutine(DisplayFirstMessageStartingText());
    }

    // Displays the witch's first message ever
    private IEnumerator DisplayFirstMessageStartingText()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        witchBaloon.SetActive(true);
        witchIsTyping = true;
        StartCoroutine(TypeText(startingMessage[0]));
    }

    // Displays the rest of the message
    private void DisplayStartingText()
    {
        if (startingMessageCounter < 4)
        {
            witchBaloonText.text = "";
            witchIsTyping = true;
            StartCoroutine(TypeText(startingMessage[startingMessageCounter]));
            startingMessageCounter++;
        }
        else
        {
            witchBaloonButton.SetActive(true);
            witchBaloon.SetActive(false);
            witchIsTyping = false;
            witchIsTalking = false;
            player.GetComponent<PlayerManager>().enabled = true;
            timerIsRunning = true;
        }
    }

    public void DisplayFoundWitchMessage()
    {
        timerIsRunning = false;
        witchBaloon.SetActive(true);
        witchBaloonText.text = "";
        StartCoroutine(TypeText(witchMessage));
        
        StartCoroutine(WaitSomeSecsAndTakeWitchBack());
    }

    private IEnumerator WaitSomeSecsAndTakeWitchBack()
    {
        yield return new WaitForSeconds(7.5f);

        witchBaloon.SetActive(false);
        witchIsToBeFound = false;
        timerIsRunning = true;
        fencesManager.GetWitchToStartingPosition();
    }

    // Types the witch's message text
    private IEnumerator TypeText(string message)
    {
        foreach (char letter in message.ToCharArray())
        {
            witchBaloonText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        witchIsTyping = false;
        witchBaloonButton.SetActive(true);
    }

    void Update()
    {
        if (timerIsRunning && !witchIsTalking)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                totalLoopCounter++;
                loopCounter++;
                if (loopCounter == 5) 
                {
                    fencesManager.SetNewFenceArea();
                    loopCounter = 0;
                    witchIsToBeFound = true;
                    StartCoroutine(DisplayPlayerMessage());
                }

                TakePlayerToStartPosition();
                StartLoopAgain();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !witchIsTyping)  
        {
            witchBaloonButton.SetActive(false);
            DisplayStartingText();
        }
    }

    private IEnumerator DisplayPlayerMessage()
    {
        playerBaloon.SetActive(true);
        playerBaloonText.text = "";
        
        foreach (char letter in playerQuote.ToCharArray())
        {
            playerBaloonText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSecondsRealtime(2f);
        playerBaloon.SetActive(false);
    }

    public bool GetWitchState()
    {
        return witchIsToBeFound;
    }

    // Takes the player to its starting position after the time ends
    private void TakePlayerToStartPosition()
    {
        player.transform.position = new Vector3(-52, -8, -1);

        witchBaloon.SetActive(true);
        witchBaloonText.text = "";
        
        int rnd = Random.Range(0, 4);
        StartCoroutine(TypeText(witchQuotes[rnd]));
        StartCoroutine(DisplayRandomWitchMessage());
    }

    private IEnumerator DisplayRandomWitchMessage()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        witchBaloon.SetActive(false);
    }

    // Starts the loop again
    private void StartLoopAgain()
    {
        cartelOnePanel.SetActive(false);
        cartelTwoPanel.SetActive(false);
        cartelThreePanel.SetActive(false);

        timeRemaining = 59;
        timerIsRunning = true;
    }

    // Displays time on screen
    private void DisplayTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (seconds > 9)
        {
            timerText.text = "00:" + seconds;
        }
        else 
        {
            timerText.text = "00:0" + seconds;
        }     
    }

    // Pause game
    public void PauseGame()
    {
        // + sarebbe meglio fermare il movimento del player
        timerIsRunning = false;
        pausePanel.SetActive(true);
    }

    // Goes back to game after pause
    public void GoBackToGame()
    {
        timerIsRunning = true;

        exitPanel.SetActive(false);
        pausePanel.SetActive(false);
        cartelOnePanel.SetActive(false);
        cartelTwoPanel.SetActive(false);
        cartelThreePanel.SetActive(false);
    }

    public void ActivateCartelPanel(string cartelName)
    {
        if (cartelName == "CartelHome")
        {
            cartelHomePanel.SetActive(true);
        }
        else if (cartelName == "Cartel1")
        {
            cartelOnePanel.SetActive(true);
        }
        else if (cartelName == "Cartel2")
        {
            cartelTwoPanel.SetActive(true);
        }
        else if (cartelName == "Cartel3")
        {
            cartelThreePanel.SetActive(true);
        }
    }

    public void SetCartelsGotRightCounter()
    {
        cartelsGotRightCounter++;
        Debug.Log("cartels got right: " + cartelsGotRightCounter);
        
        if (cartelsGotRightCounter == 3)
        {
            timerIsRunning = false;
            finalPanel.SetActive(true);
            totalLoopCounterText.text = totalLoopCounter.ToString() + " loops.";
        }
    }

    // Activates exit panel
    public void ActivateExitPanel()
    {
        timerIsRunning = false;
        exitPanel.SetActive(true);
    }

    // Goes back to Main Menu scene
    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}