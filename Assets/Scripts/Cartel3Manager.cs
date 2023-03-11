using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cartel3Manager : MonoBehaviour
{
    public Text no1Text;
    public Text no2Text;
    public Text no3Text;

    public GameObject cartel3Panel;
    public GameObject wrongAnswerPanel;
    public GameObject rightAnswerPanel;

    private static int[] rightAnswer = { 1, 5, 3 };
    private int[] insertCode;
    private int codeCounter;
    private bool isCorrect;

    private bool isSubmitted;

    private GameManager cartelCounting;

    void Start()
    {
        insertCode = new int[3];
        codeCounter = 0;
        isCorrect = true;
        isSubmitted = false;

        cartelCounting = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    public void ButtonPressed()
    {
        if (codeCounter < 3)
        {
            string buttonName = EventSystem.current.currentSelectedGameObject.name;
        
            if (buttonName == "Button1")
            {
                insertCode[codeCounter] = 1;
            }
            else if (buttonName == "Button2")
            {
                insertCode[codeCounter] = 2;
            }
            else if (buttonName == "Button3")
            {
                insertCode[codeCounter] = 3;
            }
            else if (buttonName == "Button4")
            {
                insertCode[codeCounter] = 4;
            }
            else if (buttonName == "Button5")
            {
                insertCode[codeCounter] = 5;
            }
            else if (buttonName == "Button6")
            {
                insertCode[codeCounter] = 6;
            }                                   

            codeCounter++;

            TextUpdate();
        }
    }

    // Updates the code on the screen
    private void TextUpdate()
    {
        no1Text.text = insertCode[0].ToString();
        no2Text.text = insertCode[1].ToString();
        no3Text.text = insertCode[2].ToString();
    }

    // "Cancels" the code on the UI in order to have it at "0000" everytime it has to be inserted again
    public void CancelCode()
    {
        no1Text.text = ".";
        no2Text.text = ".";
        no3Text.text = ".";
        
        codeCounter = 0;

        insertCode[0] = 0;
        insertCode[1] = 0;
        insertCode[2] = 0;
    }

    public void SubmitCode()
    {
        Debug.Log("right answer: " + rightAnswer[0] + " " + rightAnswer[1] + " " + rightAnswer[2]);
        Debug.Log("inserted answer: " + insertCode[0] + " " + insertCode[1] + " " + insertCode[2]);
        
        for (int i = 0; i < 3; i++)
        {
            if (insertCode[i] != rightAnswer[i])
            {
                isCorrect = false;
                break;
            }
            else 
            {
                isCorrect = true;
            }
        }

        Debug.Log("is correct: " + isCorrect);

        if (isCorrect)
        {
            Debug.Log("RIGHT CODE!");
            rightAnswerPanel.SetActive(true);
            isSubmitted = true;
            CancelCode();

            cartelCounting.SetCartelsGotRightCounter();
        }
        else
        {
            Debug.Log("WRONG CODE!");
            wrongAnswerPanel.SetActive(true);
            CancelCode();
        }
    }

    public void BackToGameButton()
    {
        CancelCode();
        cartel3Panel.SetActive(false);

        if (isSubmitted)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<SpriteRenderer>());
        }
    }

    public void BackToPanelButton()
    {
        CancelCode();
        wrongAnswerPanel.SetActive(false);
    }
}