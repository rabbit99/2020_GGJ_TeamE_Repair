using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPick : MonoBehaviour
{
    public enum ToolType { wrench, tape, nut, none }
    public ToolType m_ToolType;
    private BoxCollider2D b2d;
    private Rigidbody2D r_2d;
    public bool Picking;
    public SpriteRenderer OutlineObj;
    // Start is called before the first frame update
    void Start()
    {
        b2d = GetComponent<BoxCollider2D>();
        r_2d = GetComponent<Rigidbody2D>();
        OutlineObj.material.SetColor("_SolidOutline", Color.clear);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BePickUp()
    {
        b2d.enabled = false;
        r_2d.bodyType = RigidbodyType2D.Kinematic;
        Picking = true;
    }

    public void BeThrowAway()
    {
        b2d.enabled = true;
        r_2d.bodyType = RigidbodyType2D.Dynamic;
        Picking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Picking) return;

        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            if (movement.IsPicking) return;
            NotificationCenter.Default.Post(this, NotificationKeys.InTheTool, collision.gameObject.name);
            Debug.Log(gameObject.name + " Trigger In, " + collision.gameObject.name);
            OutlineObj.material.SetColor("_SolidOutline", Color.red);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Picking) return;

        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            if (movement.IsPicking) return;
            NotificationCenter.Default.Post(this, NotificationKeys.OutTheTool, collision.gameObject.name);
            Debug.Log(gameObject.name + " Trigger Out, " + collision.gameObject.name);
            OutlineObj.material.SetColor("_SolidOutline", Color.clear);
        }
    }
}
