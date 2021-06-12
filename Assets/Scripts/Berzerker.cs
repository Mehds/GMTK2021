using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berzerker : MonoBehaviour
{   
    public Transform transform = null;
    public float movementSpeed = 0.1f;

    public float minimumDistance = 2f;

    public GameObject Target = null;

    private GameObject targetingCone = null;
    public GameObject targetCircle = null;

    public bool isAttacking = false;
    public float windUpTime = 1.0f;
    private float timeTilAttack;
    public float range = 2f;

    // Start is called before the first frame update
    void Start()
    {
        timeTilAttack = windUpTime;
        targetingCone = transform.Find("TargetingCone").gameObject;
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
        // if (other.transform.gameObject.layer == 8 || other.transform.gameObject.layer == 7){
        //     isAttacking = true;
        //     targetingCone.GetComponent<SpriteRenderer>().enabled = true;
        // }
        if (other.transform.gameObject.layer == 8 || other.transform.gameObject.layer == 7){
            isAttacking = true;
            targetCircle.GetComponent<SpriteRenderer>().enabled = true;
        }


    }

    void handleAttack(){
        if (timeTilAttack <= 0){
            // Debug.Log("BOOM BOOM SHAKALAKA BOOMBOOM!");
            damageTargets();
            // Get all gameobjects inside the target
            // Deal damage to all of them
            isAttacking = false;
            timeTilAttack = windUpTime;
            //targetingCone.GetComponent<SpriteRenderer>().enabled = false;
            targetCircle.GetComponent<SpriteRenderer>().enabled = false;
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


    // public Vector3[] handleLineOfSight(){
    //     float yAngle = targetingCone.transform.eulerAngles.y;

    //     Vector3 LoS = Quaternion.AngleAxis(targetingCone.transform.eulerAngles.z, new Vector3(0, 0, 1)) * Vector3.down;
    //     Vector3 LoSL = Quaternion.AngleAxis(targetingCone.transform.eulerAngles.z + 30, new Vector3(0, 0, 1)) * Vector3.down;
    //     Vector3 LoSR = Quaternion.AngleAxis(targetingCone.transform.eulerAngles.z - 30, new Vector3(0, 0, 1)) * Vector3.down;

    //     if (yAngle == 180){
    //         LoS = new Vector3(-LoS.x, LoS.y, LoS.z);
    //         LoSL = new Vector3(-LoSL.x, LoSL.y, LoSL.z);
    //         LoSR = new Vector3(-LoSR.x, LoSR.y, LoSR.z);
    //     }

        
    //     Vector3[] lines = {LoS, LoSL, LoSR};

    //     return lines;
    // }
    public void damageTargets(){
        // ToDo: Make sure to only be in the characters layer
        // Vector3[] lines = handleLineOfSight();
        int targetMask = 1 << 8 | 1 << 7;
        // for (int i = 0; i < 3; i++){
        //     RaycastHit2D[] hits;

        //     hits = Physics2D.RaycastAll(transform.position, lines[i], range, targetMask);
        //     Debug.Log(hits.Length);
        //     for (int j = 0; j < hits.Length; j++)
        //     {
        //         RaycastHit2D hit = hits[j];
        //         Debug.Log("I smash");
        //         Debug.Log(hit.transform.gameObject);

        //         Character target = hit.transform.gameObject.GetComponent<Character>();
        //         target.applyDamage(5);
        //     }
        // }

        RaycastHit2D[] hits;

        hits = Physics2D.CircleCastAll(transform.position, 1.5f, new Vector3(0,0,0), Mathf.Infinity, targetMask);
        
        for (int j = 0; j < hits.Length; j++)
        {
            RaycastHit2D hit = hits[j];
            Character target = hit.transform.gameObject.GetComponent<Character>();
            if (target != null && target != transform.gameObject){
                target.applyDamage(5);
            }
        }
    }
}
