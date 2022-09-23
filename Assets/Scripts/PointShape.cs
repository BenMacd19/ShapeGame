using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for controlling the behavior of a point shape
public class PointShape : MonoBehaviour
{
    // Calls "AddScore" on the GameManager when a collison with the player is detected.
    // Passes in the tag of this object (Sphere, Capsule).
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.AddScore(this.tag);
            Destroy(this.gameObject);
        }
    }
}
