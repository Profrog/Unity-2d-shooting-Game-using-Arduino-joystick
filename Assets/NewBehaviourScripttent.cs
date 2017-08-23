using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScripttent : MonoBehaviour {


    private void OnDisable()
    {
                  
    }


    // Use this for initialization
    void Start () {
		
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnTriggerStay(Collider coll)
    {

        Debug.Log("tent" + coll.gameObject.tag);
        if (coll.gameObject.tag == "Player")
        {
      
            var someScript2 = coll.transform.GetComponent<NewBehaviourScriptforHim2>();

            bool check = someScript2.okay;
            Debug.Log("tent2" + check);
            //bool check = true;
            if (check == true)
            {
                //Debug.Log("check" + check);
                goingTent();
             
            }
        }

        else
        {
            Debug.Log("tent3" + coll.tag);
        }

      }

    public void goingTent()
    {
        OnDisable();
        SceneManager.LoadScene("aa");
  
    }

}
