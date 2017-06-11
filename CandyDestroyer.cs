using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyHolder CandyHolder;
    public int Reward;
    public GameObject EffectPrefab;
    public Vector3 EffectRotation;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Candy"))
        {
            CandyHolder.AddCandy(Reward);
            
            Destroy(other.gameObject);

            if (EffectPrefab != null)
            {
                Instantiate(EffectPrefab, other.transform.position, Quaternion.Euler(EffectRotation));
            }
        }
    }
}