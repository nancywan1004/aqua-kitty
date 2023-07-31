using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float movementSpeed = 1.2f;

    public GameObject bulletPrefab;
    //public GameObject webZipPrefab;
    public GameObject popsPrefab;
    public Transform firePoint;

    public Animator animator;

    private Rigidbody2D _rigidbody;
    //private GameObject webZip;
    private State state;
    private Vector3 mousePos;
    private float rotateY;
    private GameObject pops;
    private float holdDownStartTime;
    private float holdDownTime;

    private Vector3 webZipDir;
    private Vector3 webZipTargetPosition;
    private float webZipSpeed;
    //private SpriteRenderer webZipSpriteRenderer;
    private Vector2 webZipStart;
    private Vector2 webZipEnd;

    private GlobalInputActions _inputActions;

    private enum State
    {
        Normal,
        WebZipping,
        WebZippingSliding,
        Attached,
        Dizzy
    }

    private void Awake()
    {
        _inputActions = new GlobalInputActions();
        _inputActions.Player.Bubbleshoot.performed += context => Shoot();
        _inputActions.Player.Grapple.started += context =>
        {
            HandleWebZipStart();
        };
        // _inputActions.Player.Grapple.performed += context =>
        // {
        //     webZip.transform.position = firePoint.transform.position;
        //     webZipSpriteRenderer = webZip.GetComponent<SpriteRenderer>();
        //     webZipStart = new Vector2(webZip.GetComponent<SpriteRenderer>().size.x, 0);
        //     webZipEnd = new Vector2(webZip.GetComponent<SpriteRenderer>().size.x,
        //         Vector3.Distance(firePoint.transform.position, webZipTargetPosition));
        //
        //     webZipSpriteRenderer.size = webZipEnd;
        // };
        _inputActions.Player.Grapple.canceled += context =>
        {
            HandleWebZipping();
        };
        state = State.Normal;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.isKinematic = false;
    }
    
    public void OnEnable()
    {
        _inputActions.Enable();
    }

    public void OnDisable()
    {
        _inputActions.Disable();
    }
    
    // Update is called once per frame
    private void Update()
    {
        var move = _inputActions.Player.Move.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(move);
        switch (state)
        {
            case State.Normal:
                animator.Play("Cat_Swim");
                HandleMovement();
                break;
            case State.WebZipping:
                break;
            case State.WebZippingSliding:
                HandleWebZippingSliding();
                break;
            case State.Attached:
                HandleRelativeRotation();
                break;
            case State.Dizzy:
                // if (webZip != null)
                // {
                //     webZip.gameObject.SetActive(false);
                // }
                animator.Play("Cat_Dizzy");
                return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            state = State.Dizzy;
            StartCoroutine(backToNormal());
        }
    }

    IEnumerator backToNormal()
    {
        yield return new WaitForSeconds(2.0f);
        state = State.Normal;
    }

    private void FixedUpdate()
     {
        switch (state)
        {
            case State.Normal:
                _rigidbody.velocity = new Vector2(0, 0);
                break;
        }
     }

    private void HandleRelativeRotation()
    {
        rotateY = 0f;
        if (mousePos.x > transform.position.x)
        {
            rotateY = 180f;
        }
        transform.localRotation = Quaternion.Euler(0, rotateY, 0);
    }

    private void HandleMovement() {
        rotateY = 0f;
        if (mousePos.x > transform.position.x)
        {
            rotateY = 180f;
        }
        transform.rotation = Quaternion.Euler(0, rotateY, 0);
        Vector3 moveVector = (mousePos - transform.position).normalized;
        transform.position += moveVector * Time.deltaTime * movementSpeed;
    }


    private void Shoot ()
    {
        if (BubbleBarController.Instance.GetCurrBubble() > 0)
        {
            // Spawn bullets
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            float oxygenSpeed = 1f;
            BubbleBarController.Instance.ConsumeBubble(oxygenSpeed);
            StartCoroutine(DestroyBubble(bullet));
        }
    }

    IEnumerator DestroyBubble(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        if (bullet != null)
        {
            pops = Instantiate(popsPrefab, bullet.gameObject.transform.position, Quaternion.identity);
            pops.GetComponent<ParticleSystem>().Play();
            //Debug.Log("childCount is: " + bullet.transform.childCount);
            if (bullet.transform.childCount > 0)
            {
               // Debug.Log("the child is: " + bullet.transform.GetChild(0).transform);
                for (float i = bullet.transform.childCount; i > 0; i--)
                {
                    GarbageSpawner.Instance.RemoveGarbage();
                }
            }
            //SoundManager.PlaySound("bubblePop");
            Destroy(bullet);
            Destroy(pops, 3f);
        }
    }


    private void HandleWebZipStart() {

        GrappleSliderController.Instance.HideSlider();

        holdDownStartTime = Time.time;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);

        //Debug.Log(hit.collider);
        // if (hit.collider != null && hit.collider.CompareTag("Helper"))
        // {
            GrappleSliderController.Instance.InitSlider(holdDownStartTime);
            SoundManager.PlaySound("grapple");
            webZipTargetPosition = mousePos;
            webZipDir = (webZipTargetPosition - transform.position).normalized;

            //spawnWebZip(firePoint.transform.position, firePoint.transform.rotation);
            //Vector3 webDir = (webZipTargetPosition - firePoint.transform.position).normalized;
            //webZip.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(webDir.y, webDir.x) * Mathf.Rad2Deg - 90);

            webZipSpeed = 20.0f;
            state = State.WebZipping;
        //}

    }

    private void HandleWebZipping()
    {
        //Debug.Log("HandleWebZipping status");
        GrappleSliderController.Instance.HideSlider();
        holdDownTime = Time.time - holdDownStartTime;
        //webZipSpriteRenderer.size = Vector2.Lerp(webZipStart, webZipEnd, holdDownTime);

        float travelDistance = Vector2.Distance(transform.position, webZipTargetPosition);
        if (webZipSpeed * holdDownTime >= travelDistance)
        {
           // Debug.Log("Hold for too long!");
            transform.position = Vector2.MoveTowards(
                transform.position,
                webZipTargetPosition,
                webZipSpeed * (travelDistance / webZipSpeed)
          );
        } else
        {
            transform.position = Vector2.MoveTowards(
            transform.position,
            webZipTargetPosition,
            webZipSpeed * holdDownTime
        );
        }

        state = State.WebZippingSliding;

    }

    private void HandleWebZippingSliding()
    {
        //Debug.Log("HandleWebZippingSliding status");
        // webZipDir = new Vector3(webZipDir.x, webZipDir.y, 0);
        webZipSpeed -= webZipSpeed * Time.deltaTime * 15.0f;
        transform.position += webZipDir * webZipSpeed * Time.deltaTime;
        //Debug.Log("webZipSpeed is: " + webZipSpeed);
        if (webZipSpeed <= 0.8f)
        {
            //webZip.gameObject.SetActive(false);
            state = State.Normal;
        }
    }

    // private void spawnWebZip(Vector2 position, Quaternion rotation)
    // {
    //     if (webZip == null)
    //     {
    //         webZip = Instantiate(webZipPrefab, position, rotation);
    //     }
    //     else
    //     {
    //         webZip.transform.position = position;
    //         webZip.transform.rotation = rotation;
    //         webZip.gameObject.SetActive(true);
    //     }
    // }

}
