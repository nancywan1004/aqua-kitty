using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public GameObject popsPrefab;
    //public GameObject pops;
    //private ParticleSystem _pops;

    Rigidbody2D rb;
    GameObject pops;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 force = transform.right * bulletSpeed;
        rb.AddForce(force, ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<GarbageController>() != null) {
            SoundManager.PlaySound("bubbleHit");
            collision.gameObject.transform.SetParent(this.gameObject.transform);
            collision.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    IEnumerator stopPops()
    {
        yield return new WaitForSeconds(.4f);
        //pops.GetComponent<ParticleSystem>().Stop();
    }
}
