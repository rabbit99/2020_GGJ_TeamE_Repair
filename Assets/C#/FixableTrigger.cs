using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixableTrigger : MonoBehaviour
{
    public ToolPick.ToolType m_ToolType = ToolPick.ToolType.wrench;
    public MovementWithAsset CurrentPlayer;
    public bool NeedRepair;
    public UnityEvent OnBroken, OnRepairFinished, OnStartRepair;
    private float t = 1;
    private float gameOverTime = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (NeedRepair)
        {
            gameOverTime -= Time.deltaTime;
            if (gameOverTime < 0)
            {
                NeedRepair = false;
                GameManager.Instance.GameOver();
            }
        }
    }
    public void Broken()
    {
        NeedRepair = true;
        OnBroken.Invoke();
    }
    private void Repair()
    {
        NeedRepair = false;
        OnRepairFinished.Invoke();
    }
    public void StartRepair()
    {
        if (!NeedRepair) return;
    }
    public void Repairing()
    {
        if (!NeedRepair) return;
        if (t > 0)
        {
            t -= Time.deltaTime;
        }
        else
        {
            Repair();
        }
    }
    public void RepairGiveup()
    {
        if (!NeedRepair) return;
        t = 1;
        Broken();
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
