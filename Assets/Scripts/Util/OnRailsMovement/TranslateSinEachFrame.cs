using UnityEngine;

public class TranslateSinEachFrame : MonoBehaviour
{
    [SerializeField] Vector3 
        speed = new Vector3(),     //how fast
        intensity = new Vector3(), //how far out
        offset = new Vector3(),
        offsetRandom = new Vector3();

    [SerializeField] bool constrainPosition = false;
    Vector3 sin = new Vector3();



    void Start()
    {
        Vector3 randomise = new Vector3(
            Random.Range(0, offsetRandom.x),
            Random.Range(0, offsetRandom.y),
            Random.Range(0, offsetRandom.z)
            );
        offset += randomise;
        sin += offset;
    }

    void FixedUpdate()
    {
        sin += speed;

        Vector3 v = new Vector3(
            Mathf.Sin(sin.x) * intensity.x,
            Mathf.Sin(sin.y) * intensity.y,
            Mathf.Sin(sin.z) * intensity.z
            );

        if (constrainPosition)
        {
            transform.position = v;
        }
        else
        {
            transform.position += v;
        }
    }
}
