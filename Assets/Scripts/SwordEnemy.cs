using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Character
{

    public Berzerker berzerker = null;
    public Player player = null;

    public float minimumDistance = 0.7f;
    // public float deathTimer = 2f;

    public bool isAttacking = false;
    public float windUpTime = 0.5f;
    private float timeTilAttack;

    // Start is called before the first frame update
    void Start()
    {
        timeTilAttack = windUpTime;
    }

    // Update is called once per frame
    void Update()
    {
        handleState();
        // handleDeath();
        if (isAlive){
            handleMovement();
            //handleAttack();
        }
    }

    void handleState(){
        if (!isAlive) {
            GetComponent<SpriteRenderer>().color = Color.red;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<CircleCollider2D>());
        }
    }
    // void handleDeath(){
    //     if (!isAlive){
    //         deathTimer -= Time.deltaTime;
    //         // if (deathTimer <= 0) {
    //         //     Destroy(transform.gameObject);
    //         // }
    //     }
    // }

    void handleMovement(){
            Vector3 distToBerzerker = berzerker.transform.position - transform.position;
            Vector3 distToPlayer = player.transform.position - transform.position;

            Vector3 targetPosition = new Vector3(0, 0, 0);
            Vector3 direction = new Vector3(0, 0, 0);;
            if (distToBerzerker.magnitude > distToPlayer.magnitude){
                targetPosition = player.transform.position;
                direction = distToPlayer;
            } else {
                targetPosition = berzerker.transform.position;
                direction = distToBerzerker;
            }

            if (targetPosition.x < transform.position.x){
                transform.eulerAngles = new Vector3(0, 0, 0); 
            } else {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            
            if (direction.magnitude > minimumDistance) {
                transform.position = transform.position + Vector3.Scale(Vector3.Normalize(direction),new Vector3(movementSpeed, movementSpeed, 0) );
            }
        }
    
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log(other);
    }
}
