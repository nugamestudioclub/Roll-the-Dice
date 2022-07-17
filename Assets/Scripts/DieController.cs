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
    private DiceFollower pointer;
    Vector3 direction;
    float distToWall = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        flipping = false;
        pointer = GameObject.FindObjectOfType<DiceFollower>();
        direction= pointer.transform.forward; ;
    }

    // Update is called once per frame
    void Update()
    {
        
        float rotationZ = 0;
        if (!flipping)
        {
            

            if (Input.GetKey(KeyCode.W))
            {
                direction = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
                print(CheckWalls(direction));
                if (CheckWalls(direction))
                    StartCoroutine(Flip(Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up), 1, true));
                
                print(direction);
                
               
            }
            else if (Input.GetKey(KeyCode.A))
            {
                direction = -Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);
                if (CheckWalls(direction))
                    StartCoroutine(Flip(-Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up), 1, true));
                
                print(direction);
                
                
            }
            else if (Input.GetKey(KeyCode.S))
            {
                direction = -Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
                if (CheckWalls(direction))
                    StartCoroutine(Flip(-Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up), 1, true));
                
                print(direction);
                
                
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up);
                if (CheckWalls(direction))
                    StartCoroutine(Flip(Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up), 1, true));
               
                print(direction);
                
                
            }
        }

        RaycastHit hit;
        
        
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            distToWall = Vector3.Distance(transform.position, hit.point);
            //print(distToWall);
            if (distToWall <= 0.5)
            {
                Stop();
            }
        }
    }
    bool CheckWalls(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            distToWall = Vector3.Distance(transform.position, hit.point);
            //print(distToWall);
            if (distToWall <= 0.8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
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

            CorrectPosition();
            yield return new WaitForSeconds(.2f);
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

    private void CorrectPosition()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x * 2) / 2, 0.5f, Mathf.Round(transform.position.z * 2) / 2);
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
        if (textPrefab == null)
        {
            Debug.LogError("No text prefab provided");
        }

        GameObject textObject = Instantiate(textPrefab, transform.position, Quaternion.identity);
        TextMesh mesh = textObject.transform.GetChild(0).GetComponent<TextMesh>();
        mesh.text = text;

        textObject.GetComponent<Animator>().Play("displayText");
        Destroy(textObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        StopAllCoroutines();
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0.5f, 0.5f, 0.5f);
        transform.rotation = Quaternion.identity;
        flipping = false;
        GameObject.Find("Camera Rotator").transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void Stop()
    {
        StopAllCoroutines();
        Transform rotator = GameObject.Find("Camera Rotator").transform;
        transform.position = new Vector3(((int)transform.position.x) + 0.5f, ((int)transform.position.y) + 0.5f, ((int)transform.position.z) + 0.5f);
        rotator.eulerAngles = new Vector3((int)rotator.eulerAngles.x, (int)rotator.eulerAngles.y, (int)rotator.eulerAngles.z);
        flipping = false;
    }
}
