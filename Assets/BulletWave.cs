using UnityEngine;
using System.Collections;

public class BulletWave : MonoBehaviour
{
    /* The first bullet should always take a little time to appear to let the battle box animation play*/
    public float[] timeWaitedBeforeBulletSpawns;
    public GameObject[] bullets;
    public Vector3[] bulletSpawnLocations;

    private GameObject attackThisWaveBelongsToo;
    private int bulletIndex;

    private IEnumerator waveCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawningBullets(GameObject attack) 
    {
        Debug.Log("Start Spawning Bullets Method");
        bulletIndex = 0;
        waveCoroutine = bulletSpawner();
        attackThisWaveBelongsToo = attack;
        StartCoroutine(waveCoroutine);
    }

    private IEnumerator bulletSpawner()
    {
        //it may be good to also impliment custom rotation in the same way or with additional logic.
        while (bulletIndex < bullets.Length)
        {
        yield return new WaitForSeconds(timeWaitedBeforeBulletSpawns[bulletIndex]);
        Instantiate(bullets[bulletIndex], bulletSpawnLocations[bulletIndex], gameObject.transform.rotation);
        Debug.Log("bullet instantiated?");
        bulletIndex++;
        }
        
        attackThisWaveBelongsToo.GetComponent<Attack>().SpawnNextWave();
    }


}
