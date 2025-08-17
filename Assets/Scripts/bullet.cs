using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum ColliderType {Box, Circle, Polygon}
    public ColliderType colliderType;
    private Collider2D bulletCollider;
    
    public int bulletDamage;
    public int minBulletDamage;

    public float bulletLifespan;
    public bool bulletHasLifespan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(colliderType == ColliderType.Box) bulletCollider = gameObject.GetComponent<BoxCollider2D>();
        if(colliderType == ColliderType.Circle) bulletCollider = gameObject.GetComponent<CircleCollider2D>();
        if(colliderType == ColliderType.Polygon) bulletCollider = gameObject.GetComponent<PolygonCollider2D>();


    }

    // Update is called once per frame
    void Update()
    {
        //destorys the bullet after [bulletLifespan] seconds
        if (bulletHasLifespan) 
        {
            bulletLifespan -= Time.deltaTime;
            if(bulletLifespan<=0) EndBulletLife();//could make a bullet play an animation on death or something with some parameters
        }

    }

    void EndBulletLife()
    {
        Destroy(gameObject);
    }
}
