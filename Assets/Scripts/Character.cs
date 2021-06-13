using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float movementSpeed;
    public int health;
    public bool isAlive = true;
    private Transform transform = null;

    // Start is called before the first frame update
    
    void Start(){
        transform = GetComponent<Transform>();
    }

    public void applyDamage(int damage){
        health -= damage;
        if (health <= 0){
            isAlive = false;
        }
    }

    // public void pushBack(Transform t){
    //     Vector3 direction = t.position - transform.position;
    //     transform.position -= Vector3.Scale(direction, new Vector3(1.5f, 1.5f, 0f));

    // }
}
