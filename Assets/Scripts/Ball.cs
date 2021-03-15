using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 ballStartPosition;
    private int lifes;
    public GameObject[] lifeImages; // TODO: Put UI Code into Settings Script

    private Vector3 velocity;
    public float maxX;
    public float maxZ;

    public int damagePower;
    
    public TMP_Text scoreText;
    private int scoreValue;

    private void Start()
    {
        lifes = 3;
        damagePower = 1;
        scoreValue = 0;

        ballStartPosition = new Vector3(0, 0, 0.5f);
        maxX = 15f;
        maxZ = 15f;
        velocity = new Vector3(0, 0, -maxZ);
        Time.timeScale = 1;
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
        }
        else if (other.CompareTag("Brick")) // TODO: Try Laurens Code
        {
            scoreValue += 100;
            scoreText.text = scoreValue.ToString();
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
            LooseLife();
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

    private void LooseLife()
    {
        if (lifes <= 1)
        {
            lifeImages[0].SetActive(false);
            FindObjectOfType<SettingsMenu>().GameOver();
        }
        else
        {
            lifes--;
            lifeImages[lifes].SetActive(false);
            transform.position = ballStartPosition;
            velocity = new Vector3(0, 0, -maxZ);
        }
    }
}
