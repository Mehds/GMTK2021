using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCone : MonoBehaviour
{
    public Berzerker Parent;
    public Transform TargetTransform;
    public Transform transform;
    public float range = 2f;
    public float updateSpeed = 0.2f;
    public float angleChange = 1f;

    private Vector3 LoS = new Vector3(0, 0, 0);
    private Vector3 LoSL = new Vector3(0, 0, 0);
    private Vector3 LoSR = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        //Parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Parent.isAttacking){
            handleMovement();
        }

    }

    void handleMovement(){
        // To-Do: Rotation towards player and shift towards player
        //transform.Rotate(0, 0, Time.deltaTime * 5);
        Vector3 TargetPosition = TargetTransform.position;
        Vector3 direction = TargetPosition - transform.position;


        float angle = Vector3.Angle(LoS, TargetPosition);
        LoS = Quaternion.AngleAxis(transform.eulerAngles.z, new Vector3(0, 0, 1)) * Vector3.down;
        float yAngle = transform.eulerAngles.y;

        if (yAngle == 180){
             LoS = new Vector3(-LoS.x, LoS.y, LoS.z);
        }

        Debug.DrawRay(transform.position, LoS, Color.red);

        // Debug.Log(angle);

        if (angle > 3 && angle < 357){
            if (LoS.y < Vector3.Normalize(TargetPosition).y){
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - angleChange);
            } else {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angleChange);
            }
        }        

    }
}
