using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject die;
    private Vector3 direction;
    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        yRotation = 0;
        direction = Vector3.back;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = die.transform.position + Vector3.up * 3 + direction * 6;
        transform.rotation = Quaternion.Euler(25, yRotation, 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            yRotation = -90;
            direction = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            yRotation = 90;
            direction = -Vector3.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            yRotation = 0;
            direction = Vector3.back;
        }
    }
}
