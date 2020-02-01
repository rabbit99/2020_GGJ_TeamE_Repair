using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private bool stairs = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (stairs)
        {
            transform.Translate(Vector3.up * Input.GetAxis("Horizontal"));
        }
        else
        {
            transform.Translate(Vector3.right * Input.GetAxis("Vertical"));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //是否碰樓梯
        if (collision.transform.tag == "stairs" && Input.GetKeyDown(KeyCode.W))
        {
            stairs = true;
            transform.position = new Vector3(collision.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //是否離開樓梯
        if (collision.transform.tag == "stairs")
        {
            stairs = false;
        }
    }
}
