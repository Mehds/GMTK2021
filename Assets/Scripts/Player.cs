using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 0.3f;
    public int health = 1;
    public Rigidbody2D rb2d = null;
    public Transform transform = null;

    public GameObject anchor = null;
    public LineRenderer lineRenderer = null;
    
    public float maxDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // if (GetComponent<Rigidbody2d>()){
        //     rb2d = GetComponent<Rigidbody2d>();
        // }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w") || Input	.GetKey("up")){
            handleMovement( new Vector3(0, movementSpeed, 0));
        }

        if (Input.GetKey("s") || Input	.GetKey("down")){
            handleMovement( new Vector3(0, -1 * movementSpeed, 0));

            // transform.position = transform.position + new Vector3(0, -1 * movementSpeed, 0) ;
        }

        if (Input.GetKey("a") || Input	.GetKey("left")){
            handleMovement( new Vector3(-1 * movementSpeed, 0, 0));

            // transform.position = transform.position + new Vector3(-1 * movementSpeed, 0, 0) ;
        }

        if (Input.GetKey("d") || Input	.GetKey("right")){
            handleMovement( new Vector3(movementSpeed, 0, 0));

            // transform.position = transform.position + new Vector3(movementSpeed, 0, 0) ;
        }

        lineRenderer.startWidth = 0.3f;
        lineRenderer.endWidth = 0.3f;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, anchor.transform.position);

    }

    void handleMovement(Vector3 intendedMovement){
        
        Vector3 newPosition = transform.position + intendedMovement;
        float distance = (newPosition - anchor.transform.position).magnitude;

        if (distance < maxDistance){
            transform.position = newPosition;
        }

        // TO-DO
        // Else, get yanked

        // Idea: Movement speed is a function of distance?

    }
}
