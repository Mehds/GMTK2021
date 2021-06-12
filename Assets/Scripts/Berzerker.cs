using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berzerker : MonoBehaviour
{   
    public Transform transform = null;
    public float movementSpeed = 0.1f;

    public float minimumDistance = 0.5f;

    public GameObject Target = null;

    public TargetingCone targetingCone = null;

    public bool isAttacking = false;

    public float windUpTime = 1.0f;
    private float timeTilAttack;
    // Start is called before the first frame update
    void Start()
    {
        timeTilAttack = windUpTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAttacking){
            handleMovement();
        }
        else {
            handleAttack();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I see you!");
        isAttacking = true;
        targetingCone.GetComponent<SpriteRenderer>().enabled = true;

    }

    void handleAttack(){
        if (timeTilAttack <= 0){
            Debug.Log("BOOM BOOM SHAKALAKA BOOMBOOM!");
            targetingCone.damageTargets();
            // Get all gameobjects inside the target
            // Deal damage to all of them
            isAttacking = false;
            timeTilAttack = windUpTime;
            targetingCone.GetComponent<SpriteRenderer>().enabled = false;
        } else {
            timeTilAttack -= Time.deltaTime;
        }
    }
    void handleMovement(){
        Vector3 targetPosition = Target.transform.position;
        
        if (targetPosition.x < transform.position.x){
            transform.eulerAngles = new Vector3(0, 0, 0); 
        } else {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
        Vector3 direction = targetPosition - transform.position;

        if (direction.magnitude > minimumDistance) {
            transform.position = transform.position + Vector3.Scale(Vector3.Normalize(direction),new Vector3(movementSpeed, movementSpeed, 0) );
        }

    }


}
