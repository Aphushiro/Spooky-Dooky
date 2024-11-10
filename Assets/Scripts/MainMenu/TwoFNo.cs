using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TwoFNo : MonoBehaviour
{
    public Canvas twoFactureExit;
    public void CloseTwoFactureExit()
    {
        twoFactureExit.gameObject.SetActive(false);
    }

}
