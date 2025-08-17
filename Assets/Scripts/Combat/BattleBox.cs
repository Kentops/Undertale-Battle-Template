using UnityEngine;

public class BattleBox : MonoBehaviour
{
    public Animator myAnim;
    public int debugState;
    public int myState = 0; //0 = hidden, 1 = shown
    public static BattleBox I; //Singleton

    //Creates an event others can subscribe to
    public delegate void BoxAction();
    public static event BoxAction boxClose;
    public static event BoxAction boxOpen;

    private void Awake()
    {
        I = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateBoxState(int newState)
    {
        Debug.Log("updatingBoxState to "+newState);
        myAnim.SetInteger("State", newState);
        myState = newState;

        if(myState == 0)
        {
            if(boxClose == null) { return; }
            boxClose();
        }
        else
        {
            if(boxOpen == null) { return; }
            boxOpen();
        }
    }


}
