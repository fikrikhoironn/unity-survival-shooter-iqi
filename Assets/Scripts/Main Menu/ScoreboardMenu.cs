using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreboardMenu : MonoBehaviour
{
    public GameObject scorePrefab;
    public Transform scoreParent;

    void OnEnable()
    {
        // Clear the scoreboard
        foreach (Transform child in scoreParent)
        {
            Destroy(child.gameObject);
        }

        // Get the scores from the player prefs
        var scores = DataManager.instance.scores;

        // Sort the scores
        Array.Sort(scores, (x, y) => TimeSpan.Parse(x.time).CompareTo(TimeSpan.Parse(y.time)));

        // Add the scores to the scoreboard
        foreach (var score in scores)
        {
            var scoreObject = Instantiate(scorePrefab, scoreParent);
            scoreObject.GetComponent<Score>().setText(score.name, TimeSpan.Parse(score.time));
        }
    }
}
