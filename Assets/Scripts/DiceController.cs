using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    private Rigidbody rb;
    private bool flipping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flipping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!flipping)
        {
            if (Input.GetKey(KeyCode.W)) StartCoroutine(Flip(Vector3.forward));
            else if (Input.GetKey(KeyCode.A)) StartCoroutine(Flip(Vector3.left));
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Flip(Vector3.back));
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Flip(Vector3.right));
        }
    }

    IEnumerator Flip(Vector3 dir)
    {
        flipping = true;

        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + dir / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, dir);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * 300, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        flipping = false;

        //Debug.Log(FindFaceUp());
    }

/*    int FindFaceUp()
    {

    }*/
}
