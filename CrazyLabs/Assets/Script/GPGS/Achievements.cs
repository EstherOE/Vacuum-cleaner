using System;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public static Achievements instance;

    private string chickenPickinLeaderBoard = "";


    // List of games achievements
    [Header("Completed Games")]
    public string complete_1_game;



    // hidden achievement
    public string hidden_achievement;

    // incremental achievement
    public string incremental_achievement;


    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // TODO? More Optimization...
        playedGamesAchievements();
    }




    // Played Games Achievements
    public void playedGamesAchievements()
    {
        // Get played games
        // var playedGames = GameManager.TotalGamesPlayed;
        var playedGames = 0;

        // Get Unclock Achievements
        // var unclock = GetComponent<PlayGames>();
        //unclock.UnlockAchievement(string ID, 100.00f);

        // switch to award played games | 10 achievements | // TODO! Add more awards...
        switch (playedGames)
        {
            case 1:
                UnlockAchievement(complete_1_game, 100.00f);
                break;
        }
    }

   



    // Unlock any achievement - by its ID and score
    public void UnlockAchievement(String achievementID, double scoreAmount)
    {
        Social.ReportProgress(achievementID, scoreAmount, success =>
        {
            if (success)
            {
                Debug.Log("Achievement Unlocked Successfully");
            }
            else
            {
                Debug.Log("Achievement not unlocked..!");
            }
        });
    }


    // Updating each player's Leaderboard Score
	public void UpdateLeaderboardScore()
	{
        // Get input from input field
		var leaderboardScoreValue = 20;
        // var leaderboardScoreValue = GameObject.Find("XPInputField").GetComponent<InputField>();
		// Get Score from Game Manager // ? TODO: Get Score...

        // print input value
        // Debug.Log("This is the Value : " + leaderboardScoreValue.text);


		Social.ReportScore(leaderboardScoreValue, chickenPickinLeaderBoard, (bool success) => {
			// handle success or failure
		});
	}

    // Show LeaderBoard
	public void ShowLeaderBoardBtn()
	{
		// Social.ShowLeaderboardUI(); // all Leaderboards

		PlayGamesPlatform.Instance.ShowLeaderboardUI(chickenPickinLeaderBoard); // specific Leaderboard
	}

	// Show Achievements
	public void ShowAchievementsBtn()
	{
		Social.ShowAchievementsUI();
	}
}