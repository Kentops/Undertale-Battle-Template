using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KrisHealth : MonoBehaviour
{
    public static KrisHealth I;
    public int health;
    public int maxHealth;
    public bool isInvincible; //Are you sure?

    [SerializeField] private SpriteRenderer myRenderer;
    [SerializeField] private Sprite heart;
    [SerializeField] private AudioSource hurtSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        I = this;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator takeDamage(int damage)
    {
        isInvincible = true;
        health -= damage;
        KrisMenu.I.updateHealth();
        hurtSound.Play();
        if(health <= 0)
        {
            SceneManager.LoadScene("Game Over Scene");
        }


        for (int i = 0; i < 2; i++) //Flicker heart
        {
            myRenderer.sprite = null;
            yield return new WaitForSeconds(0.1f);
            myRenderer.sprite = heart;
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
    }

    //Collision is calculated by kris so that the bullets can have trigger colliders.
    //This means the soul can interact with them without bouncing off of the bullets.
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet theBullet = collision.GetComponent<Bullet>();
            if (isInvincible == false)
            {
                StartCoroutine("takeDamage", theBullet.bulletDamage);
                if (theBullet.destoryOnContact)
                {
                    theBullet.EndBulletLife();
                }
            }
        }
    }

    private void OnDisable()
    {
        isInvincible = false;
        myRenderer.sprite = heart;
    }
}
