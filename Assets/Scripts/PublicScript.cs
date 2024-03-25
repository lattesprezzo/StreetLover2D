using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicScript : MonoBehaviour
{

    [SerializeField] PrivateScript privateScript;
    float FetchedEnergy;
    void Start()
    {

        FetchedEnergy = privateScript.PublicEnergy = 10f;
        Debug.Log("Fetched Energy is: " + FetchedEnergy);
        
    }


    void Update()
    {
        
    }
}
