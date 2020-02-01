using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixableTrigger : MonoBehaviour
{
    public ToolPick.ToolType m_ToolType = ToolPick.ToolType.wrench;
    public MovementWithAsset CurrentPlayer;
    public bool NeedRepair;
    public UnityEvent OnBroken, OnRepair;
    private float t = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedRepair)
        {
            
        }
    }
    public void Broken()
    {
        NeedRepair = true;
        OnBroken.Invoke();
    }
    public void Repair()
    {
        OnRepair.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (CurrentPlayer != null) return;
        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.InTheFixablePipe, collision.gameObject.name);
            Debug.Log("Pipe Trigger In, " + collision.gameObject.name);

            CurrentPlayer = movement;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.OutTheFixablePipe);
            Debug.Log("Pipe Trigger Out, " + collision.gameObject.name);

            CurrentPlayer = null;
        }
    }
}
