using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Vector3 velocity;
    public float maxX;
    public float maxZ;

    public int damagePower;

    private void Start()
    {
        maxX = 15f;
        maxZ = 15f;
        velocity = new Vector3(0, 0, -maxZ);
        damagePower = 1;
    }
    
    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            float maxDistance = other.transform.localScale.x * 1 * 0.5f + transform.localScale.x * 1 * 0.5f;
            float dist = transform.position.x - other.transform.position.x;
            float nDist = dist / maxDistance;

            velocity = new Vector3(nDist * maxX, velocity.y, -velocity.z);
            if (other.CompareTag("Brick"))
            {
                other.gameObject.GetComponent<Brick>().LooseHp(damagePower);
            }
        }
        else if (other.CompareTag("Brick"))
        {
            // Debug.Log("Brick: " + other.transform.position);
            // Debug.Log("Ball: " + transform.position);

            if (CalculateDistance(transform, other.transform))
            {
                // Debug.Log("Vertical");
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            }
            else
            {
                // Debug.Log("Horizontal");
                velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
            }
            //other.GetComponent<Brick>().LooseHp(damagePower);
        }
        else if (other.CompareTag("VerticalWall"))
        {
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
        }
        else if (other.CompareTag("HorizontalWall"))
        {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        } else if (other.CompareTag("OutOfBounds"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        GetComponent<AudioSource>().Play();
    }

    private bool CalculateDistance(Transform ballTransform, Transform brickTransform)
    {
        float distanceX = Mathf.Abs(ballTransform.position.x - brickTransform.position.x);
        float distanceZ = Mathf.Abs(ballTransform.position.z - brickTransform.position.z);
        // Debug.Log(distanceX);
        // Debug.Log(distanceZ);
        if (distanceX >= brickTransform.localScale.x / 2)
        {
            if (distanceZ <= brickTransform.localScale.z / 2)
            {
                return true;
            }
            return distanceX - brickTransform.localScale.x / 2 > distanceZ - brickTransform.localScale.z / 2;
        }

        return false;
    }
}
