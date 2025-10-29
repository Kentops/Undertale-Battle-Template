using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    public float time;
    public float speed;

    public Vector3 dir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {

        if (time>=0)
        {
            gameObject.transform.Translate(speed*dir*Time.deltaTime);
            time -= Time.deltaTime;
        }
    }
}
