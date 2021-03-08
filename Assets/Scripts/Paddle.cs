using System;
using UnityEngine;

/**
 * Controller Class for a paddle
 */
public class Paddle : MonoBehaviour
{
    public Transform playArea;
    private float playAreaSize;
    private float paddleSize;
    [SerializeField]
    [Tooltip("Changes the speed with which the paddle moves to the left or right")]
    private float moveSpeed;

    private void Start()
    {
        moveSpeed = 9f;
        playAreaSize = playArea.localScale.x * 10;
        paddleSize = transform.localScale.x * 1;
    }

    void Update()
    {
        float direction = Input.GetAxis("Horizontal"); // Get Direction (Left or Right)
        float newX = transform.position.x + Time.deltaTime * moveSpeed * direction;
        float maxX = 0.5f * playAreaSize - 0.5f * paddleSize;
        float clampedX = Mathf.Clamp(newX, -maxX, maxX);
        
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
