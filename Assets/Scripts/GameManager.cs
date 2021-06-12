using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player playerCharacter = null;

    public GameOverScreen gameOverScript;

    public HUD hud;



    

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

        if(playerCharacter != null)
        {
            hud.Refresh(playerCharacter.health, playerCharacter.GetDodgeCooldownTimer());
        }
    }

    public void GameOver(bool isAlive)
    {
        if (!isAlive)
        {
            gameOverScript.Setup();
            hud.Disable();
            playerCharacter.GetComponent<Player>().enabled = false ;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Level00");
    }
}
