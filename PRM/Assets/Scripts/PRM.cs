using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PRM : MonoBehaviour {
    public Transform plane;


    private int numOfNodes;
    private List<Node> roadmap;
    
    public GameObject nodePrefab;
    public GameObject edgePrefab;

    public int maxNodes;
    public int maxEdges;

    private Node lastNode, firstNode;
    private Node start, goal;
    public GameObject startObj, goalObj;

    private AStar planner;


    void Start ()
    {
        numOfNodes = 0;
        StartCoroutine(CreateRoadMap());
        roadmap = new List<Node>();
        start = new Node(-1, startObj);
        goal = new Node(-1, goalObj);

        planner = new AStar(start, goal);

       
 	}
    IEnumerator CreateRoadMap()
    {
        while(numOfNodes < maxNodes)
        {
            StartCoroutine(InsertNode());
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(0.5f);

        //insercao do Start e Goal para o desenho
        float minStartDist, minGoalDist;
        minStartDist = minGoalDist = Mathf.Infinity;
        foreach(Node n in roadmap)
        {
            var dist = (start.obj.transform.position - n.obj.transform.position).magnitude;
            if (dist < minStartDist)
            {
                minStartDist = dist;
                firstNode = n;
            }
            dist = (goal.obj.transform.position - n.obj.transform.position).magnitude;
            if (dist < minGoalDist)
            {
                minGoalDist = dist;
                lastNode = n;
            }
        }


        var edge = (GameObject)Instantiate(edgePrefab, edgePrefab.transform.position, edgePrefab.transform.rotation);
        edge.GetComponent<LineRenderer>().SetPosition(0, start.obj.transform.position);
        edge.GetComponent<LineRenderer>().SetPosition(1,firstNode.obj.transform.position);
        edge.name = "EdgeStart";


        edge = (GameObject)Instantiate(edgePrefab, edgePrefab.transform.position, edgePrefab.transform.rotation);
        edge.GetComponent<LineRenderer>().SetPosition(0, goal.obj.transform.position);
        edge.GetComponent<LineRenderer>().SetPosition(1, lastNode.obj.transform.position);
        edge.name = "EdgeGoal";

        

        StartCoroutine(InsertEdges());

        yield return new WaitForSeconds(5.0f);

        var path = planner.FindPath(firstNode, lastNode, roadmap);
        Debug.Log("============================");
        foreach (int id in path)
        {
            Debug.Log(id);
        }
            	 
        //PrintRoadMapElements();
    }

    IEnumerator InsertEdges()
    {
       for (int i = 0; i < roadmap.Count; i++)
        {
            var currentEdges = 0;
            for (int j = 0; j < roadmap.Count; j++)
            {

                if (roadmap[i].id == roadmap[j].id)
                    continue;
                else if (currentEdges > maxEdges)
                {
                    break;
                }
                else
                {
                    RaycastHit ray;
                    var dir = roadmap[j].obj.transform.position - roadmap[i].obj.transform.position;
                    if (Physics.Raycast(roadmap[i].obj.transform.position, dir, out ray))
                    {
                        if (!(ray.transform.CompareTag("Obstacle")) && !(roadmap[j].HasNode(roadmap[i].id)))
                        {
                            roadmap[i].AddEdge(roadmap[j].id, (roadmap[i].obj.transform.position - roadmap[j].obj.transform.position).magnitude);
                            currentEdges++;

                            var edge = (GameObject)Instantiate(edgePrefab, edgePrefab.transform.position, edgePrefab.transform.rotation);
                            edge.GetComponent<LineRenderer>().SetPosition(0, roadmap[i].obj.transform.position);
                            edge.GetComponent<LineRenderer>().SetPosition(1, roadmap[j].obj.transform.position);
                            edge.name = "Edge" + roadmap[i].obj.name + roadmap[j].obj.name;
                        }
                        yield return new WaitForSeconds(1.0f);
                    }
                    yield return new WaitForSeconds(1.0f);
                }
            }
        }
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
