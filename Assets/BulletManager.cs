using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject[] Attacks;

    public int nextAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BattleBox.boxOpen += BeginNextAttack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginNextAttack() //can add checks here later if the bossfight has different phases and we need to stay between a few attacks
    {
        if(!(nextAttack<Attacks.Length))
        {
            nextAttack=0;
        }
        Debug.Log("Begin Next Attack method");
        Attacks[nextAttack].GetComponent<Attack>().SpawnNextWave();
        nextAttack++;
    }
}
