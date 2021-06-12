using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float dodgeSpeed = 0.6f;
    public float dodgeMultiplier = 2f;
    public float dodgeTimer = 0.25f; 

    public float dodgeCooldown = 2f;

    public Berzerker berzerker = null;
    public LineRenderer lineRenderer = null;
    
    public float maxDistance = 5f;

    //Private params

    private bool isPressedUp = false;
    private bool isPressedDown = false;
    private bool isPressedLeft = false;
    private bool isPressedRight = false;
    private bool isPressedDodge = false;

    private float dodgeTimerCounter = 0;
    private float dodgeCooldownTimer = 0;
    private bool isDodging = false;
    private bool isDodgeAvailable = true;
    private bool isImmune = false;
    private SpriteRenderer characterSprite = null;


    // Start is called before the first frame update
    void Start()
    {
        characterSprite = GetComponent<SpriteRenderer>();
        characterSprite.color = Color.white;
    }

    // Update is called once per frame

    void Update()
    {
        FindObjectOfType<GameManager>().GameOver(isAlive);
        GetPlayerInput();
        SetPlayerState();
        SetPlayerEffects();
    }
    void FixedUpdate()
    {

        Vector3 movementDirection = new Vector3(0,0,0);
        if (isPressedUp){
            movementDirection = movementDirection + new Vector3(0, movementSpeed, 0);
            isPressedUp = false;
        }

        if (isPressedDown){
            movementDirection = movementDirection + new Vector3(0, -1 * movementSpeed, 0);
            isPressedDown = false;
        }

        if (isPressedLeft){
            movementDirection = movementDirection + new Vector3(-1 * movementSpeed, 0, 0);
            isPressedLeft = false;
        }

        if (isPressedRight){
            movementDirection = movementDirection + new Vector3(movementSpeed, 0, 0);
            isPressedRight = false;
        }

        if(isPressedDodge)
        {
            if (dodgeCooldownTimer <= 0)
            {
                isDodging = true;
                dodgeTimerCounter = dodgeTimer;
                dodgeCooldownTimer = dodgeCooldown;
            }
            {
                Debug.Log("Dodge on cooldown ! " + dodgeCooldownTimer.ToString() + "s left.");
            }
            isPressedDodge = false;
        }

        handleMovement(movementDirection);

        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, berzerker.transform.position);

    }

    void handleMovement(Vector3 intendedMovement){
        
        

        if(isDodging)
        {
            intendedMovement = Vector3.Scale (intendedMovement, new Vector3(dodgeMultiplier, dodgeMultiplier, 0));
        }
                
        
        Vector3 newPosition = transform.position + intendedMovement;
        float distance = (newPosition - berzerker.transform.position).magnitude;

        if (distance < maxDistance){
            transform.position = newPosition;
        }

        // TO-DO
        // Else, get yanked

        // Idea: Movement speed is a function of distance?

    }

    void GetPlayerInput()
    {
        if (Input.GetKey("w") || Input.GetKey("up")){
            isPressedUp = true;
        }

        if (Input.GetKey("s") || Input.GetKey("down")){
            isPressedDown = true;
        }

        if (Input.GetKey("a") || Input.GetKey("left")){
            isPressedLeft = true;
        }

        if (Input.GetKey("d") || Input.GetKey("right")){
            isPressedRight = true;
        }

        if (Input.GetKeyDown("space"))
        {
            isPressedDodge = true;
        }
    }

    void SetPlayerState()
    {
        //Dodge state
        if (dodgeTimerCounter > 0)
        {
            dodgeTimerCounter = dodgeTimerCounter - Time.deltaTime;
        }
        else
        {
            isDodging = false;
        }

        //Dodge cooldown
        if (dodgeCooldownTimer >0)
        {
            dodgeCooldownTimer -= Time.deltaTime;
        }

        //Immune state
        isImmune = isDodging;
    }

    void SetPlayerEffects()
    {
        if (isImmune)
        {
            characterSprite.color = Color.cyan;
        }
        else
        {
            characterSprite.color = Color.white;
        }
    }

    public void DisableControl()
    {
        isPressedDodge = false;
        isPressedUp = false;
        isPressedDown = false;
        isPressedLeft = false;
        isPressedRight = false;
    }


}
