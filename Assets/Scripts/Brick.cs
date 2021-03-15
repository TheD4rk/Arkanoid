using System;
using UnityEngine;
using Random = System.Random;

/**
 * Bricks are Objects that loose 1 Hp upon getting hit by the ball and eventually are destroyed.
 * They can also drop Power-Ups upon destruction
 */
public class Brick : MonoBehaviour
{
    public int healthpoints;
    private Material material;
    
    private Random random;
    private int powerUpDroprate;
    void Start()
    {
        random = new Random();
        powerUpDroprate = 25;
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void LooseHp(int damage)
    {
        healthpoints -= damage;
        if (healthpoints <= 0)
        {
            Destroy(gameObject);
        }
        ChangeColor();
    }

    private void ChangeColor()
    {
        switch (healthpoints)
        {
            case 1: material.color = Color.red;
                break;
            case 2: material.color = Color.blue;
                break;
            case 3: material.color = Color.green;
                break;
            case 4: material.color = Color.yellow;
                break;
            case 5: material.color = Color.magenta;
                break;
        }
    }

    private void PossiblePowerUp()
    {
        float dropValue = random.Next(1, 100);
        if (dropValue <= powerUpDroprate)
        {
            if (dropValue >= 51)
            {
                Debug.Log("Drop Power-Up 1");
                // TODO: Drop Power Up 1
            }
            else
            {
                Debug.Log("Drop Power-Up 2");
                // TODO: Drop Power Up 2
            }
        }
        Debug.Log("No Power-Up!");
    }
    
    private void OnDestroy()
    {
        Debug.Log("Brick destroyed");
        PossiblePowerUp();
    }
}
