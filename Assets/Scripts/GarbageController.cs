using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GarbageController : MonoBehaviour
{
    private GameObject pops;
    [SerializeField]
    private GameObject popsPrefab;
    [SerializeField]
    private GameObject comboPrefab;
    [SerializeField]
    private GameObject comboTextObj; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Garbage"))
        {
            //Debug.Log("Double kill!");
            GameObject comboText = Instantiate(comboTextObj, transform.position + 0.5f * transform.right, Quaternion.identity);
            //comboText.transform.SetParent(transform);
            TextMeshPro theText = comboText.transform.GetComponent<TextMeshPro>();
            theText.text = "+1!";

            //Debug.Log("Gain bubble!");
            PowerfulBubbleBarController.Instance.GainBubble(1.0f);

            SoundManager.PlaySound("bubbleHit");

            transform.parent.localScale = new Vector3(transform.parent.localScale.x * 1.25f, transform.parent.localScale.y * 1.25f, 0);

            if (pops != null) Destroy(pops);
            pops = Instantiate(popsPrefab, transform.position, Quaternion.identity);
            pops.GetComponent<ParticleSystem>().Play();
            Destroy(pops, 3f);
            Destroy(comboText, 2f);
            //StartCoroutine(WaitAndClearText(2.0f))
        }
    }

/*    private IEnumerator WaitAndClearText(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            comboText.text = "";
            //print("WaitAndPrint " + Time.time);
        }
    }
*/
    // Update is called once per frame
    void Update()
    {
        
    }


}
