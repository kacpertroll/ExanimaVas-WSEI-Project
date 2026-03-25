using UnityEngine;

public class FlashlightMove : MonoBehaviour
{
    public Vector3 vectOffset;
    public GameObject goFollow;
    public float speed = 1.0f;

    void Start()
    {
        vectOffset = transform.position - goFollow.transform.position;
    }

    void Update()
    {
        transform.position = goFollow.transform.position + vectOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
    }

}
