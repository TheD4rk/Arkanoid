using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private int scoreMultiplier;

    public Image damageBar;
    private float damageTime;
    private bool damageOn;
    
    public Image scoreBar;
    private float scoreTime;
    private bool scoreOn;

    private void Start()
    {
        lifes = 3;
        damagePower = 1;
        scoreValue = 0;

        damageTime = 0f;

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
            GetComponent<AudioSource>().Play();
        }
        else if (other.CompareTag("Brick")) // TODO: Try Laurens Code
        {
            scoreValue += 100 * scoreMultiplier;
            scoreText.text = scoreValue.ToString();

            if (CalculateDistance(transform, other.transform))
            {
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            }
            else
            {
                velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
            }
            other.GetComponent<Brick>().LooseHp(damagePower);
            GetComponent<AudioSource>().Play();
        }
        else if (other.CompareTag("VerticalWall"))
        {
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            GetComponent<AudioSource>().Play();
        }
        else if (other.CompareTag("HorizontalWall"))
        {
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
            GetComponent<AudioSource>().Play();
        } else if (other.CompareTag("OutOfBounds"))
        {
            LooseLife();
        }
    }

    private bool CalculateDistance(Transform ballTransform, Transform brickTransform)
    {
        float distanceX = Mathf.Abs(ballTransform.position.x - brickTransform.position.x);
        float distanceZ = Mathf.Abs(ballTransform.position.z - brickTransform.position.z);
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

    public void ActivateDamagePowerUp()
    {
        damageTime = 1f;
        if (!damageOn)
        {
            damagePower++;
            damageOn = true;
            StartCoroutine(DamageTimer());
        }
    }

    private IEnumerator DamageTimer()
    {
        for (;damageTime > 0; damageTime -= 0.01f)
        {
            damageBar.fillAmount = damageTime;
            yield return new WaitForSeconds(0.1f);
        }
        damageOn = false;
        damagePower--;
    }

    public void ActivateDoubleScore()
    {
        scoreTime = 1f;
        if (!scoreOn)
        {
            scoreMultiplier++;
            scoreOn = true;
            StartCoroutine(ScoreTimer());
        }
    }
    
    private IEnumerator ScoreTimer()
    {
        for (;scoreTime > 0; scoreTime -= 0.01f)
        {
            scoreBar.fillAmount = scoreTime;
            yield return new WaitForSeconds(0.1f);
        }
        scoreOn = false;
        scoreMultiplier--;
    }
}
