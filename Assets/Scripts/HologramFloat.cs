using UnityEngine;

public class HologramFloat : MonoBehaviour
{
    public float amplitude = 0.15f;
    public float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPos + Vector3.up * y;
    }
}
