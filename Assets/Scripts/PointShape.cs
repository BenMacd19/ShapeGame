using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for controlling the behavior of a point shape
public class PointShape : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.AddScore(this.tag);
            Destroy(this.gameObject);
        }
    }
}
