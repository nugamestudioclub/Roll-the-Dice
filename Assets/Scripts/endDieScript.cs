using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endDieScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * 200, Time.deltaTime * 200, Time.deltaTime * 200);
    }
}
