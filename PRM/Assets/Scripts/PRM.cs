using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PRM : MonoBehaviour {
    
	private int numOfNodes;
    private List<Node> roadmap;

    public Transform plane;
    public GameObject nodePrefab;
    public int maxNodes;
    public GameObject edgePrefab;


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
          //  print("Numero de nodes: " + numOfNodes);
            StartCoroutine(InsertNode());
            yield return new WaitForSeconds(1.0f);
           // InsertNode();
        }
        // provavelmeente vai ter edge duplicado
        //depois tem que guardar as edges em alguma estrutura

        for (int i = 0; i < roadmap.Count; i++)
        {
            for (int j = 0; j < roadmap.Count; j++)
            {
                if (roadmap[i].id == roadmap[j].id)
                    continue;
                else
                {
                    if (!(Physics.Raycast(roadmap[i].obj.transform.position, roadmap[j].obj.transform.position)))
                    {
                        var edge = (GameObject)Instantiate(edgePrefab, edgePrefab.transform.position, edgePrefab.transform.rotation);
                        edge.GetComponent<LineRenderer>().SetPosition(0, roadmap[i].obj.transform.position);
                        edge.GetComponent<LineRenderer>().SetPosition(1, roadmap[j].obj.transform.position);
                        edge.name = "Edge" + roadmap[i].obj.name + roadmap[j].obj.name;
                    }
                }
               
                yield return new WaitForSeconds(1.0f);
            }

        }
        

        //PrintRoadMapElements();

    }   
    IEnumerator InsertNode()
    {
        int x, z;
		var maxPos = plane.localScale.x * 10;
		x = Mathf.RoundToInt (Random.Range (0, maxPos));
		z = Mathf.RoundToInt(Random.Range(0, maxPos));

        var nodeObj = (GameObject)Instantiate(nodePrefab, new Vector3(x, 0, z), nodePrefab.transform.rotation);
        nodeObj.name = "Node" + numOfNodes;
        yield return new WaitForSeconds(1.0f);
        
        if (nodeObj.GetComponent<NodeInfo>().collisionFree)
        {
            roadmap.Add(new Node(numOfNodes, nodeObj));
            numOfNodes++;
        }
        else
        {
            Destroy(nodeObj);
            print("nao deu pra criar");
        }
        yield return new WaitForSeconds(2.0f);

    }


    void PrintRoadMapElements()
    {
        print("nodes dentro do roadmap");
        foreach(Node n in roadmap)
        {
            print(n.obj.name);
        }
    }
}
