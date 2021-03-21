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
    
    private int powerUpDroprate;
    public GameObject damagePowerUp;
    public GameObject doubleScorePowerUp;
    void Start()
    {
        powerUpDroprate = 15;
        material = gameObject.GetComponent<MeshRenderer>().material;
        ChangeColor();
    }

    public void LooseHp(int damage)
    {
        healthpoints -= damage;
        if (healthpoints <= 0)
        {
            PossiblePowerUp();
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
        Random random = new Random();
        float dropValue = random.Next(1, 100);
        if (dropValue <= powerUpDroprate)
        {
            if (dropValue >= 51)
            {
                Instantiate(damagePowerUp, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(doubleScorePowerUp, transform.position, Quaternion.identity);
            }
        }
    }

    private void OnDestroy()
    {
        LevelController lc = FindObjectOfType<LevelController>();
        if (lc != null)
        {
            lc.DecrementBricks();
        }
    }
}
