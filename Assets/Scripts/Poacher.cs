using UnityEngine;

public class Poacher : MonoBehaviour
{
    public float speed = 2f;
    private Animal targetAnimal;

    float searchInterval = 0.5f;
    float searchTimer = 0f;

    void Start()
    {
        targetAnimal = FindNearestAnimal();
    }

    void Update()
    {
        searchTimer -= Time.deltaTime;
        if (searchTimer <= 0f)
        {
            searchTimer = searchInterval;
            targetAnimal = FindNearestAnimal();
        }

        Vector3 dir = (targetAnimal.transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    Animal FindNearestAnimal()
    {
        var animals = AnimalManager.Instance.animalList;
        Animal nearest = null;
        float minDist = float.MaxValue;

        foreach (var animal in animals)
        {
            float dist = Vector3.Distance(transform.position, animal.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = animal;
            }
        }

        return nearest;
    }
}
