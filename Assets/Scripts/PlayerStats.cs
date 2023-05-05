using UnityEditor;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private const string RecordPrefsKey = "Record";

    [SerializeField]
    private GameUI gameUI;

    private int lives;
    private int score;
    private int record;

    public int Score {
        get { return score; }
        set {
            score = value;
            gameUI.SetScoreText(value);
        }
    }

    public int Lives {
        get { return lives; }
        set
        {
            lives = value;
            gameUI.SetLivesText(value);
        }
    }

    public int Record => score > record ? score : record;

    public bool IsNewRecord => score > record;

    private void Start()
    {
        record = PlayerPrefs.GetInt(RecordPrefsKey);
    }

    private void OnDestroy()
    {
        if (IsNewRecord) {
            PlayerPrefs.SetInt(RecordPrefsKey, score);
            PlayerPrefs.Save();
        }
    }

    [MenuItem("Tools/ResetRecord")]
    public static void ResetRecord() {
        PlayerPrefs.SetInt(RecordPrefsKey, 0);
    }
}
