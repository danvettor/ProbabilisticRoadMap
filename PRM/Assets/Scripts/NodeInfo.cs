using UnityEngine;
using System.Collections;

public class NodeInfo : MonoBehaviour {
    public bool collisionFree = true;
	void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Obstacle"))
        {
            collisionFree = false;
        }
    }
}
