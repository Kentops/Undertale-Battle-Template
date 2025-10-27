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
        activeWave++;
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


        BattleBox.I.updateBoxState(0);
        //Send the next message
        KrisMenu.I.displayText(BattleLines.I.lines[BattleLines.I.currentMessage], false);
        BattleLines.I.currentMessage = (BattleLines.I.currentMessage + 1) % BattleLines.I.lines.Length;

        //Little delay
        yield return new WaitForSeconds(2);
        KrisMenu.I.changeState();
        Destroy(this.gameObject);
    }
}
