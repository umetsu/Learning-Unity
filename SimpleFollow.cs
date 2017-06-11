using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public GameObject Target;
    public float FollowSpeed;
    
    private Vector3 _diff;
    
    void Start()
    {
        _diff = Target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        // 線形補間によるスムージング
        transform.position = Vector3.Lerp(
            transform.position,
            Target.transform.position - _diff,
            Time.deltaTime * FollowSpeed);
    }
}