using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour
{
    public void ReturnButton()
    {
        PlayerStats.Instance.UiExitGame();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
