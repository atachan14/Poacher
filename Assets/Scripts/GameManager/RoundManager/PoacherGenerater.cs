using System;
using UnityEngine;

public class PoacherGenerater : MonoBehaviour
{

    protected Action[] genActions;

    void Awake()
    {
        genActions = new Action[]
        {
        Wave0,
        Wave1,
        Wave2,
        Wave3,
        Wave4,
        Wave5,
        Wave6,
        Wave7,
        Wave8,
        Wave9,
        Wave10,
        };
    }

   

    void OnEnable()
    {
        BaseRoundManager.OnWaveStart += HandleWaveStart;
    }

    void OnDisable()
    {
        BaseRoundManager.OnWaveStart -= HandleWaveStart;
    }
    protected virtual void HandleWaveStart(int waveNum)
    {
        if (waveNum >= 0 && waveNum < genActions.Length)
        {
            genActions[waveNum]?.Invoke();
        }
        else
        {
            Debug.LogWarning($"����`��Wave�ԍ�: {waveNum}");
        }
    }


    protected virtual void Wave0() => Debug.Log($"{name} Gen0 �J�n�I");
    protected virtual void Wave1() => Debug.Log("");
    protected virtual void Wave2() => Debug.Log($"{name} Gen2 �J�n�I");
    protected virtual void Wave3() => Debug.Log($"{name} Gen3 �J�n�I");
    protected virtual void Wave4() => Debug.Log($"{name} Gen4 �J�n�I");
    protected virtual void Wave5() => Debug.Log($"{name} Gen5 �J�n�I");
    protected virtual void Wave6() => Debug.Log($"{name} Gen6 �J�n�I");
    protected virtual void Wave7() => Debug.Log($"{name} Gen7 �J�n�I");
    protected virtual void Wave8() => Debug.Log($"{name} Gen8 �J�n�I");
    protected virtual void Wave9() => Debug.Log($"{name} Gen9 �J�n�I");
    protected virtual void Wave10() => Debug.Log($"{name} Gen10 �J�n�I");

}
