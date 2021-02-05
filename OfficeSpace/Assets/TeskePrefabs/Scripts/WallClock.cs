using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    float timeConstant;
    public float timeMultiplier = 1;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timeConstant = timer * timeMultiplier;
        ClockHandMovement();

    }

    void ClockHandMovement()
    {
        float second = timeConstant;
        float minute  = second / 60;
        Debug.Log("Current Minute: " + minute);
        float hour = minute / 60;
        Debug.Log("Current Hour: " + hour);
        Debug.Log("Current Time.deltaTime: " + timer);

        float minuteAngle = -360 * (minute);
        float hourAngle = -360 * (hour);

        hourHand.localRotation = Quaternion.Euler(0, 120 + hourAngle, 0);
        minuteHand.localRotation = Quaternion.Euler(0, minuteAngle, 0);
    }
}
