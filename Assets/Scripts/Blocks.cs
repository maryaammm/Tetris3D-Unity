
// Tetris 3D
// note: cinemachine package is used for camera control

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blocks : MonoBehaviour
{
	//
	private float prevTime;
	public float fallTime = 0.5f;
	// dimension of the grid cube
	public static int Xgrid = 10;
    public static int Ygrid = 10;
	public static int Zgrid = 10;
	//
	public Vector3 rotPoiint;
	// save fallen blocks position
	public static Transform[,,] fillGrid = new Transform[Xgrid,Ygrid+5,Zgrid];
    // a game over flag
    private bool gameOver = false;
    bool keyPress = false;

    bool drop = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	// check movement of shadow
    	if(validMoveShadow())
    	{	
    		GameObject.FindWithTag("shadow").transform.position -= new Vector3(0,1,0);

    		if(!validMoveShadow())
	    	{
	    		GameObject.FindWithTag("shadow").transform.position += new Vector3(0,1,0);
	    	}
    	}

        // move the block down
        //    	
        if(Time.time - prevTime > fallTime)
        {
            transform.position += new Vector3(0,-1,0);

            if(!validMove())
            {  // undo the move
                transform.position -= new Vector3(0,-1,0);
                // add the block to grid
                addBlocks();
                drop = true;
                // check for line
                checkFullLine();
                // destroy the shadow
     			Destroy(GameObject.FindWithTag("shadow"));
                // disable and access spawn script
                this.enabled = false;
               	FindObjectOfType<Spawn>().spawnBlock();
                
            }
            prevTime = Time.time;
        }

    	//// move
     	// quick down movement
     	if(Input.GetKeyUp(KeyCode.Z))
     	{
     		transform.position += new Vector3(0,-1,0);
     		if(!validMove())
    		{ // if movement is not valid undo it
     			transform.position -= new Vector3(0,-1,0);
     		}
     	}
    	
     	// x-left
     	if(Input.GetKeyUp(KeyCode.LeftArrow))
     	{
     		transform.position += new Vector3(-1,0,0);
     		// move the shadow
     		GameObject.FindWithTag("shadow").transform.position += new Vector3(-1,0,0);
     		keyPress = true;
     		//FindObjectOfType<addShadow>().dropShadow();
     		//Instantiate(this.gameObject, new Vector3(5,0,5), Quaternion.identity);
     		if(!validMove())
    		{ // if movement is not valid undo it
     			transform.position -= new Vector3(-1,0,0);
     			// move the shadow
     			GameObject.FindWithTag("shadow").transform.position -= new Vector3(-1,0,0);
	     	}
	     	if(!validMoveShadow())
	    	{
	    		GameObject.FindWithTag("shadow").transform.position += new Vector3(0,1,0);
	    	}
     	}
     	// x-right
     	if(Input.GetKeyUp(KeyCode.RightArrow))
     	{
     		transform.position += new Vector3(1,0,0);
     		// move the shadow
     		GameObject.FindWithTag("shadow").transform.position += new Vector3(1,0,0);
     		keyPress = true;

     		if(!validMove())
    		{ // if movement is not valid undo it
     			transform.position -= new Vector3(1,0,0);
     			// move the shadow
     			GameObject.FindWithTag("shadow").transform.position -= new Vector3(1,0,0);
     		}
     		if(!validMoveShadow())
	    	{
	    		GameObject.FindWithTag("shadow").transform.position += new Vector3(0,1,0);
	    	}
     	}
     	// z-right
     	if(Input.GetKeyUp(KeyCode.DownArrow))
     	{
     		transform.position += new Vector3(0,0,-1);
     		// move the shadow
     		GameObject.FindWithTag("shadow").transform.position += new Vector3(0,0,-1);
     		keyPress = true;

     		if(!validMove())
    		{ // if movement is not valid undo it
     			transform.position -= new Vector3(0,0,-1);
     			// move the shadow
     			GameObject.FindWithTag("shadow").transform.position -= new Vector3(0,0,-1);
     		}
     		if(!validMoveShadow())
	    	{
	    		GameObject.FindWithTag("shadow").transform.position += new Vector3(0,1,0);
	    	}
     	}
     	// z-left
     	if(Input.GetKeyUp(KeyCode.UpArrow))
     	{
     		transform.position += new Vector3(0,0,1);
     		// move the shadow
     		GameObject.FindWithTag("shadow").transform.position += new Vector3(0,0,1);
     		keyPress = true;

     		if(!validMove())
    		{ // if movement is not valid undo it
     			transform.position -= new Vector3(0,0,1);
     			// move the shadow
     			GameObject.FindWithTag("shadow").transform.position -= new Vector3(0,0,1);
     		}
     		if(!validMoveShadow())
	    	{
	    		GameObject.FindWithTag("shadow").transform.position += new Vector3(0,1,0);
	    	}
     	}

     	//// rotate
     	// A,S,D -> rotate in x,y,z axis
     	// x
     	if(Input.GetKeyUp(KeyCode.A))
     	{ //
	     	transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(1,0,0), 90);
	     	// rotate shadow
	     	GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), 
	     															new Vector3(1,0,0), 90);
	     	
	     	//Debug.Log(GameObject.FindWithTag("shadow").transform.position.y);
	     	if(!validMove())
	     	{ // rotate back if it is not a valid move
	     		transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(1,0,0), -90);
	     		// rotate shadow
	     		GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(1,0,0), -90);
	     	}

            // if shadow is below y-axis move +1 up
            if(!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0,1,0);
            }
	    }
	    // y
	    if(Input.GetKeyUp(KeyCode.S))
     	{
	     	transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0,1,0), 90);
	     	// rotate shadow
	     	GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0,1,0), 90);

	     	if(!validMove())
	     	{ // rotate back if it is not a valid move
	     		transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0,1,0), -90); 
	     		// rotate shadow
	     		GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0,1,0), -90);
	     	}
            // if shadow is below y-axis move +1 up
            if(!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0,1,0);
            }
	    }
	    // z
     	if(Input.GetKeyUp(KeyCode.D))
     	{
	     	transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0,0,1), 90);
	     	// rotate shadow
	     	GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0,0,1), 90);

	     	if(!validMove())
	     	{ // rotate back if it is not a valid move
	     		transform.RotateAround(transform.TransformPoint(rotPoiint), new Vector3(0,0,1), -90);
	     		// rotate shadow
	     		GameObject.FindWithTag("shadow").transform.RotateAround(GameObject.FindWithTag("shadow").transform.TransformPoint(rotPoiint), new Vector3(0,0,1), -90);
	     	}
            // if shadow is below y-axis move +1 up
            if(!validMoveShadow())
            {
                GameObject.FindWithTag("shadow").transform.position += new Vector3(0,1,0);
            }
	    }

        // check for game over
        if (gameOver)
        { // load the game over scene
            SceneManager.LoadScene("GameOver");
        }
    }

    // define a function to check whether the movement is valid
    public bool validMove()
    {
    	foreach (Transform children in transform)
    	{
    		// get x,y,z position as int
    		// for all childrens - in this case cubes
    		int Xpos = Mathf.RoundToInt(children.transform.position.x);
            int Ypos = Mathf.RoundToInt(children.transform.position.y);
    		int Zpos = Mathf.RoundToInt(children.transform.position.z);

      		// if any exceed the bouder limit return false
    		if( Xpos < 0 || Xpos >= Xgrid ||
    			Ypos < 0 || 
    			Zpos < 0 || Zpos >= Zgrid )
    		{
    			return false;
    		}
            // check if position already taken
            if (fillGrid[Xpos,Ypos,Zpos] != null)
            {
                return false;
            }
    	}
    	return true;
    } 

    // change it to whether reach the end of fall on top of other block
    bool reachBottom()
    {
    	foreach (Transform children in transform)
    	{
    		// get y position as int
    		// for all childrens - in this case cubes
    		int Ypos = Mathf.RoundToInt(children.transform.position.y);
    		// if any exceed the bouder limit return false
    		if( Ypos <= 0 )
    		{
    			return false;
    		}
    	}
    	return true;
    }

    // keep track of fallen blocks
    void addBlocks()
    {
        foreach (Transform children in transform)
        {
            // get x,y,z position as int
            // for all childrens - in this case cubes
            int Xpos = Mathf.RoundToInt(children.transform.position.x);
            int Ypos = Mathf.RoundToInt(children.transform.position.y);
            int Zpos = Mathf.RoundToInt(children.transform.position.z);

            fillGrid[Xpos,Ypos,Zpos] = children;

            // check for game over
            if (fillGrid[Xpos,10,Zpos] != null)
            {
                gameOver = true;
                Debug.Log("game over");
            }
        }

    }

    // after a block landed check for full line to delete
    void checkFullLine()
    {
        for(int y = 0; y < Ygrid; y++)
        {
            checkPlane(y);
        }
    }

    // check for full lines in xz plane
    void checkPlane(int y)
    {
        for(int x = 0; x < Xgrid; x++)
        { 
            // check x lines 
            if(isLineX(x,y))
            { // if there is a line
                for(int z = 0; z < Zgrid; z++)
                {
                    Destroy(fillGrid[x,y,z].gameObject);               
                    fillGrid[x,y,z] = null;
                }
                //Debug.Log("x:");
                //Debug.Log(x);
                //Debug.Log("y:");
                //Debug.Log(y);
    
                lineDownX(x,y);
            }
        }
        for(int z = 0; z < Zgrid; z++)
        { 
            // check z lines 
            if(isLineZ(y,z))
            { // if there is a line
                for(int x = 0; x < Xgrid; x++)
                {
                    Destroy(fillGrid[x,y,z].gameObject);
                    fillGrid[x,y,z] = null;
                }
                //Debug.Log("z:");
                //Debug.Log(z);
                //Debug.Log("y:");
                //Debug.Log(y);
                lineDownZ(z,y);
            }
        }
    }

    // check for line in x-axis
    bool isLineX(int x, int y)
    {
        for(int z =0; z < Zgrid; z++)
        {
            if(fillGrid[x,y,z] == null)
            { // if only one block is empty return flase
                return false;
            }
        }
        return true;
    }

    // check for line in z-axis
    bool isLineZ(int y, int z)
    { // indexing different from isLineX
        bool isLine;
        for(int x =0; x < Xgrid; x++)
        {
            if(fillGrid[x,y,z] == null)
            { // if only one block is empty return flase
                return false;
            }
        }
        return true;
    }

    void lineDownX(int x, int j)
    {
        for (int y = j; y < 10; y++)
        {
            for (int z=0; z < Zgrid; z++)
            {
                if(fillGrid[x,y,z] != null)
                {
                    fillGrid[x,y-1,z] = fillGrid[x,y,z];
                    fillGrid[x,y,z] = null;
                    fillGrid[x,y-1,z].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }

    void lineDownZ(int z, int j)
    {
        for (int y = j; y < 10; y++)
        {
            for (int x=0; x < Xgrid; x++)
            {
                if(fillGrid[x,y,z] != null)
                {
                    fillGrid[x,y-1,z] = fillGrid[x,y,z];
                    fillGrid[x,y,z] = null;
                    fillGrid[x,y-1,z].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }

    // check for valid shadow placement

    bool validMoveShadow()
    {
    	foreach (Transform children in GameObject.FindWithTag("shadow").transform)
    	{
    		int Xpos = Mathf.RoundToInt(children.transform.position.x);
            int Ypos = Mathf.RoundToInt(children.transform.position.y);
            int Zpos = Mathf.RoundToInt(children.transform.position.z);

            if(Ypos < 0)
            {
            	return false;
            }
	    	if (fillGrid[Xpos,Ypos,Zpos] != null)
	            {
	                return false;
	            }

	        // deal with shadow in button but block are over it
	        for(int i=Ypos; i<10; i++)
	        {
	        	if(fillGrid[Xpos,i,Zpos] != null)
	        		GameObject.FindWithTag("shadow").transform.position = new Vector3(GameObject.FindWithTag("shadow").transform.position.x,
	        																			i+1,
	        																			GameObject.FindWithTag("shadow").transform.position.z);
	        }

	    }
	    return true;
    }

}
