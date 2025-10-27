using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyManager : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public int mercy = 0;

    private void Awake()
    {
        health = maxHealth;
    }

    //Shake him a little
    public IEnumerator takeDamage()
    {
        Vector3 original = transform.position;
        Vector3 desired1 = transform.position + (0.25f * Vector3.left);
        Vector3 desired2 = transform.position + (0.5f * Vector3.right);

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(blink(2));
        while(transform.position != desired1)
        {
            transform.position = Vector3.MoveTowards(transform.position, desired1, 3 * Time.deltaTime);
            yield return null;
        }
        while(transform.position != desired2)
        {
            transform.position = Vector3.MoveTowards(transform.position, desired2, 6 * Time.deltaTime);
            yield return null;
        }
        while(transform.position != original)
        {
            transform.position = Vector3.MoveTowards(transform.position, original, 3 * Time.deltaTime);
            yield return null;
        }
    }

    //Makes the enemy briefly transparent
    public IEnumerator blink(int blinks)
    {
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();

        for(int i = 0; i < blinks; i++)
        {
            while (myRenderer.color.a > 0.1f)
            {
                myRenderer.color = myRenderer.color - new Color(0, 0, 0, 8 * Time.deltaTime);
                yield return null;
            }
            while (myRenderer.color.a < 1f)
            {
                myRenderer.color = myRenderer.color + new Color(0, 0, 0, 8 * Time.deltaTime);
                yield return null;
            }
        }
    }

    //Makes the enemy disappear. Used for death and sparing
    public IEnumerator fadeAway()
    {
        SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
        while (myRenderer.color.a > 0)
        {
            myRenderer.color = myRenderer.color - new Color(0, 0, 0, 3 * Time.deltaTime);
            yield return null;
        }
    }

    //Shoves the enemy offscreen
    public IEnumerator spared()
    {
        StartCoroutine(fadeAway());
        Vector3 desired = transform.position + (2 * Vector3.right);
        while (transform.position != desired)
        {
            transform.position = Vector3.MoveTowards(transform.position, desired, 3 * Time.deltaTime);
            yield return null;
        }
    }
}
