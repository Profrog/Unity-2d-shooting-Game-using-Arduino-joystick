using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


public class bulletInformation : MonoBehaviour {

    public float damage = 1;
    private Stopwatch sw = new Stopwatch();

    // Use this for initialization
    void Start () {
        sw.Start();
	}
	
	// Update is called once per frame
	void Update () {

        if (sw.ElapsedMilliseconds >= 100)
        {
            sw.Reset();
            sw.Start();
            //shootchecking = true;
            damage += 0.01f;
        }

    }
}
