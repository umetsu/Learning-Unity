using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private const int SphereCandyFrequency = 3;
    private const int MaxShotPower = 5;
    private const int RecoverySeconds = 3;

    private int _sampleCandyCount;
    private int _shotPower = MaxShotPower;
    private AudioSource _shotSound;

    public GameObject[] CandyPrefabs;
    public GameObject[] CandySquarePrefabs;
    public CandyHolder CandyHolder;
    public float ShotSpeed;
    public float ShotTorque;
    public float BaseWidth;

    void Start()
    {
        _shotSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
    }

    private void Shot()
    {
        if (CandyHolder.GetCandyAmount() <= 0) return;
        if (_shotPower <= 0) return;

        var candy = Instantiate(SampleCandy(), GetInstantiatePosition(), Quaternion.identity);

        candy.transform.parent = CandyHolder.transform;

        var candyRigidbody = candy.GetComponent<Rigidbody>();
        candyRigidbody.AddForce(transform.forward * ShotSpeed);
        candyRigidbody.AddTorque(new Vector3(0, ShotTorque, 0));

        CandyHolder.ConsumeCandy();
        ConsumePower();
        
        _shotSound.Play();
    }

    private void ConsumePower()
    {
        --_shotPower;
        StartCoroutine(RecoverPower());
    }

    private IEnumerator RecoverPower()
    {
        yield return new WaitForSeconds(RecoverySeconds);
        ++_shotPower;
    }

    private void OnGUI()
    {
        GUI.color = Color.black;

        var label = "";
        for (int i = 0; i < _shotPower; i++)
        {
            label = label + "+";
        }
        
        GUI.Label(new Rect(0, 15, 100, 30), label);
    }

    private GameObject SampleCandy()
    {
        GameObject prefab;
        if (_sampleCandyCount % SphereCandyFrequency == 0)
        {
            prefab = CandyPrefabs[Random.Range(0, CandyPrefabs.Length)];
        }
        else
        {
            prefab = CandySquarePrefabs[Random.Range(0, CandySquarePrefabs.Length)];
        }

        ++_sampleCandyCount;

        return prefab;
    }

    private Vector3 GetInstantiatePosition()
    {
        var x = BaseWidth * (Input.mousePosition.x / Screen.width) - BaseWidth / 2;
        return transform.position + new Vector3(x, 0, 0);
    }
}