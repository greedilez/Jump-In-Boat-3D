using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private Human human;

    private int score = 0;

    public int PlayerScore{ get => score; }

    private void Awake(){
        InitializeFields();
        LoadFromPP();
        SubscribeAddingScoreOnWin();
    }

    private void Update() => SyncTextAndScore();

    private void InitializeFields(){
        human = FindObjectOfType<Human>();
    }

    private void LoadFromPP() => score = PlayerPrefs.GetInt("Score", 0);

    private void SaveToPP() => PlayerPrefs.SetInt("Score", score);

    private protected void SubscribeAddingScoreOnWin(){
        human.onHumanWin += () => {
            score++;
            SaveToPP();
        };
    }

    private protected void SyncTextAndScore() => scoreText.text = $"Your score: {score}";
}
