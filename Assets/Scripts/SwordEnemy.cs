using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : Character
{

    public Berzerker berzerker = null;
    public Player player = null;

    private GameObject targetingCircle = null;

    public float minimumDistance = 0.7f;

    public bool isAttacking = false;
    public float windUpTime = 0.5f;
    private float timeTilAttack;

    // Start is called before the first frame update
    void Start()
    {
        timeTilAttack = windUpTime;
        targetingCircle = transform.Find("TargetingCircle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        handleState();
        // handleDeath();
        if (isAlive && !isAttacking){
            handleMovement();
            //handleAttack();
        } else if (isAlive && isAttacking){
            handleAttack();
        }
    }

    void handleState(){
        if (!isAlive) {
            GetComponent<SpriteRenderer>().color = Color.red;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(targetingCircle);
            transform.gameObject.layer = 2;
        }
    }

    void pushBack(Transform t){
        Vector3 direction = Vector3.Scale( t.position - transform.position, new Vector3(-2, -2, -2));
        transform.position += direction;
    }
    void handleAttack(){
        if (timeTilAttack <= 0){
            damageTargets();
            // Get all gameobjects inside the target
            // Deal damage to all of them
            isAttacking = false;
            timeTilAttack = windUpTime;
            targetingCircle.GetComponent<SpriteRenderer>().enabled = false;
        } else {
            timeTilAttack -= Time.deltaTime;
        }

    }

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
        if (isAlive && other.transform.gameObject.layer == 7){
            isAttacking = true;
            if (targetingCircle != null){
                targetingCircle.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void damageTargets()
    {
        
        Collider2D[] hits;
        int layerMask = 1 << 7;
        hits = Physics2D.OverlapCircleAll(transform.position, 0.85f, layerMask);

        for (int j = 0; j < hits.Length; j++)
        {
            Collider2D hit = hits[j];
            Character target = hit.transform.gameObject.GetComponent<Character>();
            if (target != null && target != transform.gameObject){
                target.applyDamage(1);
            }

            // Berzerker b = hit.transform.gameObject.GetComponent<Berzerker>();
            // if (b != null){
                // transform.gameObject.GetComponent<Character>().applyDamage(1);
                // transform.gameObject.GetComponent<Character>().pushBack(hit.transform);
            // }
        }
    }

}
