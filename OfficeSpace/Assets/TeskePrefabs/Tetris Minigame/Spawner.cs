using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] pieces;
    private int randomInt;
    public bool hasStarted = false;
    public GameObject scaleReference;
    public float scaleX;
    public float scaleY;
    public float scaleZ;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeNext();
        SpawnTetrimino();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTetrimino()
    {
        if(transform.childCount == 0)
        {
            GameObject nextPiece = Instantiate(pieces[randomInt], transform.position, Quaternion.identity);
            nextPiece.transform.SetParent(this.transform);
            
            RandomizeNext();
        }
    }

    public void RandomizeNext()
    {
         randomInt = Random.Range(0, pieces.Length);
    }

    public bool HasStarted()
    {
        if(hasStarted == false)
        {
            hasStarted = true;
            return false;
        }

        if(hasStarted == true)
        {
            return true;
        }

        return true;
    }
}
