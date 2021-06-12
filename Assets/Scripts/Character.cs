using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float movementSpeed = 0.3f;
    public int health = 1;
    public bool isAlive = true;
    public Rigidbody2D rb2d = null;
    public Transform transform = null;

    // Start is called before the first frame update
    
    public void applyDamage(int damage){
        health -= damage;
        if (health <= 0){
            isAlive = false;
            Debug.Log("OH NO IT DIED");
        }
    }

    public void pushBack(Transform t){
        Vector3 direction = t.position - transform.position;
       
        transform.position -= Vector3.Scale(direction, new Vector3(1.5f, 1.5f, 0f));

    }
}
