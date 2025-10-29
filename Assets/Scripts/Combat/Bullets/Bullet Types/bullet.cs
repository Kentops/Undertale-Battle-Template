using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    public bool destoryOnContact; //Destroy bullet on contact with player
    public bool hasDeathAnim;
    public float bulletDeathAnimTime; // only matters if the bullet has a death animation

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
        if (!hasDeathAnim)
            Destroy(gameObject);
        else 
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
            StartCoroutine(DelayBulletDeath(bulletDeathAnimTime));
        }
    }

    private IEnumerator DelayBulletDeath(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
