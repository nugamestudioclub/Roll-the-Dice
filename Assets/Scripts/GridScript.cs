using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    private int height = 20;
    private int width = 10;
    private float spaceSize = 1f;

    [SerializeField]
    private GameObject cellPrefab;
    [SerializeField]
    private GameObject redSpacePrefab;
    [SerializeField]
    private GameObject goalPrefab;
    private GameObject[,] grid;
    private ArrayList redSpaces;
    private Vector2Int goal;

    [SerializeField]
    private Vector2 goalLocation = new Vector2(9, 19);
    [SerializeField]
    private Vector2[] redLocations = new Vector2[]{new Vector2(1,0),new Vector2(2,0)};
    

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new GameObject[width, height];
        redSpaces = new ArrayList();

        configureGrid();

        if (cellPrefab == null || redSpacePrefab == null)
        {
            Debug.LogError("No cell prefab provided");
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cell;
                bool isRedSpace = redSpaces.Contains(new Vector2Int(x, y));
                bool isGoal = goal == new Vector2Int(x, y);

                if (isRedSpace)
                {
                    cell = redSpacePrefab;
                } else if (isGoal)
                {
                    cell = goalPrefab;
                } else
                {
                    cell = cellPrefab;
                }

                grid[x, y] = Instantiate(cell, new Vector3(x * spaceSize + 1, 0.5f, y * spaceSize + 1), Quaternion.identity);
                grid[x, y].transform.parent = transform;
                grid[x, y].gameObject.name = "Cell (" + x + ", " + y + ")";

                if (isRedSpace) grid[x, y].GetComponent<CellScript>().MakeRedSpace();
                else if (isGoal)
                {
                    grid[x, y].GetComponent<CellScript>().MakeGoal();
                }
            }
        }
    }

    private void configureGrid()
    {

        setGoal((int)goalLocation.x, (int)goalLocation.y);
        foreach(Vector2 v in redLocations)
        {
            setRedSpace((int)v.x, (int)v.y);
        }
        //setRedSpace(1, 0);
        //setRedSpace(2, 0);
    }

    private void setRedSpace(int x, int y)
    {
        redSpaces.Add(new Vector2Int(x, y));
    }

    private void setGoal(int x, int y)
    {
        goal = new Vector2Int(x, y);
    }
}
