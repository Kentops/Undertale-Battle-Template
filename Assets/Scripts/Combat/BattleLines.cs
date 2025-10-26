using UnityEngine;

public class BattleLines : MonoBehaviour
{
    //This class decides what message will pop up after each combat
    public static BattleLines I;
    public string[] lines;
    public int currentMessage = 0;

    private void Awake()
    {
        I = this;   
    }
}
