using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private GameObject die;
    [SerializeField]
    private float rotationSpeed = 10;
    private bool rotating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = die.transform.position;

        if (!rotating)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                StartCoroutine(Rotate(90));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                StartCoroutine(Rotate(-90));
            }
        }
    }

    private IEnumerator Rotate(float deg)
    {
        rotating = true;
        float remainingAngle = Mathf.Abs(deg);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * rotationSpeed, remainingAngle);
            transform.Rotate(new Vector3(0, Mathf.Sign(deg) * rotationAngle, 0));
            remainingAngle -= rotationAngle;
            yield return null;
        }

        rotating = false;
    }
}
