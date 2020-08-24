using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;

    // Saved for optimization purposes
    Rigidbody rbd;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();

        rbd.velocity = transform.forward * speed;
    }
}
