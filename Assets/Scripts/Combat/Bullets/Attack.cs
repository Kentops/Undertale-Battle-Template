using UnityEngine;
using System.Collections;
public class Attack : MonoBehaviour
{
    /* To create a timeout between waves add an empty GameObject at the end of a wave and set it's waitTime to be whatever number that the timeout should be*/
    public GameObject[] waves;
    private int activeWave = -1;

    private GameObject currentWave;

    public void SpawnNextWave() 
    {
        Debug.Log("SpawnNextWave method");
        Debug.Log(activeWave);
        activeWave++;
        Debug.Log(name);
        if (activeWave < waves.Length)
        {
            currentWave = Instantiate(waves[activeWave], transform);
            currentWave.GetComponent<BulletWave>().StartSpawningBullets(this);
        }
        else { StartCoroutine("EndAttack"); }
        

    }
    IEnumerator EndAttack() 
    {
        Destroy(currentWave);

        //Little delay
        BattleBox.I.updateBoxState(0);
        yield return new WaitForSeconds(1);
        KrisMenu.I.changeState();
        Destroy(this.gameObject);
    }
}
