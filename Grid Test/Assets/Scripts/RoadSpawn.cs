using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawn : MonoBehaviour {

    public GameObject roadChunk;
    public Dictionary<int, List<GameObject>> roadDict;

    public int block = 0;

    void Start()
    {
        roadDict = new Dictionary<int, List<GameObject>>();
    }

    public void InitialSpawn()
    {
        var cur = Instantiate(roadChunk);
        cur.GetComponent<GridSnapping>().enabled = true;
    }

    public void UpdateBlocks()
    {
        List<GameObject> curBlock = new List<GameObject>();

        var traces = GameObject.FindGameObjectsWithTag("Trace");
        foreach (GameObject g in traces)
        {
            var curTransform = g.transform;
            Destroy(g);
            var curRoadChunk = Instantiate(roadChunk, curTransform.position, curTransform.rotation, this.transform);
            StartCoroutine(DeleteIfColliding(curRoadChunk, block));
            curBlock.Add(curRoadChunk);
        }

        roadDict.Add(block++, curBlock);
    }

    IEnumerator DeleteIfColliding(GameObject curRoadChunk, int curBlock)
    {
        yield return new WaitForFixedUpdate();

        if (curRoadChunk.GetComponent<CollisionChecking>().isColliding)
        {
            roadDict[curBlock].Remove(curRoadChunk);
            Destroy(curRoadChunk);
        }



    }






}



