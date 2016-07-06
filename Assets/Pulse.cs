using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour
{
    private float timer = 0;
    private Vector3 scale;
    public float PulseFactor = .5f;
    public float PulseSpeed = 5f;

    void Start()
    {
        scale = transform.localScale;
    }

    void Update ()
	{
	    timer += Time.deltaTime * PulseSpeed;
        float v = Mathf.Sin(timer)/Mathf.PI * PulseFactor;
        transform.localScale = scale + new Vector3(v, v, v);
	}
}
