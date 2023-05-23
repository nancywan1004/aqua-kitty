using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperController : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3.0f;
    [SerializeField]
    private float characterVelocity;
    [SerializeField]
    private bool isVertical;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        if (movementDirection == null)
        {
            if (isVertical == true)
            {
                movementDirection = new Vector2(0, Random.Range(-1.0f, 1.0f)).normalized;
            }
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 0).normalized;
        }

        if (!isVertical)
        {
            if (movementDirection.x > 0)
            {
                movementDirection = new Vector2(Random.Range(-1.0f, 0f), 0).normalized;
            }
            else
            {
                movementDirection = new Vector2(Random.Range(0f, 1.0f), 0).normalized;
            }
        }

        if (isVertical == true)
        {
            if (movementDirection.y > 0 && isVertical == true)
            {
                movementDirection = new Vector2(0, Random.Range(-1.0f, 0f)).normalized;
            }
            else
            {
                movementDirection = new Vector2(0, Random.Range(0f, 1.0f)).normalized;
            }
        }
        
        movementPerSecond = movementDirection * characterVelocity;
    }
}
