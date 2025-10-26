using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public int mercy = 0;

    private void Awake()
    {
        health = maxHealth;
    }
}
