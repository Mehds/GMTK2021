using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player playerCharacter = null;

    public GameOverScreen gameOverScript;

    public HUD hud;

    public float startCountdown = 3;

    private Player player;
    private Berzerker berzerker;
    private Character[] ennemies;
    private bool gamePaused = true;


    

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "Level00")
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

            //Debug.Log(berzerker.GetComponent<Berzerker>().ToString());

        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (playerCharacter.health <= 0)
        // {
        //     Debug.Log("Git Gud! HP:" + playerCharacter.health.ToString());
        // } 

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
            hud.Refresh(playerCharacter.health, playerCharacter.GetDodgeCooldownTimer());
            GameOver(playerCharacter.isAlive);
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
