using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene("Main");//Remeber to rename to base survival scene name
    }
}
