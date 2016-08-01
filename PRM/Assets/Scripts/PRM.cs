using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PRM : MonoBehaviour {

    public GameObject node;
    private List<GameObject> prmVertices;
    public int numOfNodes;
    void Start ()
    {
        prmVertices = new List<GameObject>();
        CreateRoadMap();
 	}
    void CreateRoadMap()
    {
        while(prmVertices.Count < numOfNodes)
        {
            print("Tamanho PRM " + prmVertices.Count);
            prmVertices.Add((GameObject)Instantiate(node, InsertVertex(), node.transform.rotation));
        }
    }
    Vector3 InsertVertex()
    {
        int x, z;
        x = Mathf.RoundToInt(Random.Range(-15.0f, 15.0f));
        z = Mathf.RoundToInt(Random.Range(-15.0f, 15.0f));
        return new Vector3(x, 0, z);
          
    }
	// Update is called once per frame
	void Update () {
	
	}
}
