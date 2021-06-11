using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowV2 : MonoBehaviour
{
	public Blocks func;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(func.validMove())
        {
        	transform.position += Vector3.down;
        }
    }
}
