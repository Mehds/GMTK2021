using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCircle : MonoBehaviour
{
    private GameObject Parent = null;

    // Update is called once per frame

    public void Start(){
        GetComponent<SpriteRenderer>().enabled = false;
        Parent = transform.parent.gameObject;
    }
}
