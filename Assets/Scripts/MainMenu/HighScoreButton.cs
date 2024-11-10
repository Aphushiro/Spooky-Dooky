using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreButton : MonoBehaviour
{
    bool TutorialOpen = false;
    public GameObject highScorePanel;
    public TMP_Text tutorialButtonText;
    public void OpenHighScorePanel()
    {
        highScorePanel.SetActive(true);
    }

    public void CloseHighScorePanel()
    {
        Debug.Log("Close");
        highScorePanel.SetActive(false);
    }
    public void ToggleTutorial()
    {
        if (TutorialOpen)
        {
            TutorialOpen = false;
            highScorePanel.SetActive(false);
            tutorialButtonText.text = "Tutorial";
        }
        else
        {
            TutorialOpen = true;
            highScorePanel.SetActive(true);
            tutorialButtonText.text = "Close Tutorial";
        }
    }

}
