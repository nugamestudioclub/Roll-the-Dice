using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFollower : MonoBehaviour
{
    DieController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindObjectOfType<DieController>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = controller.transform.position;
    }
}
