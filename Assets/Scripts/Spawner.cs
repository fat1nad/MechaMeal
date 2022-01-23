// Author: Fatima Nadeem

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    static public Spawner instance;

    bool unfrozen; // Frozen status for pause/game over
    bool canSpawn; // Bool value to impose spawn rate

    int ingCount; // No. of incoming ingredient types
    int spawnPointsCount; // No. of spawn points

    float ingSpeed; // Speed of incoming ingredients in units per second
    float spawnRateGap; // Time interval between each spawn in seconds

    float spawnPointsY; // Y value of spawn points in world coordinates
    Vector2 usedIngLoc; // Location of all used ingredients

    List<Vector2> spawnPoints;
    List<GameObject> used; // Ingredients already instantiated and used


    [Range(0f, 15f)]
    public float initIngSpeed; // Initial speed of incoming ingredients in
                               // units per second
    [Range(0f, 15f)]
    public float initSpawnRateGap; // Initial time interval between each spawn
                                   // in seconds

    [Range(0f, 5f)]
    public float diffIncrease; // This value dictates the difficulty - increase
                               // in the speed of incoming ingredients and the
                               // decrease in the spawn rate gap

    public float outsideOffset; // position offset in viewport coordinates for
                                // items(spawn points and used ingredients)
                                // kept outside screen

    public GameObject[] fallingIng; // Incoming ingredient types

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Camera cam = Camera.main;
        
        spawnPointsY = cam.ViewportToWorldPoint
            (new Vector2(0, 1 + outsideOffset)).y;

        usedIngLoc = cam.ViewportToWorldPoint(new Vector2(0, -outsideOffset));

        spawnPoints = new List<Vector2>();
        spawnPointsCount = 0;

        used = new List<GameObject>();

        ingCount = fallingIng.Length;

        ingSpeed = initIngSpeed;
        spawnRateGap = initSpawnRateGap;
        canSpawn = true;

        unfrozen = false;
    }


    void Update()
    {
        if (unfrozen)
        {
            StartCoroutine(Spawn());
            spawnRateGap /= (diffIncrease * Time.deltaTime);
            ingSpeed *= (diffIncrease * Time.deltaTime);
        }
    }
    IEnumerator Spawn()
    {
        if (canSpawn)
        {
            canSpawn = false;

            // Selecting a spawn point at random to spawn
            int num = UnityEngine.Random.Range(0, spawnPointsCount);
            Vector2 spawnPoint = spawnPoints[num];

            // Selecting an ingredient type at random to spawn
            num = UnityEngine.Random.Range(0, ingCount);
            GameObject ingToSpawn = fallingIng[num];

            // Finding a used copy of the ingredient type
            GameObject usedIng = used.Find
                (ingredient => ingredient.name == ingToSpawn.name);
            
            if (usedIng != null) // If a used copy exists
            {
                // Reusing the copy
                usedIng.transform.position = spawnPoint;
                usedIng.GetComponent<Ingredient>().Reset();
                used.Remove(usedIng);
            }
            else
            {
                Instantiate(ingToSpawn, spawnPoint,
                    ingToSpawn.transform.rotation);
            }

            yield return new WaitForSeconds(spawnRateGap);

            canSpawn = true;
        }
    }

    public float IngSpeed()
    {
        return ingSpeed;
    }

    public Vector2 UsedIngloc()
    {
        return usedIngLoc;
    }

    public void AddToUsed(GameObject usedIng)
    /* 
        This function allows any script to inform the spawner that an 
        ingredient has been used.
     */
    {
        used.Add(usedIng);
    }

    public void LoadLevelSpawnPoints(Transform[] levelSpawnPts)
    {       
        spawnPointsCount = levelSpawnPts.Length;

        spawnPoints.Clear();
        for (int i = 0; i < spawnPointsCount; i++)
        { 
            spawnPoints.Add(new Vector2(levelSpawnPts[i].position.x, 
                spawnPointsY));
        }
    }

    public void Freeze()
    {
        unfrozen = false;
    }

    public void Unfreeze()
    {
        unfrozen = true;
    }

    public void Resett()
    {
        spawnRateGap = initSpawnRateGap;
        ingSpeed = initIngSpeed;
        StopAllCoroutines();
        canSpawn = true;
    }
}
