using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI lives;

    [Header("Game Over UI")]
    [SerializeField]
    private GameObject gameOverUIRoot;
    [SerializeField]
    private TextMeshProUGUI gameOverScore;
    [SerializeField]
    private TextMeshProUGUI record;
    [SerializeField]
    private GameObject newText;

    [Header("Introduce UI")]
    [SerializeField]
    private GameObject introduceUIRoot;

    private void Start()
    {
        gameOverUIRoot.SetActive(false);
    }

    public void SetScoreText(int score)
    {
        this.score.text = score.ToString();
    }

    public void SetLivesText(int lives)
    {
        this.lives.text = lives.ToString();
    }

    public void ShowIntroduceUI()
    { 
        introduceUIRoot.SetActive(true);
    }

    public void ShowGameOverUI(int score, int record, bool isNewRecord)
    {
        gameOverUIRoot.SetActive(true);
        gameOverScore.text = score.ToString();
        this.record.text = record.ToString();
        newText.SetActive(isNewRecord);
    }
}
