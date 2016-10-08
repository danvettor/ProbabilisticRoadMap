using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PRM : MonoBehaviour {

    private GameObject node;
	private int numOfNodes;
    private List<Node> roadmap;

    public Transform plane;
    public GameObject nodePrefab;
    public int maxNodes;


    void Start ()
    {
        numOfNodes = 0;
        StartCoroutine(CreateRoadMap());
        roadmap = new List<Node>();

 	}
    IEnumerator CreateRoadMap()
    {
        while(numOfNodes < maxNodes)
        {
            print("Numero de nodes: " + numOfNodes);
            StartCoroutine(InsertNode());
           // InsertNode();
            yield return new WaitForSeconds(1.0f);
        }
        PrintRoadMapElements();

    }   
    IEnumerator InsertNode()
    {
        int x, z;
		var maxPos = plane.localScale.x * 10;
		x = Mathf.RoundToInt (Random.Range (0, maxPos));
		z = Mathf.RoundToInt(Random.Range(0, maxPos));

        node = (GameObject)Instantiate(nodePrefab, new Vector3(x, 0, z), nodePrefab.transform.rotation);
        node.name = "Node " + numOfNodes;
        yield return new WaitForSeconds(1.0f);
        if (node.GetComponent<NodeInfo>().collisionFree)
        {
            roadmap.Add(new Node(numOfNodes));
            numOfNodes++;
        }
        else
        {
            Destroy(node);
        }
    }


    void PrintRoadMapElements()
    {
        foreach(Node n in roadmap)
        {
            print(n.name);
        }
    }
}
