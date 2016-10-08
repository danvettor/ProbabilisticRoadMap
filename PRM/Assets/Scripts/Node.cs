using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Node : MonoBehaviour {
    //get ID later
    public int id;
    public GameObject obj;
    
    // dict com id e custo
    private Dictionary<int, float> neighbours;
    
    public Node(int id, GameObject obj)
    {
        this.id = id;
        this.obj = obj;
        neighbours = new Dictionary<int, float>();
    }

    public void AddEdge(int id, float cost)
    {
        if (!(neighbours.ContainsKey(id)))
        {
            neighbours.Add(id, cost);
        }
        else
        {
            print("Vizinho já adicionado");
        }
    }
}
