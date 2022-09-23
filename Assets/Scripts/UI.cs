using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Class for controlling the UI flow of the game
public class UI : MonoBehaviour
{  
    [SerializeField] private CanvasGroup gameOverScreen;
    [SerializeField] private CanvasGroup gameWinScreen;
    [SerializeField] private CanvasGroup blackScreen;

    [SerializeField] private TextMeshPro level;
    [SerializeField] private TextMeshPro score;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        FadeInUI();
    }

    // Update is called once per frame
    void Update()
    {
        level.text = "Level: " + gameManager.level.ToString();
        score.text = "Score: " + gameManager.score.ToString();
    }

    public void FadeInUI()
    {
        blackScreen.LeanAlpha(0f, 2f).setEaseOutQuad().setOnComplete(DeactivateBlack);
    }

    private void DeactivateBlack()
    {
        blackScreen.gameObject.SetActive(false);
    }

    public void FadeInGameWin()
    {
        gameWinScreen.gameObject.SetActive(true);
        blackScreen.gameObject.SetActive(true);
        blackScreen.LeanAlpha(1f, 2f).setEaseOutQuad();
        gameWinScreen.LeanAlpha(1f, 2f).setEaseOutQuad();
    }

    public void FadeInGameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        blackScreen.gameObject.SetActive(true);
        blackScreen.LeanAlpha(1f, 2f).setEaseOutQuad();
        gameOverScreen.LeanAlpha(1f, 2f).setEaseOutQuad();
    }
}
