using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField]
    Vector3 movementVector;

    [SerializeField]
    [Range(0, 1)]
    float movementFactor;
    [SerializeField]
    float period = 2f;

    [SerializeField]
    float rotationX;
    [SerializeField]
    float rotationY;
    [SerializeField]
    float rotationZ;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        BeginOscillation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            BeginOscillation();
        }
    }

    void BeginOscillation()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

        transform.Rotate(rotationX, rotationY, rotationZ);
    }
}
