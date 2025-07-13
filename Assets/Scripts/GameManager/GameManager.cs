using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int round { get; private set; } = 0;
    GameObject nowRoundGO;


    void Awake()
    {
    }
    void Start()
    {
        StartRound();
    }


    public void StartRound()
    {
        nowRoundGO = Instantiate(AssetsManager.Instance.roundDB.roundDict[round],transform);
    }

    public void EndRound()
    {
        Destroy(nowRoundGO);

        round++;

        //BuildUpSceneåƒÇ—èoÇµÇ∆Ç©
    }
}
