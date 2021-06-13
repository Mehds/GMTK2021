using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour
{
    public bool horizontal = true;
    public int coefficient = 1;
    private Vector3 initialPosition;
    public float range;
    private float maxCoord;
    private float minCoord;
    public float movementSpeed = 0.001f;
    public float rotationDegrees = 1f;
    private Transform transfrom;

    // Start is called before the first frame update
    void Start()
    {
        transfrom = GetComponent<Transform>();
        initialPosition = transfrom.position;
        if(horizontal){
            maxCoord = initialPosition.x + range;
            minCoord = initialPosition.x - range;
        } else {
            maxCoord = initialPosition.y + range;
            minCoord = initialPosition.y - range;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotate();

        if (horizontal){
            moveHorizontal();
        } else {
            moveVertical();
        }
    }

    void rotate(){
        transform.eulerAngles += Vector3.forward * rotationDegrees;
    }

    void moveHorizontal(){
        if ((transfrom.position.x + movementSpeed) > maxCoord){
            coefficient = -1;
        }

        if ((transfrom.position.x - movementSpeed) < minCoord){
            coefficient = 1;
        }

        transfrom.position += new Vector3(coefficient * movementSpeed, 0, 0);
    }

    void moveVertical(){
        if ((transfrom.position.y + movementSpeed) > maxCoord){
            coefficient = -1;
        }

        if ((transfrom.position.y - movementSpeed) < minCoord){
            coefficient = 1;
        }

        transfrom.position += new Vector3(0, coefficient * movementSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log(other);

        if (other.transform.gameObject.layer == 7 || other.transform.gameObject.layer == 8){
            if (other.transform.gameObject.GetComponent<Character>().isAlive){
                other.transform.gameObject.GetComponent<Character>().applyDamage(3);
            }
        }
    }


}
