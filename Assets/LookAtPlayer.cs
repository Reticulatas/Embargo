using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	void FixedUpdate ()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0, 180.0f, 0);
    }
}
