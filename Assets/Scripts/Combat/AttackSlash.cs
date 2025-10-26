using UnityEngine;

public class AttackSlash : MonoBehaviour
{
    public void onAnimEnd()
    {
        Destroy(this.gameObject);
    }
}
