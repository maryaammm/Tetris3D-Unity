using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // create a block array to add out blocks to it
    // blocks already saved in prefabs
    [Header("Blocks")]
    public GameObject[] blocks;
    // shadow of the blocks
    public GameObject[] shadows;
    // spawn & shadow position
    public Vector3 spawnPos;
    public Vector3 shadowPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnBlock();
    }

    // spawn one peice from block list at the top
    public void spawnBlock()
    {
        // choose a random block and spawn it
        int randomBlock = Random.Range(0,blocks.Length);
        //Instantiate(blocks[randomBlock], spawnPos, Quaternion.identity);
        GameObject block = Instantiate (blocks[randomBlock], spawnPos, Quaternion.identity) as GameObject;
        // generate a shadow gameobject
        GameObject shadowBlock = Instantiate(shadows[randomBlock], shadowPos, Quaternion.identity) as GameObject;
    }
}
