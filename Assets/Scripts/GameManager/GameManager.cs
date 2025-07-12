using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int round { get; private set; } = 0;
    GameObject nowRoundGO;
    [SerializeField] RoundData roundData;

    [SerializeField] private PoacherDatabase poacherDatabase;
    public static PoacherDatabase DB { get; private set; }

    void Awake()
    {
        DB = poacherDatabase;
    }
    void Start()
    {
        StartRound();
    }


    public void StartRound()
    {
        nowRoundGO = Instantiate(roundData.roundDict[round],transform);
    }

    public void EndRound()
    {
        Destroy(nowRoundGO);

        round++;

        //BuildUpSceneåƒÇ—èoÇµÇ∆Ç©
    }
}
