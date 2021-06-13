using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float dodgeMultiplier = 2f;
    public float dodgeTimer = 0.25f; 
    public float dodgeCooldown = 2f;

    public Berzerker berzerker = null;
    public LineRenderer lineRenderer = null;
    public float maxDistance = 5f;

    public float timeToMaxSpeed = 1f;
    public float currentSpeed = 0f;
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

    private Vector3 lastIntendedMotion;

    // Start is called before the first frame update
    void Start()
    {
        characterSprite = GetComponent<SpriteRenderer>();
        characterSprite.color = Color.white;

        lineRenderer.sortingLayerName = "Characters";
        lineRenderer.sortingOrder = 3;
    }

    // Update is called once per frame

    void Update()
    {
        GetPlayerInput();
        DetermineDirection();
        DrawLine();
        SetPlayerState();
        SetPlayerEffects();
    }
    void DetermineDirection()
    {
        // TODO: normalize them all.
        bool isMoving = false;

        Vector3 movementDirection = new Vector3(0,0,0);
        if (isPressedUp){
            movementDirection = movementDirection + new Vector3(0, 1, 0);
            isPressedUp = false;
            isMoving = true;
        }

        if (isPressedDown){
            movementDirection = movementDirection + new Vector3(0, -1, 0);
            isPressedDown = false;
            isMoving = true;
        }

        if (isPressedLeft){
            movementDirection = movementDirection + new Vector3(-1 , 0, 0);
            isPressedLeft = false;
            isMoving = true;
        }

        if (isPressedRight){
            movementDirection = movementDirection + new Vector3(1, 0, 0);
            isPressedRight = false;
            isMoving = true;
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
                //Debug.Log("Dodge on cooldown ! " + dodgeCooldownTimer.ToString() + "s left.");
            }
            isPressedDodge = false;
            isMoving = true;
        }

        // handleMovement(Vector3.Normalize(movementDirection));
        handleAcceleratedMovement(Vector3.Normalize(movementDirection), isMoving);
    }

    void DrawLine(){
        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, berzerker.transform.position);
    }

    void handleAcceleratedMovement(Vector3 intendedMovement, bool accelerate){
        float target = 0f;
        if (accelerate){
            target = movementSpeed;
            lastIntendedMotion = intendedMovement;
        } else {
            intendedMovement = lastIntendedMotion;
        }

        float changeRate = 1 / timeToMaxSpeed * Time.deltaTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, target, changeRate);
<<<<<<< HEAD
        //Debug.Log(changeRate + ", " + currentSpeed);

        intendedMovement = Vector3.Scale(intendedMovement, new Vector3(currentSpeed, currentSpeed, 0));
        //Debug.Log(intendedMovement.magnitude);
=======

        intendedMovement = Vector3.Scale(intendedMovement, new Vector3(currentSpeed, currentSpeed, 0));
>>>>>>> c7989da212116df4af838893882bb7e46f683f55

        if(isDodging)
        {
            intendedMovement = Vector3.Scale (intendedMovement, new Vector3(dodgeMultiplier, dodgeMultiplier, 0));
        }

        Vector3 newPosition = transform.position + intendedMovement;
        float distance = (newPosition - berzerker.transform.position).magnitude;

        if (distance < maxDistance){
            transform.position = newPosition;
        }
    }
    void handleMovement(Vector3 intendedMovement){

                
        
        Vector3 newPosition = transform.position + intendedMovement;
        float distance = (newPosition - berzerker.transform.position).magnitude;

        if (distance < maxDistance){
            transform.position = newPosition;
        }
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
        if(!isAlive){
            characterSprite.color = Color.red;
            return;
        }
        
        if (isImmune)
        {
            characterSprite.color = Color.cyan;
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else
        {
            characterSprite.color = Color.white;
            GetComponent<CapsuleCollider2D>().enabled = true;
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

    public float GetDodgeCooldownTimer()
    {
        return dodgeCooldownTimer;
    }

}
