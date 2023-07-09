using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= gunPos.x;
        mousePos.y -= gunPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        } else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }

        //if (90 < angle && angle < 180)
        //{
        //    if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        //    {
        //        transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -angle));
        //    }
        //}

        //if (angle < 90)
        //{
        //    if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= transform.position.x && Camera.main.ScreenToWorldPoint(Input.mousePosition).y >= transform.position.y)
        //    {
        //        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        //    }
        //}
    }
}
