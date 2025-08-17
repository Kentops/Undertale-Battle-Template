using UnityEngine;
using System.Collections;
public class Attack : MonoBehaviour
{
    /* To create a timeout between waves add an empty GameObject at the end of a wave and set it's waitTime to be whatever number that the timeout should be*/
    public GameObject[] waves;
    private int activeWave;

    void Start()
    {
        activeWave = -1;
    }

    public void SpawnNextWave() 
    {
        Debug.Log("SpawnNextWave method");
        activeWave++;
        if(activeWave<waves.Length)
        {
            waves[activeWave].GetComponent<BulletWave>().StartSpawningBullets(this.gameObject);
        }
        else EndAttack();
        

    }
    void EndAttack() 
    {
        //has to be implimented to pull up the menu again

    }
}
