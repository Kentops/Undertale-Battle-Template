using System.Collections;
using UnityEngine;

public class WallBouncingBullet : Bullet
{
    //A bullet that bounces off of the walls
    [SerializeField] private float speed;
    [SerializeField] private Vector3 velocityDir;
    private bool firstEntry = true; //Allow bullet to start outside

    private void Start()
    {
        StartCoroutine(move());
        StartCoroutine(startup());
    }

    private IEnumerator move()
    {
        while(true) //While alive anyway
        {
            transform.position += velocityDir * speed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator startup()
    {
        //Allow bullet through walls
        yield return new WaitForSeconds(0.5f);
        firstEntry = false;
    }

    //Requires rigidbody
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Horizontal Wall") 
        {
            if (firstEntry == true) { return; }
            velocityDir = new Vector3(velocityDir.x, -1 * velocityDir.y,0);
        }
        else if(LayerMask.LayerToName(collision.gameObject.layer) == "Vertical Wall")
        {
            if (firstEntry == true) { return; }
            velocityDir = new Vector3(-1 * velocityDir.x, velocityDir.y, 0);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
