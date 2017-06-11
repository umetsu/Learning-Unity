using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    private Vector3 _startPosition;

    public float Amplitude;
    public float Speed;

    // Use this for initialization
    void Start()
    {
        _startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var z = Amplitude * Mathf.Sin(Time.time * Speed);
        transform.localPosition = _startPosition + new Vector3(0, 0, z);
    }
}