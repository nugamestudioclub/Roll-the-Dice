using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieController : MonoBehaviour
{
    [SerializeField]
    private GameObject textPrefab;
    private Rigidbody rb;
    private bool flipping;
    private Dictionary<Vector3, int> sides;

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
            if (Input.GetKey(KeyCode.W)) StartCoroutine(Flip(Vector3.forward, 1, true));
            else if (Input.GetKey(KeyCode.A)) StartCoroutine(Flip(Vector3.left, 1, true));
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Flip(Vector3.back, 1, true));
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Flip(Vector3.right, 1, true));
        }
    }

    private IEnumerator Flip(Vector3 dir, int amt, bool firstMove)
    {
        flipping = true;

        for (int i = 0; i < amt; i++)
        {
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
        }

        int rollNum = FindFaceUp();

        if (firstMove)
        {
            if (textPrefab)
            {
                DisplayText(rollNum.ToString());
            }

            StartCoroutine(FlipDelay(dir, rollNum, false));
        } else
        {
            flipping = false;
        }
    }

    private IEnumerator FlipDelay(Vector3 dir, int rollNum, bool firstMove)
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Flip(dir, rollNum, firstMove));
    }

    private int FindFaceUp()
    {
        ConfigureSides();

        float min = 90;
        int result = 0;

        foreach(KeyValuePair<Vector3, int> entry in sides)
        {
            float angleBetween = Vector3.Angle(entry.Key, Vector3.up);

            if (angleBetween < min)
            {
                result = entry.Value;
                min = angleBetween;
            }
        }

        return result;
    }

    private void ConfigureSides()
    {
        sides = new Dictionary<Vector3, int>
        {
            { transform.up, 2 },
            { -transform.up, 5 },
            { transform.right, 4 },
            { -transform.right, 3 },
            { transform.forward, 1 },
            { -transform.forward, 6 }
        };
    }

    private void DisplayText(string text)
    {
        GameObject textObject = Instantiate(textPrefab, transform.position, Quaternion.identity);
        Debug.Log(textObject.transform.childCount);
        TextMesh mesh = textObject.transform.GetChild(0).GetComponent<TextMesh>();
        mesh.text = text;
        textObject.transform.SetSiblingIndex(0);
        textObject.transform.rotation = Camera.main.transform.rotation;

        textObject.GetComponent<Animator>().Play("displayText");
        Destroy(textObject, 1.5f);
    }
}
