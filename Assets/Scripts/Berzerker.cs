using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berzerker : MonoBehaviour
{   
    public Transform transform = null;
    public float movementSpeed = 0.1f;

    public float minimumDistance = 0.5f;

    public GameObject target = null;

    public Collider2D fieldOfView = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = target.transform.position - transform.position;
        
        if (direction.magnitude > minimumDistance) {
         transform.position = transform.position + Vector3.Scale(Vector3.Normalize(direction),new Vector3(movementSpeed, movementSpeed, 0) );
        }

        // To do

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boum! you're dead!");
    }
    



}
