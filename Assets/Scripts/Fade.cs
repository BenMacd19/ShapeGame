using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that moves an object in and out of the scene
public class Fade : MonoBehaviour
{
    private int _fadeDuration = 2;
    public int fadeDuration { get { return _fadeDuration; } private set { _fadeDuration = value; } }

    // Start is called before the first frame update
    void Start()
    {
        MoveDown();
    }

    // Moves the game object up
    public void MoveUp()
    {
        transform.LeanMoveLocal(transform.position + new Vector3(0, 10, 0), 2).setEaseInQuart();
    }

    // Moves the game object down
    public void MoveDown()
    {
        transform.LeanMoveLocal(transform.position - new Vector3(0, 10, 0), 2).setEaseOutQuart();
    }
}
