using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berzerker : MonoBehaviour
{   
    private Transform transform = null;
    public float movementSpeed = 0.1f;

    public float minimumDistance = 2f;

    public GameObject Target = null;
    public GameObject targetingCircle = null;

    public bool isAttacking = false;
    public float windUpTime = 1.0f;
    private float timeTilAttack;
    int targetMask = 1 << 8 | 1 << 7;

    // Start is called before the first frame update
    void Start()
    {
        timeTilAttack = windUpTime;
        transform = GetComponent<Transform>();
        // targetingCone = transform.Find("TargetingCone").gameObject;
    }

    // Update is called once per frame
    void Update()
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
        if (other.transform.gameObject.layer == 7){
            isAttacking = true;
            targetingCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void handleAttack(){
        if (timeTilAttack <= 0){
            damageTargets();
            isAttacking = false;
            timeTilAttack = windUpTime;
            targetingCircle.GetComponent<SpriteRenderer>().enabled = false;
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

    public void damageTargets(){

        Collider2D[] hits;

        float radius = targetingCircle.GetComponent<CircleCollider2D>().bounds.extents.x; 

        hits = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
        
        for (int j = 0; j < hits.Length; j++)
        {
            Collider2D hit = hits[j];
<<<<<<< HEAD
            //Debug.Log((transform.position - hit.transform.position).magnitude);
=======
>>>>>>> c7989da212116df4af838893882bb7e46f683f55
            Character target = hit.transform.gameObject.GetComponent<Character>();
            if (target == null){
                target = hit.transform.parent.gameObject.GetComponent<Character>();
            }
            if (target != null && target != transform.gameObject){
                target.applyDamage(5);
            }
        }
    }
}
