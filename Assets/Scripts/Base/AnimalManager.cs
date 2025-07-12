using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager Instance { get; private set; }
    public List<Animal> animalList = new();

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animalList.AddRange(GetComponentsInChildren<Animal>());
    }
}

