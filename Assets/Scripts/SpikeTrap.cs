using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{



    public bool isAttacking = false;
    public float windUpTime = 0.5f;
    private float timeTilAttack;
    int targetMask = 1 << 8 | 1 << 7;
    // Start is called before the first frame update
    void Start()
    {
        timeTilAttack = windUpTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking){
            handleAttack();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 8 || other.transform.gameObject.layer == 7){
            isAttacking = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void handleAttack(){
        if (timeTilAttack <= 0){
            damageTargets();
            isAttacking = false;
            timeTilAttack = windUpTime;
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        } else {
            timeTilAttack -= Time.deltaTime;
        }
    }

     public void damageTargets(){

        Collider2D[] hits;

        //hits = Physics2D.OverlapCircleAll(transform.position, 1.5f, targetMask);
        hits = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), gameObject.GetComponent<BoxCollider2D>().size, 0f, targetMask);
        
        for (int j = 0; j < hits.Length; j++)
        {
            Collider2D hit = hits[j];
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
