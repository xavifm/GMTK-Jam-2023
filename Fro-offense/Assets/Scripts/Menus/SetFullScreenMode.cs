using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFullScreenMode : MonoBehaviour
{
    public void ChangeFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
