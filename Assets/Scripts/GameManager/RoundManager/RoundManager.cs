using System;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static event Action<int> OnWaveStart;

    public int nowWave { get; private set; } = 0;

    void Start()
    {
        WaveStart(1);
    }

    public void WaveStart(int waveNum)
    {
        nowWave = waveNum;
        OnWaveStart?.Invoke(waveNum);
    }

    public void NextWave()
    {
        WaveStart(nowWave + 1);
    }
}