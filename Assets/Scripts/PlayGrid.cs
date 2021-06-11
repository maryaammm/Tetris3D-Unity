using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : MonoBehaviour
{
	// declare grid planes
	public GameObject BottomPlane;
	public GameObject Face01, Face02, Face03, Face04;

	// grid size
	public int Xsize, Ysize, Zsize;
	// define a grid array
	public Transform[,,] mainGrid;

	// use Awake function to only represent playfeild
	//
	void OnDrawGizmos()
	{
		
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
