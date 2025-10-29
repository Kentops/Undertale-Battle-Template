using System.Collections;
using UnityEngine;

public class warningBullet : Bullet
{
    //A bullet that fades into existence

    private SpriteRenderer myRenderer;
    private Collider2D myCollider;

    [SerializeField] private float appearSpeed;


    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        myRenderer.color = Color.gray;
        myRenderer.color -= new Color(0, 0, 0, 1);
        myRenderer.color += new Color(0, 0, 0, 0.1f); //Alpha will now be 0.1
        myCollider.enabled = false;
        StartCoroutine(Co_ShowUp());
    }

    private IEnumerator Co_ShowUp()
    {
        while(myRenderer.color.a < 0.99f)
        {
            float increment = appearSpeed * Time.deltaTime;
            myRenderer.color += new Color(0, 0, 0, increment);
            yield return null;
        }
        myRenderer.color = Color.white;
        myCollider.enabled = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
