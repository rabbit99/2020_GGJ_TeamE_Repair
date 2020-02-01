using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPick : MonoBehaviour
{
    public enum ToolType { wrench , tape , nut }
    public ToolType m_ToolType;
    private BoxCollider2D b2d;
    private Rigidbody2D r_2d;
    // Start is called before the first frame update
    void Start()
    {
        b2d = GetComponent<BoxCollider2D>();
        r_2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BePickUp()
    {
        b2d.enabled = false;
        r_2d.bodyType = RigidbodyType2D.Kinematic;
    }

    public void BeThrowAway()
    {
        b2d.enabled = true;
        r_2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.InTheTool, collision.gameObject.name);
            Debug.Log("Tool Trigger In, " + collision.gameObject.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.OutTheTool, collision.gameObject.name);
            Debug.Log("Tool Trigger Out, " + collision.gameObject.name);
        }
    }
}
