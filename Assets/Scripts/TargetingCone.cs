using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCone : MonoBehaviour
{
    public GameObject Parent;
    public GameObject Target;
    public Transform transform;
    public float updateSpeed = 0.2f;
    public float angleChange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To-Do: Rotation towards player and shift towards player
        //transform.Rotate(0, 0, Time.deltaTime * 5);
        Vector3 direction = Target.transform.position - transform.position;


        // Todo: make it less complicated
        // updated y position
        Vector3 newPosition = transform.position + Vector3.Scale(Vector3.Normalize(direction),new Vector3(0, updateSpeed, 0) );
        if (newPosition.y < Parent.transform.position.y + 0.75 && newPosition.y > Parent.transform.position.y - 0.75){
            transform.position = newPosition;
        }


        // Draw a ray pointing at our target in

        // Debug.Log(transform.eulerAngles);
        Vector3 LoS = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.down) * Vector3.down;

        float angle = Vector3.Angle(Vector3.Normalize(Target.transform.position), Vector3.Normalize(transform.position));
        // float linedUp = Vector3.Angle(Vector3.Normalize(Parent.transform.position), Vector3.Normalize(Target.transform.position));
        //Debug.Log(angle);

        if (angle > 90){
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - angleChange);
        } else {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angleChange);
        }

        Debug.DrawRay(transform.position, LoS, Color.green);
        Debug.DrawRay(transform.position, Target.transform.position, Color.red);

        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Boum! you're dead!");
    }
}
