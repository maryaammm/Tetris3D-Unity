using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addShadow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        dropShadow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropShadow()
    {
    	//Instantiate(this.gameObject, new Vector3(5,5,5), Quaternion.identity);
    	GameObject shadow = Instantiate (this.gameObject, new Vector3(5,0,5), Quaternion.identity) as GameObject;
    }
}
