using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithAsset : MonoBehaviour, INotification

{
    public int playerId = 0; // The Rewired player id of this character
    public float speed = 1;

    

    private Player player; // The Rewired Player
    private bool canClimb = false;
    private Vector3 moveMent = new Vector3();
    private Rigidbody2D r_2d;
    private bool fire;
    private bool isPicking = false;
    private GameObject item;

    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool initialized;
    private string lastMovement = "right";

    // Start is called before the first frame update
    void Start()
    {
        r_2d = GetComponent<Rigidbody2D>();
        AddNotificationObserver();
        //NotificationCenter.Default.Post(this, NotificationKeys.MissionInfoRefresh);
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        player = ReInput.players.GetPlayer(playerId);

        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate() => Move();

    private void Move() {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize(); // Reinitialize after a recompile in the editor
        Debug.Log("Move");
        if (canClimb)
        {
            moveMent = new Vector3
            {
                x = player.GetAxis("Move Horizontal"),
                y = player.GetAxis("Move Vertical"),
            }.normalized;
        }
        else
        {
            moveMent = new Vector3
            {
                x = player.GetAxis("Move Horizontal"),
            }.normalized;
        }
       
        //Debug.Log(" mInput.x" + mInput.x);
        transform.Translate(moveMent * speed * Time.deltaTime);
        //GetComponent<Rigidbody2D>().pos

        string face = null;
        if(moveMent.x > 0)
        {
            transform.localScale = new Vector2(-0.279f, 0.279f);
            lastMovement = "right";
        }
        else if(moveMent.x < 0)
        {
            transform.localScale = new Vector2(0.279f, 0.279f);
            lastMovement = "left";
        }
        else if(moveMent.x == 0)
        {
            switch (lastMovement)
            {
                case "right":
                    transform.localScale = new Vector2(-0.279f, 0.279f);
                    break;
                case "left":
                    transform.localScale = new Vector2(0.279f, 0.279f);
                    break;
            }
        }

        fire = player.GetButtonDown("Fire");
        if (fire && item && !isPicking)
        {
            Debug.Log("do Pick up tool");
            item.GetComponent<ToolPick>().BePickUp();
            item.transform.SetParent(this.gameObject.transform);
            isPicking = true;
        }
         else if(fire && item && isPicking)
        {
            Debug.Log("do Throw tool away");
            item.GetComponent<ToolPick>().BeThrowAway();
            item.transform.SetParent(null);
            isPicking = false;
        }
    }

    #region Notification
    void AddNotificationObserver()
    {
        NotificationCenter.Default.AddObserver(this, NotificationKeys.InTheLadder);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.OutTheLadder);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.InTheTool);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.OutTheTool);
    }

    void RemoveNotificationObserver()
    {
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.InTheLadder);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.OutTheLadder);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.InTheTool);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.OutTheTool);
    }

    public void OnNotify(Notification _noti)
    {
        if (_noti.name == NotificationKeys.InTheLadder)
        {
            Debug.Log("(string)_noti.data" + (string)_noti.data);
            if((string)_noti.data == this.gameObject.name)
            {
                canClimb = true;
                r_2d.gravityScale = 0;
            }
        }
        if (_noti.name == NotificationKeys.OutTheLadder)
        {
            Debug.Log("(string)_noti.data" + (string)_noti.data);
            if ((string)_noti.data == this.gameObject.name)
            {
                canClimb = false;
                r_2d.gravityScale = 1;
            }
        }
        if (_noti.name == NotificationKeys.InTheTool)
        {
            if ((string)_noti.data == this.gameObject.name)
            {
                ToolPick _item = (ToolPick)_noti.sender;
                item = _item.gameObject;
            }
        }
        if (_noti.name == NotificationKeys.OutTheTool)
        {
            if ((string)_noti.data == this.gameObject.name)
            {
                item = null;
            }
        }
    }
    #endregion
}
