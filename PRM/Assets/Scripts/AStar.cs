using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour {

    private Node _start, _goal;
    List<int> path;
    private float distToGoal, distToNext;
    private float minimumFunction;
    private int idToPath;
    public AStar(Node start, Node goal)
    {
        _start = start;
        _goal = goal;
        path = new List<int>();
    }

    public List<int> FindPath(Node firstNode,Node lastNode, List<Node> prm)
    {
        minimumFunction = Mathf.Infinity;
        
        foreach (int id in firstNode.neighbours.Keys)
        {
            distToNext = firstNode.neighbours[id];//cost
            distToGoal = (prm[id].obj.transform.position - _goal.obj.transform.position).magnitude;//distance to goal
            var temp = distToGoal + distToNext;
            if (temp < minimumFunction)
            {
                minimumFunction = temp;
                idToPath = id;
            }
            Debug.Log("id: " + id);
        }

        Debug.Log("id to Path: " + idToPath);
        path.Add(idToPath);
        Debug.Log("Passei do add");
        if (prm[idToPath].obj == lastNode.obj)
        {
            return path;
        }
        else
            FindPath(prm[idToPath], lastNode, prm);

        return path;
    }
}
