using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static bool isIntroShowed;

    public static GameManager Instance { get; private set; }
    public PlayerStats Stats { get; private set; }
    public bool IsGameLost { get; private set; }

    private List<CollisionInfo> collisions;

    [SerializeField]
    private GameUI gameUI;
    [SerializeField]
    private BallSpawner spawner;

    [Header("Game settings")]
    [SerializeField]
    private int InitialLives;

    private void Start()
    {
        Instance = this;
        Stats = GetComponent<PlayerStats>();
        collisions = new List<CollisionInfo>();

        IsGameLost = false;
        Stats.Score = 0;
        Stats.Lives = InitialLives;

        if (!isIntroShowed) {
            gameUI.ShowIntroduceUI();
            isIntroShowed = true;
        }
    }

    private void TriggerGameOver()
    {
        if (!IsGameLost) {
            IsGameLost = true;

            var balls = FindObjectsOfType<Ball>();
            foreach (var b in balls) { 
                b.Die();
            }

            spawner.Finish();

            gameUI.ShowGameOverUI(Stats.Score, Stats.Record, Stats.IsNewRecord);
        }
    }

    public void RegisterCollision(int self, int other)
    {
        // First, check if "opponent" already registered the collision and remove it
        if (collisions.RemoveAll(c => c.SelfId == other && c.OtherId == self) > 0) {
            Stats.Score -= 2;
            Stats.Lives--;
        } else {
            collisions.Add(new CollisionInfo { SelfId = self, OtherId = other });
        }

        if (Stats.Lives == 0) { 
            TriggerGameOver();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
