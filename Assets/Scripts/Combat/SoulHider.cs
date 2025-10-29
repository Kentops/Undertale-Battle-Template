using UnityEngine;

public class SoulHider : MonoBehaviour
{
    //The purpose of this class is to disable the soul while the battle box is closed
    //and enable it when the box is open
    public GameObject theSoul;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        BattleBox.boxOpen += onOpen;
        BattleBox.boxClose += onClose;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onOpen()
    {
        theSoul.SetActive(true);
        theSoul.transform.position = new Vector3(-23, 0.69f, 0);
    }

    private void onClose()
    {
        theSoul.SetActive(false);
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject go in gos) go.GetComponent<Bullet>().EndBulletLife();
    }

    private void OnDestroy()
    {
        BattleBox.boxOpen -= onOpen;
        BattleBox.boxClose -= onClose;
    }
}
