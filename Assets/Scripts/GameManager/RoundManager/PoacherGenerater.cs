using System;
using UnityEngine;

public class PoacherGenerater : MonoBehaviour
{

    protected Action[] genActions;

    void Awake()
    {
        genActions = new Action[]
        {
        Gen0,
        Wave1,
        Wave2,
        Gen3,
        Gen4,
        Gen5,
        Gen6,
        Gen7,
        Gen8,
        Gen9,
        Gen10,
        };
    }

   

    void OnEnable()
    {
        RoundManager.OnWaveStart += HandleWaveStart;
    }

    void OnDisable()
    {
        RoundManager.OnWaveStart -= HandleWaveStart;
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


    protected virtual void Gen0() => Debug.Log($"{name} Gen0 �J�n�I");
    protected virtual void Wave1() => Debug.Log($"{name} Gen1 �J�n�I");
    protected virtual void Wave2() => Debug.Log($"{name} Gen2 �J�n�I");
    protected virtual void Gen3() => Debug.Log($"{name} Gen3 �J�n�I");
    protected virtual void Gen4() => Debug.Log($"{name} Gen4 �J�n�I");
    protected virtual void Gen5() => Debug.Log($"{name} Gen5 �J�n�I");
    protected virtual void Gen6() => Debug.Log($"{name} Gen6 �J�n�I");
    protected virtual void Gen7() => Debug.Log($"{name} Gen7 �J�n�I");
    protected virtual void Gen8() => Debug.Log($"{name} Gen8 �J�n�I");
    protected virtual void Gen9() => Debug.Log($"{name} Gen9 �J�n�I");
    protected virtual void Gen10() => Debug.Log($"{name} Gen10 �J�n�I");

}
