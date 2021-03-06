﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Node {
    //get ID later
    public int id;
    public GameObject obj;
    
    // dict com id e custo
    public Dictionary<int, float> neighbours;
    
    public Node(int id, GameObject obj)
    {
        this.id = id;
        this.obj = obj;
        neighbours = new Dictionary<int, float>();
    }

    public void AddEdge(int id, float cost=0)
    {
        if (!(neighbours.ContainsKey(id)))
        {
//			Debug.Log ("Adicionei: " + id + " ao node " + this.id);
            neighbours.Add(id, cost);
        }
        else
        {
			Debug.Log("Vizinho já adicionado");
        }
    }
    public bool HasNode(int id)
    {
        Debug.Log("testando vizinhança entre " + this.id + " e " + id);
        Debug.Log(neighbours.ContainsKey(id));
        return neighbours.ContainsKey(id);
    }
}
