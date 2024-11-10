using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HighScoreButton : MonoBehaviour
{
    public GameObject highScorePanel;
    public void OpenHighScorePanel()
    {
        highScorePanel.SetActive(true);
    }

    public void CloseHighScorePanel()
    {
        highScorePanel.SetActive(false);
    }

}
