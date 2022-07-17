using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    private bool redSpace;
    private bool goal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (redSpace)
        {
            other.GetComponent<DieController>().Respawn();
        } else if (goal)
        {
            // win game
            Debug.Log("you win");
        }
    }

    public void MakeRedSpace()
    {
        redSpace = true;
        goal = false;
    }

    public void MakeGoal()
    {
        goal = true;
        redSpace = false;
    }
}
