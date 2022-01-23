// Author: Fatima Nadeem

using UnityEngine;

[System.Serializable]
public class Level
{
    public Transform[] spawnPoints;
    public GameObject wholeLevel;

    int peakScore = 0;
    int stars = 0;

    public void UpdatePeakscore()
    {
        int score = ScoreManager.instance.InGameScore();

        if (score > peakScore)
        {
            peakScore = score;

            if (peakScore >= 15)
            {
                stars = 3;
            }
            else if (peakScore >= 10)
            {
                stars = 2;
            }
            else if (peakScore >= 5)
            {
                stars = 1;
            }
        }
    }
}
