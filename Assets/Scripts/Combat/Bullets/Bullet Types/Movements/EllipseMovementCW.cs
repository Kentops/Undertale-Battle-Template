using UnityEngine;

public class EllipseMovementCW : MonoBehaviour
{
    public float radiusX;
    public float radiusY;

    public float timeToEllipse;
    public float timeOffset;
    
    private float time;
    private float helperValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = timeOffset;
        helperValue = 2 * Mathf.PI / timeToEllipse;
    }
    // Update is called once per frame
    void Update()
    {
        if (time<= 0) time = timeToEllipse;
        
        gameObject.transform.Translate( Time.deltaTime * helperValue * radiusX * -Mathf.Sin(time * helperValue )/2, Time.deltaTime * helperValue * radiusY * Mathf.Cos(time * helperValue)/2, 0);
        time -= Time.deltaTime;
    }
}
