using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player playerCharacter = null;

    public GameOverScreen gameOverScript;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (playerCharacter.health <= 0)
        // {
        //     Debug.Log("Git Gud! HP:" + playerCharacter.health.ToString());
        // } 
        
    }

    public void GameOver(bool isAlive)
    {
        if (!isAlive)
        {
            gameOverScript.Setup();
            playerCharacter.GetComponent<Player>().enabled = false ;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
