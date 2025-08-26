using UnityEngine;
using System.Collections;

public class BulletWave : MonoBehaviour
{
    /* The first bullet should always take a little time to appear to let the battle box animation play*/
    public float[] timeWaitedBeforeBulletSpawns;
    public GameObject[] bullets;
    public Vector3[] bulletSpawnLocations;
    public float timeAfterLastBullet; //How long until the next wave is spawned? If last wave, how long until combat ends?

    private Attack attackThisWaveBelongsTo;
    private int bulletIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawningBullets(Attack myAttack) 
    {
        Debug.Log("Start Spawning Bullets Method");
        bulletIndex = 0;
        attackThisWaveBelongsTo = myAttack;
        StartCoroutine("bulletSpawner");
    }

    private IEnumerator bulletSpawner()
    {
        //it may be good to also impliment custom rotation in the same way or with additional logic.
        while (bulletIndex < bullets.Length)
        {
        yield return new WaitForSeconds(timeWaitedBeforeBulletSpawns[bulletIndex]);
        GameObject newBullet = Instantiate(bullets[bulletIndex], bulletSpawnLocations[bulletIndex], gameObject.transform.rotation);
        newBullet.transform.parent = transform;
        bulletIndex++;
        }

        yield return new WaitForSeconds(timeAfterLastBullet); 
        attackThisWaveBelongsTo.SpawnNextWave();
    }


}
