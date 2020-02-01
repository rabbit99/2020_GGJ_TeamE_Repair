using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixableTrigger : MonoBehaviour
{
    public ToolPick.ToolType m_ToolType = ToolPick.ToolType.wrench;
    public MovementWithAsset CurrentPlayer;
    public TimeCircleUI TimeCircle;
    public bool NeedRepair;
    public UnityEvent OnBroken, OnRepairFinished, OnStartRepair;
    private float t = 1;
    private float gameOverTime = 10;
    private bool isRepairing;
    // Start is called before the first frame update
    void Start()
    {
        TimeCircle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedRepair)
        {
            if (!isRepairing)
            {
                TimeCircle.gameObject.SetActive(true);
                TimeCircle.Circle.color = Color.red;
                gameOverTime -= Time.deltaTime;
                TimeCircle.Circle.fillAmount = gameOverTime / 10;
                if (gameOverTime < 0)
                {
                    NeedRepair = false;
                    GameManager.Instance.GameOver();
                }
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
        t = 1;
        gameOverTime = 10;
        isRepairing = false;
        OnRepairFinished.Invoke();
        TimeCircle.gameObject.SetActive(false);
    }
    public void StartRepair()
    {
        if (!NeedRepair) return;
    }
    public void Repairing()
    {
        if (!NeedRepair) return;
        isRepairing = true;
        TimeCircle.gameObject.SetActive(true);
        TimeCircle.Circle.color = Color.green;
        if (t > 0)
        {
            t -= Time.deltaTime;
        }
        else
        {
            Repair();
        }
        TimeCircle.Circle.fillAmount = 1 - t;
    }
    public void RepairGiveup()
    {
        if (!NeedRepair) return;
        t = 1;
        isRepairing = false;
        Broken();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (CurrentPlayer != null) return;
        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.InTheFixablePipe, collision.gameObject.name);
            Debug.Log(this.gameObject.name + " Trigger In, " + collision.gameObject.name);

            CurrentPlayer = movement;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.OutTheFixablePipe, collision.gameObject.name);
            Debug.Log(this.gameObject.name + " Trigger Out, " + collision.gameObject.name);

            CurrentPlayer = null;
        }
    }
}
