using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 5f;
    public Animator animator;
    private PlayerController player;
    
    [SerializeField]
    private Transform castPoint;
    [SerializeField]
    private float swimmingVelocity;
    [SerializeField]
    private float attackVelocity;
    [SerializeField]
    private Transform[] enemyPath;
    [SerializeField]
    private float attackPower;

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float latestDirectionChangeTime;
    private float latestTargettingTime;
    private readonly float directionChangeTime = 3.0f;
    private readonly float idlePeriod = 5.0f;
    private float agroRange = 15.0f;
    private float characterVelocity;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private Collider2D collider;
    private bool canSeePlayer = false;
    private Vector2 endPos;
    private RaycastHit2D[] hits;
    private bool isDetecting = false;
    private Transform enemySpawnPoint;
    private Vector3 force;
    private bool isFlipped = false;

    private int pathIndex;

    private enum State
    {
        Swimming,
        Detecting,
        ChaseTarget,
        GoingBackToStart,
        RandomMoving
    }

    private State state;

    private void Awake()
    {
        player = PlayerController.Instance;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        pathIndex = 0;
        enemySpawnPoint = enemyPath[pathIndex];
        state = State.Swimming;
    }

    void Start()
    {
        Random.InitState(42);
        transform.position = enemySpawnPoint.position;
        startPosition = transform.position;
        latestDirectionChangeTime = 0f;
        latestTargettingTime = 0f;
    }

    void Update()
    {
        switch (state) {
            default:
            case State.Swimming:
                isDetecting = false;
                laserLineRenderer.enabled = false;
                characterVelocity = swimmingVelocity;
                //if the changeTime was reached, calculate a new movement vector
                if (Time.time - latestDirectionChangeTime > directionChangeTime)
                {
                    latestDirectionChangeTime = Time.time;
                    //isFlipped = false;
                    if (pathIndex < enemyPath.Length - 1)
                    {
                        pathIndex++;
                    }
                    else
                    {
                        pathIndex = 0;
                    }
                    enemySpawnPoint = enemyPath[pathIndex];
                }

                if (enemySpawnPoint != null)
                {
                    LookAt(enemySpawnPoint.position);
                    transform.position = Vector2.MoveTowards(transform.position, enemySpawnPoint.position, Time.deltaTime * characterVelocity);
                }

                if (Time.time - latestTargettingTime > idlePeriod)
                {
                    latestTargettingTime = Time.time;
                    state = State.Detecting;
                }

                break;
            case State.Detecting:
                isDetecting = true;
                laserLineRenderer.enabled = true;
                FindTarget();
                break;
            case State.ChaseTarget:
                collider.isTrigger = false;
                characterVelocity = attackVelocity;
                LookAt(player.transform.position);
                canSeePlayer = CanSeePlayer(agroRange);
                // Vector2.Distance(transform.position, player.transform.position) > stopChaseDistance
                if (canSeePlayer == false)
                {
                    state = State.GoingBackToStart;
                }
                break;
            case State.GoingBackToStart:
                isDetecting = false;
                laserLineRenderer.enabled = false;
                collider.isTrigger = true;
                characterVelocity = swimmingVelocity;
                LookAt(startPosition);
                if (Vector2.Distance(transform.position, startPosition) < 0.1f)
                {
                    state = State.Swimming;
                }
                break;
            case State.RandomMoving:
                if (Vector2.Distance(transform.position, player.transform.position) > 10.0f)
                {
                    state = State.GoingBackToStart;
                }
                break;
        }
        animator.SetBool("isDetecting", isDetecting);
        animator.SetFloat("Speed", characterVelocity);
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            default:
            case State.Swimming:
            case State.Detecting:
            case State.RandomMoving:
                rb.velocity = new Vector2(0f, 0f);
                break;
            case State.ChaseTarget:
                MoveTo(movementDirection);
                break;
            case State.GoingBackToStart:
                MoveTo(movementDirection);
                break;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == State.ChaseTarget && collision.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound("meow");
            HealthBarController.Instance.GetAttacked(attackPower);
            state = State.GoingBackToStart;
        }
    }

    private void LookAt(Vector2 target)
    {
        movementDirection.x = target.x - transform.position.x;
        movementDirection.y = target.y - transform.position.y;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void MoveTo(Vector2 direction) {
        //Debug.Log("Chase me!");
        rb.MovePosition((Vector2) transform.position + (direction * characterVelocity * Time.deltaTime));
    }

    private void FindTarget() 
    {
        // player within target range
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            if (pathIndex < enemyPath.Length - 1)
            {
                pathIndex++;
            }
            else
            {
                pathIndex = 0;
            }
            enemySpawnPoint = enemyPath[pathIndex];
        }

        if (enemySpawnPoint != null)
        {
            LookAt(enemySpawnPoint.position);
            transform.position = Vector2.MoveTowards(transform.position, enemySpawnPoint.position, Time.deltaTime * characterVelocity);
        }

        canSeePlayer = CanSeePlayer(agroRange);
        
        if (canSeePlayer)
        {
            if (Vector2.Distance(castPoint.position, player.transform.position) <= agroRange)
            {
                state = State.ChaseTarget;
            }

        }
        if (Time.time - latestTargettingTime > idlePeriod)
        {
            latestTargettingTime = Time.time;
            state = State.Swimming;
        }
    }


    private bool CanSeePlayer(float distance)
    {
        bool val = false;
        Vector3 dir = new Vector3(player.transform.position.x - castPoint.position.x, player.transform.position.y - castPoint.position.y, 0);
        //Vector3 dir = movementDirection;
        endPos = castPoint.position + dir.normalized * distance; // new Vector3(position.x + distance)
        hits = Physics2D.RaycastAll(castPoint.position, dir.normalized, distance, 1 << LayerMask.NameToLayer("Action"));

        if (hits != null && hits.Length > 0)
        {
            if (hits[0].collider != null)
            {
                if (hits[0].collider.gameObject.CompareTag("Helper"))
                {
                    val = false;
                }
                else
                {
                    val = true;
                }
                //Debug.Log("HitCollider: " + hits[0].collider.gameObject);
            }
        }

        Debug.DrawLine(castPoint.position, endPos, Color.blue);
        laserLineRenderer.SetPosition(0, castPoint.position);
        laserLineRenderer.SetPosition(1, endPos);
        laserLineRenderer.widthCurve = new AnimationCurve(new Keyframe(0, 0.1f), new Keyframe(.1f, .5f), new Keyframe(.9f, .5f), new Keyframe(1, 1.0f));
        //laserLineRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0.3f));
        return val;
        //}
    }

}
