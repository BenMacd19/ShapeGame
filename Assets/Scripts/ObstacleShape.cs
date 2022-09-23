using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for controlling obstacle shape behavior
public class ObstacleShape : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    [SerializeField] private int obstacleDuration = 15;

    private Fade fade;
    private MeshRenderer meshRenderer;

    void Awake() {
        fade = GetComponent<Fade>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start() 
    {
        // Sets the material of the obstacle based on the current level
        meshRenderer.material = materials[GameManager.instance.level - 1];
        fade.MoveDown();
        StartCoroutine("RemoveObstacle", 0);
    }

    // Removes the obstacle from the scene after a set amount of time
    private IEnumerator RemoveObstacle()
    {
        yield return new WaitForSeconds(obstacleDuration);
        fade.MoveUp();
        yield return new WaitForSeconds(fade.fadeDuration);
        Destroy(this.gameObject);
    }

    // Calls "AddScore" on the GameManager when a collison with the player is detected.
    // Passes in the tag of this object (Sphere, Capsule).
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.RemoveScore();
        }
    }
}
