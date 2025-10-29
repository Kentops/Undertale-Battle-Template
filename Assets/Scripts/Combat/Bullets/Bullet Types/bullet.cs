using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    public bool destoryOnContact; //Destroy bullet on contact with player

    [SerializeField] public float bulletLifespan;
    [SerializeField] public bool bulletHasLifespan;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //destorys the bullet after [bulletLifespan] seconds
        if (bulletHasLifespan) 
        {
            bulletLifespan -= Time.deltaTime;
            if(bulletLifespan <= 0) EndBulletLife();//could make a bullet play an animation on death or something with some parameters
        }

    }

    public virtual void EndBulletLife()
    {
        Destroy(gameObject);
    }

}
