using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BlockGuyIdleManager : MonoBehaviour
{

    public Animator anim;
    public bool Blink = true;
    public bool FaceCamera = true;

	void Start ()
	{
	    anim = this.GetComponent<Animator>();
	}
	
	void FixedUpdate ()
	{
	    if (DayController.get().Day == 8)
	    {
            anim.Stop();
	        this.GetComponent<AnimatedTexture>().uX = 2.1f;
	        return;
	    }

	    if (Blink && Random.value < 0.002f && !anim.GetCurrentAnimatorStateInfo(0).IsName("BlockGuy_Blink"))
	    {
	        anim.Play("BlockGuy_Blink");
	    }

	    var d = Vector3.Dot(this.transform.forward, Camera.main.transform.forward);
        if (d < 0.5f && FaceCamera)
	    {
	        // face camera
	        var v = this.transform.position - Camera.main.transform.position;
	        v.y = 0;
	        transform.rotation = Quaternion.RotateTowards(transform.rotation,
	            Quaternion.LookRotation(v, Vector3.up), Time.deltaTime * 200.0f);
	    }
	}
}
