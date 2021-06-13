using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player playerCharacter = null;

    public GameOverScreen gameOverScript;

    public GameWonScreen gameWonScript;

    public GamePauseScreen gamePauseScript;

    public HUD hud;

    public float startCountdown = 4;

    private Player player;
    private Berzerker berzerker;
    private Character[] ennemies;
    private bool gamePaused = true;



    

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        if(SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "testScene")
        {
            ennemies = FindObjectsOfType<SwordEnemy>();
            player = FindObjectOfType<Player>();
            berzerker = FindObjectOfType<Berzerker>();


            for (int i = 0; i < ennemies.Length; i++)
            {
                ennemies[i].GetComponent<SwordEnemy>().enabled = false;
            }
            player.GetComponent<Player>().enabled = false;
            berzerker.GetComponent<Berzerker>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePauseScript != null)
        {    
        if (Input.GetKeyDown("escape"))
        {
            if(gamePauseScript.GetPauseState())
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        }

        if(SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "testScene")
        {
            if (startCountdown > 0)
            {
                startCountdown -= Time.deltaTime;
            }
            else
            {
                if (gamePaused)
                {
                    for (int i = 0; i < ennemies.Length; i++)
                    {
                        ennemies[i].GetComponent<SwordEnemy>().enabled = true;
                    }
                    player.GetComponent<Player>().enabled = true;
                    berzerker.GetComponent<Berzerker>().enabled = true;
                    gamePaused = false;
                }
            }


            if(playerCharacter != null)
            {
                hud.Refresh(playerCharacter.health, playerCharacter.GetDodgeCooldownTimer(), startCountdown);
                GameOver(playerCharacter.isAlive);
                GameWon();
            }
        }
    }

    public void GameOver(bool isAlive)
    {
        if (!isAlive)
        {
            gameOverScript.Setup();
            hud.Disable();
            playerCharacter.GetComponent<Player>().enabled = false ;
            for (int i = 0; i < ennemies.Length; i++)
            {
                ennemies[i].GetComponent<SwordEnemy>().enabled = false;
            }
            berzerker.GetComponent<Berzerker>().enabled = false;
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
        SceneManager.LoadScene("MainMenu");
    }

    public void GameWon()
    {
        bool ennemiesPresent = false;
        for (int i = 0; i < ennemies.Length; i++)
        {
            ennemiesPresent = ennemiesPresent | ennemies[i].isAlive;
        }

        if (!ennemiesPresent)
        {
            gameWonScript.Setup();
            player.GetComponent<Player>().enabled = false;
            berzerker.GetComponent<Berzerker>().enabled = false;
        }
    }

    public void ResumeGame()
    {
        gamePauseScript.SetInactivePauseScreen();
        SetScripts(true);
    }

    public void PauseGame()
    {
        gamePauseScript.SetActivePauseScreen();
        SetScripts(false);
    }

    private void SetScripts(bool state)
    {
        Debug.Log("SetScripts reached !" + state.ToString());
        for (int i = 0; i < ennemies.Length; i++)
        {
        ennemies[i].GetComponent<SwordEnemy>().enabled = state;
        }
        player.GetComponent<Player>().enabled = state;
        berzerker.GetComponent<Berzerker>().enabled = state;
    }
}
