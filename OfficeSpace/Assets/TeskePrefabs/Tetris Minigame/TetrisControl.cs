using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisControl : MonoBehaviour
{
    public float fallSpeed = 1;
    public float fallMultiplier = 10;
    public float fall = 0;
    public static int minWidth;
    public static int maxWidth;
    public static int minHeight;
    public static int maxHeight;

    public GameObject centerOfGrid;
    public GameObject spawner;

    public Vector3 rotationPoint;

    public bool hasStarted = false;

    public Transform[,] grid;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Started: " + FindObjectOfType<Spawner>().hasStarted);
        //use centerOfGrid object to define grid boundaries
        centerOfGrid = GameObject.FindGameObjectWithTag("origin");

        minWidth = Mathf.RoundToInt(centerOfGrid.transform.position.x);
        maxWidth = Mathf.RoundToInt(centerOfGrid.transform.position.x) + 10;
        minHeight = Mathf.RoundToInt(centerOfGrid.transform.position.y);
        maxHeight = Mathf.RoundToInt(centerOfGrid.transform.position.y) + 20;

        
        if (!FindObjectOfType<Spawner>().HasStarted())
        {
            grid = new Transform[maxWidth - minWidth, maxHeight - minHeight];

        }

    }

    // Update is called once per frame
    void Update()
    {
        TetrisMovement();
    }

    void TetrisMovement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right;

            if (ValidMove() == false)
            {
                transform.position -= Vector3.right;
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left;

            if (ValidMove() == false)
            {
                transform.position -= Vector3.left;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            if (ValidMove() == false)
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            fallSpeed = fallSpeed / fallMultiplier;
            if (ValidMove() == false)
            {
                //transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }

        if (Time.time - fall > fallSpeed)
        {
            transform.position += Vector3.down;

            if (ValidMove() == false)
            {
                //Process of adding tetrimino to grid, check for lines, then spawn next piece
                transform.position -= Vector3.down;
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<Spawner>().SpawnTetrimino();
            }

            fall = Time.time;
        }

    }

    void DeleteLine(int i)
    {
        for(int j = minWidth; j < maxWidth; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }

        Debug.Log("Line deleted");
    }

    void RowDown(int i)
    {
        for(int y = minHeight + i; y < maxHeight; y++)
        {
            for(int j = minWidth; j < maxWidth; j++)
            {
                if(grid[j, y] != null)
                {

                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position += Vector3.down;
                }
            }
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x) - minWidth;
            int roundedY = Mathf.RoundToInt(children.transform.position.y) - minHeight;

            if (roundedX < minWidth || roundedX >= maxWidth || roundedY < minHeight || roundedY >= maxHeight)
            {
                Debug.Log("Not a valid move");
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                Debug.Log("Not a valid move, at bottom");
                return false;
            }
        }
        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x) - minWidth;
            int roundedY = Mathf.RoundToInt(children.transform.position.y) - minHeight;

            grid[roundedX, roundedY] = children;
            Debug.Log("Block added to grid at: " + roundedX + ", " + roundedY);
        }

        transform.SetParent(null);
        Debug.Log("Tetrimino added to grid");
    }

    void CheckForLines()
    {
        for (int i = maxHeight - 1; i >= minHeight; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for(int j = minWidth; j < maxWidth; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }

        return true;
    }
}
