using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPick : MonoBehaviour
{
    public enum ToolType { wrench , tape , nut }
    public ToolType m_ToolType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            NotificationCenter.Default.Post(this, NotificationKeys.OutTheTool);
            Debug.Log("Tool Trigger Out, " + collision.gameObject.name);
        }
    }
}
