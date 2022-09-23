using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class that controls the flow of the game session
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int _level = 1;
    private int _score = 0;
    private int _shapesCollided = 0;

    [SerializeField] private UI gameUI;
    [SerializeField] private GameObject player;
    private ShapeSpawner shapeSpawner;
    private SaveSystem saveSystem;

    public int level { get { return _level; } private set { _level = value; } }
    public int score { get { return _score; } private set { _score = value; } }
    public int shapesCollided { get { return _shapesCollided; } private set { _shapesCollided = value; } }

    private void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {   
        shapeSpawner = GetComponent<ShapeSpawner>();
        shapeSpawner.StartCoroutine("SpawnObstacleOnGrid", 0);
        saveSystem = GetComponent<SaveSystem>();
        shapeSpawner.SpawnShapeOnGrid();
    }

    // Adds the correct score for the corrosponding shape and current level
    // Spawns a new shape after the old shape has been destroyed
    // Checks if the player has enough points to increase the game level
    public void AddScore(string shapeType)
    {
        switch (shapeType) 
        {
        case "Sphere":
            {
                if (level == 1) 
                { 
                    score += 1;
                }
                else
                {
                    score += level - 1 * 10;
                }
            }
            break;

        case "Capsule":
            {
                score += 2 + ((level - 1) * 10);
            }
            break;
        }
        shapeSpawner.SpawnShapeOnGrid();
        shapesCollided += 1;
        CheckScore();
    }

    // Removes score from the player
    public void RemoveScore()
    {
        if (score - 10 < 0)
        {
            score = 0;
        } else {
            score -= 10;
        } 
    }

    // Checks if the player has reached a multiple of 100
    // if so, the game level is increased
    private void CheckScore()
    {
        if (score > level * 100)
        {
            IncreaseLevel();
        }
    }

    // Increases the level of the game
    // If the player gets 400 points then the player wins
    private void IncreaseLevel()
    {
        if ((level + 1) > 3)
        {
            WinGame();
        } else {
            level += 1;
        }
    }

    // Saves the session stats
    // Disables the player
    // Fades in the game win screen 
    private void WinGame()
    {
        saveSystem.SaveToJson(System.DateTime.Now.ToString(), score, shapesCollided);
        player.GetComponent<PlayerMovement>().enabled = false;
        gameUI.FadeInGameWin();
    }

    // Saves the session stats
    // Disables the player
    // Fades in the game over screen
    public void GameOver()
    {
        saveSystem.SaveToJson(System.DateTime.Now.ToString(), score, shapesCollided);
        player.GetComponent<PlayerMovement>().enabled = false;
        gameUI.FadeInGameOver();
    }   

    // Reloads the game scene
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
