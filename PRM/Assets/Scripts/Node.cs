using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Node : MonoBehaviour {

    private int _id;
    public string name;
    
    // dict com id e custo
    private Dictionary<int, float> neighbours;
    
    public Node(int id)
    {
        _id = id;
        name = "Node" + _id;
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
