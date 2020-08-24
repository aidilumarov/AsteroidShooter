using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float tumble;

    Rigidbody rbd;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();

        rbd.angularVelocity = Random.insideUnitSphere * tumble;
    }

}
