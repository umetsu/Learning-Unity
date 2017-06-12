using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    private const int StageTipSize = 30;

    public Transform Character;
    public GameObject[] StageTips;
    public int StartTipIndex; // 自動生成開始インデックス
    public int PreInstantiate; // 生成先読み個数
    public List<GameObject> GeneratedStageList = new List<GameObject>(); // 生成済みステージの保持

    private int _currentTipIndex;

    // Use this for initialization
    void Start()
    {
        _currentTipIndex = StartTipIndex - 1;
        UpdateStage(PreInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        var charaPositionIndex = (int) (Character.position.z / StageTipSize);
        if (charaPositionIndex + PreInstantiate > _currentTipIndex)
        {
            UpdateStage(charaPositionIndex + PreInstantiate);
        }
    }

    private void UpdateStage(int toTipIndex)
    {
        if (toTipIndex <= _currentTipIndex) return;

        for (int i = _currentTipIndex + 1; i <= toTipIndex; ++i)
        {
            var stageObject = GeneratedStage(i);
            GeneratedStageList.Add(stageObject);
        }

        // ステージ保持上限になるまで古いステージを削除
        while (GeneratedStageList.Count > PreInstantiate + 2)
        {
            DestroyOldestStage();
        }

        _currentTipIndex = toTipIndex;
    }

    private GameObject GeneratedStage(int tipIndex)
    {
        var nextStageTip = Random.Range(0, StageTips.Length);

        return Instantiate(
            StageTips[nextStageTip],
            new Vector3(0, 0, tipIndex * StageTipSize),
            Quaternion.identity
        );
    }

    private void DestroyOldestStage()
    {
        var oldStage = GeneratedStageList[0];
        GeneratedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}