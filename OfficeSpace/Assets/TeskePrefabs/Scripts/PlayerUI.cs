using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public GameObject player;
    public GameObject clockPrefab;
    FPSController currentPlayerState;
    private GameObject clock;
    [SerializeField] private GameObject clockPosition;

    // Start is called before the first frame update
    void Start()
    {
        THECLOCK();
        currentPlayerState = player.GetComponent<FPSController>();
    }

    // Update is called once per frame
    void Update()
    {
        WorkIsLate();
    }


    void THECLOCK()
    {

    }

    void GETTOWORK()
    {

    }

    void WorkIsLate()
    {


    }

    void ExtendTimer()
    {
        //When a task is started, erase the target time
        //When a task is finished, set a new target time
    }
}
