using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;

public class leaderboardController : MonoBehaviour
{
    public InputField memberId;
    public int ID;
    public inBetweenValues ibv;

    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;


    private void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("success");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(memberId.text, int.Parse(ibv.highScore.ToString()), ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("success2");
            }
            else
            {
                Debug.Log("fail2");
            }
        });
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(ID, 10, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    Debug.Log("looping");
                    tempPlayerNames += members[i].rank + ". ";
                    tempPlayerNames += members[i].member_id;
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void OnClick()
    {
        StartCoroutine(FetchTopHighscoresRoutine());
    }
}

