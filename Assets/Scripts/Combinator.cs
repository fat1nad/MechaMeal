using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Combinator : MonoBehaviour
{
    public GameObject[] combinations;
    Combination currentCombo;
    Animator comboAnim;

    void Start()
    {
        comboAnim = GetComponent<Animator>();       
    }

    public void SetCombination(string optionName)
    {
        GameObject CC = Array.Find(combinations, combo => 
        combo.GetComponent<Combination>().machineOption == optionName);
        currentCombo = CC.GetComponent<Combination>();

        foreach (GameObject c in combinations)
        {
            Combination combo = c.GetComponent<Combination>();
            if (combo.machineOption == optionName)
            {
                c.SetActive(true);
                currentCombo = combo;
            }
            else
            {
                c.SetActive(false);
            }          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Incoming Ingredient"))
        {
            Ingredient ing = collision.GetComponent<Ingredient>();

            if (ing.name == currentCombo.matchingIngredient)
            {
                ScoreManager.instance.AddScore(1);
                comboAnim.SetTrigger(currentCombo.animationTrigger);
            }
            else
            {
                Health.instance.ReduceHealth();
                comboAnim.SetTrigger("Wrong Combo");
            }

            ing.BecomeUsed();
        }
    }

    public void Resett()
    {
        SetCombination("Bread");
    }
}
