using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    public bool isLighting = true;
    public GameObject light;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isLighting = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isLighting)
            {
                isLighting = false;
                turnOn_turnOff_Light();
            }
            else
            {
                isLighting = true;
                turnOn_turnOff_Light();
            }
        }
    }


    public void turnOn_turnOff_Light()
    {
        if (light)
        {
            if (light.active)
            {
                light.SetActive(false);
            }
            else
            {
                light.SetActive(true);
            }
        }

    }
}
