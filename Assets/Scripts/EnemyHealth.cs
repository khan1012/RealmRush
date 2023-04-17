using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [Tooltip("Increase max hit points by this amount when enemy dies")]
    [SerializeField] int difficultyRamp = 1;
    int currentHitPoints = 0;
    Enemy enemy;

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
            maxHitPoints += difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
