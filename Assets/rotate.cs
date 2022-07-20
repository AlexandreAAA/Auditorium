using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public Transform rotation;
    public float rotatespeed;

    private void Update()
    {
        transform.Rotate(0, rotatespeed * Time.deltaTime, 0);

    }
}
