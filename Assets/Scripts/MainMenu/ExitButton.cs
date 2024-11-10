using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public Canvas twoFactureExit;

    public void OpenTwoFactureExit()
    {
        twoFactureExit.gameObject.SetActive(true);
    }
}
