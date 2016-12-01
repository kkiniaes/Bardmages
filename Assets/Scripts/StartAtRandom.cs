using UnityEngine;
using System.Collections;

public class StartAtRandom : MonoBehaviour {

    public float startTime = -1f; 
    public float maxStartTime = 4f;
    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        if (startTime == -1f)
            startTime = Random.Range(0f, maxStartTime);
        Invoke("startanim", startTime);
	}
	
	// Update is called once per frame
	void startanim () {
        anim.SetTrigger("start");
	}
}
