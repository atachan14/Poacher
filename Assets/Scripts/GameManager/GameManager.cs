using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }
    public int round { get; private set; } = 0;
    GameObject nowRoundGO;


    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        StartRound();
    }


    public void StartRound()
    {
        nowRoundGO = Instantiate(AssetsManager.Instance.roundDB.roundDict[round], transform);
    }

    public void EndRound()
    {
        Destroy(nowRoundGO);

        round++;

        //BuildUpSceneåƒÇ—èoÇµÇ∆Ç©
    }

    public void GameOver(GameObject animal)
    {
        Debug.Log($"Game Over ( dead : {animal} )");

    }
}
