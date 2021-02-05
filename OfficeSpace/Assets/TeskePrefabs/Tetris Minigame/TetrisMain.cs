using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisMain : MonoBehaviour
{
    public static int gridW = 10;
    public static int gridH = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIsInsideGrid (Vector3 pos)
    {
        return (pos.x >= 0 && pos.x < gridW && pos.y >= 0);
    }

    public Vector3 Round (Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }
}
