using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseScreen : MonoBehaviour
{
    public void SetActivePauseScreen()
    {
        gameObject.SetActive(true);
    }

    public void SetInactivePauseScreen()
    {
        gameObject.SetActive(false);
    }
    public bool GetState()
    {
        return gameObject.activeSelf;
    }
}
