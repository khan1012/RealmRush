using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int currentHitPoints = 0;

    Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHits();
    }

    void ProcessHits()
    {
        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            enemy.GiveGold();
            gameObject.SetActive(false);
        }
    }
}
