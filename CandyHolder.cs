using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyHolder : MonoBehaviour
{
    private const int DefaultCandyAmount = 30;
    private const int RecoverySeconds = 10;

    private int _candyAmount = DefaultCandyAmount;
    private int _counter; // ストック回復までの残り秒数

    public void ConsumeCandy()
    {
        if (_candyAmount > 0)
        {
            --_candyAmount;
        }
    }

    public int GetCandyAmount()
    {
        return _candyAmount;
    }

    public void AddCandy(int amount)
    {
        _candyAmount += amount;
    }

    private void OnGUI()
    {
        GUI.color = Color.black;
        var label = "Candy : " + _candyAmount;
        // 回復してるときだけ秒数を表示
        if (_counter > 0)
        {
            label = label + " (" + _counter + "s)";
        }
        GUI.Label(new Rect(0, 0, 100, 30), label);
    }

    private void Update()
    {
        if (_candyAmount < DefaultCandyAmount && _counter <= 0)
        {
            StartCoroutine(RecoverCandy());
        }
    }

    private IEnumerator RecoverCandy()
    {
        _counter = RecoverySeconds;

        while (_counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            --_counter;
        }

        ++_candyAmount;
    }
}