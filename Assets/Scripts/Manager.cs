using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static public Manager instance;

    int currentLevelNum; // Current level number
     
    public Level[] levels;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        currentLevelNum = 3;
        LoadLevel(3);
    }

    public void Freeze()
    {
        Time.timeScale = 0;
        
        Spawner.instance.Freeze();

        GameObject[] ingredients = GameObject.FindGameObjectsWithTag
            ("Incoming Ingredient");
        foreach (GameObject ingredient in ingredients)
        {
            ingredient.GetComponent<Ingredient>().Freeze();
        }
    }

    public void Unfreeze()
    {
        Time.timeScale = 1;

        Spawner.instance.Unfreeze();

        GameObject[] ingredients = GameObject.FindGameObjectsWithTag
            ("Incoming Ingredient");
        foreach (GameObject ingredient in ingredients)
        {
            ingredient.GetComponent<Ingredient>().Unfreeze();
        }
    }

    public void Resett()
    {
        Spawner.instance.Resett();
        ScoreManager.instance.ResetScore();

        GameObject[] ingredients = GameObject.FindGameObjectsWithTag
            ("Incoming Ingredient");
        foreach (GameObject ingredient in ingredients)
        {
            ingredient.GetComponent<Ingredient>().BecomeUsed();
        }

        GameObject[] combinators = GameObject.FindGameObjectsWithTag
            ("Combinator");
        foreach (GameObject combinator in combinators)
        {
            combinator.GetComponent<Combinator>().Resett();
        }
    }

    public void LoadLevel(int levelNum)
    {
        if (levelNum != currentLevelNum)
        {
            levels[currentLevelNum - 1].wholeLevel.SetActive(false);
            currentLevelNum = levelNum;
        }

        levels[currentLevelNum - 1].wholeLevel.SetActive(true);
        Spawner.instance.LoadLevelSpawnPoints(levels[currentLevelNum - 1].
            spawnPoints);

        Resett();
        // close other screens except level
        Unfreeze();
    }

    public void ReplayLevel()
    {
        Resett();
        // close other screens except level
        Unfreeze();
    }

    public void LevelSelection()
    {
        // Show level screen canvas
    }

    public void GameOver()
    {
        Freeze();
        levels[currentLevelNum - 1].UpdatePeakscore();
        ScoreManager.instance.UpdateHighScore();
        // Show level summary screen
    }
}
