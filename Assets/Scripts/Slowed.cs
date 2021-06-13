using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowed : MonoBehaviour
{
    public float maxTimer =2f;
    
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            Destroy(this);
        }
    }

    public void refresh(){
        this.timer = maxTimer;
    }
}
