using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public int ballId;

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Fire(float direction, float speed)
    {
        rb.AddForce(new Vector3(direction, 0, 0) * speed, ForceMode.Impulse);
    }
}
