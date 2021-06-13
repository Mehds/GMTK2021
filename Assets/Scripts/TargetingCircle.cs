using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingCircle : MonoBehaviour
{
    private GameObject Parent = null;

    public void Start(){
        GetComponent<SpriteRenderer>().enabled = false;
        Parent = transform.parent.gameObject;
    }
}
