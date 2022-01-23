using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string name;

    bool unused;
    bool unfrozen; // Frozen status for pause/game over
    
    // Start is called before the first frame update
    void Start()
    {
        unused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (unused && unfrozen)
        {
            transform.Translate(Vector2.down * Spawner.instance.IngSpeed()
                * Time.deltaTime);
        }      
    }

    public void BecomeUsed()
    {
        unused = false;
        transform.position = Spawner.instance.UsedIngloc();
        Spawner.instance.AddToUsed(this.gameObject);
    }

    public void Reset()
    {
        unused = true;
    }

    public void Freeze()
    {
        unfrozen = false;
    }

    public void Unfreeze()
    {
        unfrozen = true;
    }
}
