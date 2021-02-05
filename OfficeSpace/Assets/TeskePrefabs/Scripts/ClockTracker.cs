using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTracker : MonoBehaviour
{

    GameStatesManager manager;
    [SerializeField] private GameObject minuteHand;
    [SerializeField] private GameObject hourHand;
    private float targetMinuteRotation;
    private float startingMinuteRotation;
    private float startingHourRotation;
    private float targetHourRotation;
    private float t;


    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameStatesManager>();
        startingMinuteRotation = minuteHand.transform.rotation.y;
        startingHourRotation = hourHand.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        t = manager.totalTime / manager.endTime;
        targetHourRotation = -240 * t;
        targetMinuteRotation = -2880 * t;
        minuteHand.transform.localEulerAngles = new Vector3(0, startingMinuteRotation + targetMinuteRotation, 0);
        hourHand.transform.localEulerAngles = new Vector3(0, 90 + targetHourRotation, 0);
    }
}
