using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public bool GetState()
    {
        return gameObject.activeSelf;
    }
}
