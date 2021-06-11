using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
	// shadow of the blocks
    //public GameObject[] shadows;
    // shadow position
    //public Vector3 shadowPos;
    //
    GameObject parent;
    Blocks parentBlock;
    //
    public void setParent(GameObject _parent)
    {
    	parent = _parent;
    	parentBlock = parent.GetComponent<Blocks>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	while(parentBlock.enabled)
    	{
    		shadowPos();
    		// correct down move
    	}
    	Destroy(gameObject);
        
    }

    public void shadowPos()
    {
        transform.position = parentBlock.transform.position;//new Vector3(parentBlock.transform.position.x,0,parentBlock.transform.position.z);
        transform.rotation = parentBlock.transform.rotation;
    }
}
