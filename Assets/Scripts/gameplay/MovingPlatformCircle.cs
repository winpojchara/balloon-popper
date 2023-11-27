using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCircle : MonoBehaviour
{

    [SerializeField] float diameter = 1;
    [SerializeField] Transform center;
    [SerializeField] float turnSpeed = 1;
    [SerializeField]float value;

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        value += turnSpeed * Time.deltaTime;
        Vector3 targetposition = center.position + new Vector3(Mathf.Sin(value * Mathf.PI) * diameter, Mathf.Cos(value * Mathf.PI) * diameter);

        transform.position =targetposition;
    }

}
