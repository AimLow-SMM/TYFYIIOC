using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCubicle : MonoBehaviour
{
    [SerializeField] private GameObject tiredMan;
    [SerializeField] private GameObject wiredMan;


    // Start is called before the first frame update
    void Start()
    {
        wiredMan.SetActive(false);
        tiredMan.SetActive(true);
    }

    public void SwitchMen()
    {
        wiredMan.SetActive(true);
        tiredMan.SetActive(false);
        this.tag = "Untagged";
    }

    // Update is called once per frame

}
