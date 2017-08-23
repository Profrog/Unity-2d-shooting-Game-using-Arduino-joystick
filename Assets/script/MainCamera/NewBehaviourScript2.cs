using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour {


    public AudioClip sndEXP3;

    // Use this for initialization
    void Start () {

        AudioSource.PlayClipAtPoint(sndEXP3, gameObject.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
