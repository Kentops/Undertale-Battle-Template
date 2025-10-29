using System.Collections;
using UnityEngine;

public class SingleMovementBullet : Bullet
{
    //Over life span, go from starting position to target
    private Vector3 A;
    [SerializeField] private Vector3 B;
    private float maxLife;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletHasLifespan = true;
        maxLife = bulletLifespan;
        A = transform.position;
        StartCoroutine(co_MoveIt());
    }

    private IEnumerator co_MoveIt()
    {
        while(bulletLifespan > 0)
        {
            float ratio = bulletLifespan / maxLife;
            transform.position = (ratio * A) + ((1 - ratio) * B);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
