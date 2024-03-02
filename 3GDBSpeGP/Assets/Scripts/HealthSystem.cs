using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet que le script tourne dans l'éditeur [ExecuteInEditMode]
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int currenthealth;
    [SerializeField] private int maxHealth = 100;
    
    [HideInInspector] public int mana;

    public void SetHealth(int newHealth)
    {
        currenthealth = newHealth;
    }

    [ContextMenu("RefillHealth")]
    public void RefillHealth()
    {
        SetHealth(maxHealth);
    }
}
