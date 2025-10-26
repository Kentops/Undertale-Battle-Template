using UnityEngine;

public class KrisController : MonoBehaviour
{
    //0 = Start, 1 = idle, 2 = fight, 3 = act/spare, 4 = item, 5 = defend, 6 = hurt, 7 = defeated, 8 = exit
    private int _state = 1;
    public int state
    {
        get => _state;
        set
        {
            //When state is set, it will automatically update the animator's state
            myAnim.SetInteger("state", value);
            _state = myAnim.GetInteger("state"); //keep _state accurate
        }
    }
   
    private Animator myAnim;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }
}
