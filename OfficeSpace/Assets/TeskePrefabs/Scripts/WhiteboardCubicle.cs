using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteboardCubicle : CubicleInteraction
{
    [SerializeField] private GameObject minigame;
    [SerializeField] private Transform computerScreen;
    private bool gameDone = true;
    private bool gameDestroyed = false;
    private GameObject theButtonGame;
    /*
    private void Update()
    {
        ClearConditionMet();

        if (gameDone && !gameDestroyed)
        {
            DestroyMinigame();
        }
    }

    public override void SpawnMinigame()
    {
        theButtonGame = Instantiate(minigame, computerScreen, false);
        gameDone = false;
    }

    public override void ClearConditionMet()
    {
        if (Input.GetMouseButtonDown(1) && !gameDone)
        {
            base.ClearConditionMet();
            Debug.Log("Markerboard Minigame won.");
            gameDone = true;
        }
    }

    private void DestroyMinigame()
    {
        Destroy(theButtonGame);
        gameDestroyed = true;
    }
    */
}
