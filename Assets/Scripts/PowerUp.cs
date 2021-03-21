using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string type;

    void Update()
    {
        transform.position += new Vector3(0, 0, -5) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Paddle"))
        {
            if (type.Equals("Damage"))
            {
                FindObjectOfType<Ball>().ActivateDamagePowerUp();
            } else if (type.Equals("Score"))
            {
                FindObjectOfType<Ball>().ActivateDoubleScore();
            }
            Destroy(gameObject);
        } else if (other.CompareTag("OutOfBounds"))
        {
            Destroy(gameObject);
        }
    }
}
