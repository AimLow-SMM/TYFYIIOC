using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerStates
{
    SAFE, UNSAFE, DANGER, WORKING
}

public class GameStatesManager : MonoBehaviour
{

    public PlayerStates currentState;
    [SerializeField] private float safetyCounter = 0;
    [SerializeField] private float endOfSafeTime;
    [SerializeField] private float endOfUnsafeTime;
    public float totalTime;
    public float endTime;
    public string nextWinScene;
    public AudioSource fogHorn;
    public bool fogHornPlayed = false;
    public bool isHoldingThing;

    // Start is called before the first frame update
    void Start()
    {
        ResetToSafe(endOfSafeTime, endOfUnsafeTime);
        isHoldingThing = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentState != PlayerStates.WORKING)
        {
            safetyCounter += Time.deltaTime;
        }

        if(safetyCounter >= endOfSafeTime)
        {
            currentState = PlayerStates.UNSAFE;
            Debug.Log("Player is UNSAFE");
        }
        if(safetyCounter >= endOfUnsafeTime)
        {
            currentState = PlayerStates.DANGER;
            Debug.Log("Player is in DANGER");
        }

        totalTime += Time.deltaTime;

        if (!fogHornPlayed && endTime - totalTime <= 20)
        {
            fogHorn.Play();
            fogHornPlayed = true;
        }

        if(totalTime > endTime)
        {
            SceneManager.LoadScene(nextWinScene);
        }
    }

    //Sets the player to safe, resets the safety timer, and redefines the boundaries of safety time based on the individual call
    public void ResetToSafe(float safetyTime, float dangerTime)
    {
        currentState = PlayerStates.SAFE;
        Debug.Log("Player is SAFE");
        endOfSafeTime = safetyTime;
        endOfUnsafeTime = dangerTime;
        safetyCounter = 0;
    }

    public void PlayerIsWorking()
    {
        currentState = PlayerStates.WORKING;
        Debug.Log("Player is WORKING");

        safetyCounter = 0;
    }


}
