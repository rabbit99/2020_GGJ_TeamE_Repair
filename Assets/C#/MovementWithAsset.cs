using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovementWithAsset : MonoBehaviour, INotification

{
    public int playerId = 0; // The Rewired player id of this character
    public float speed = 1;
    public bool IsPicking { get { return isPicking; } }


    private Player player; // The Rewired Player
    private bool canClimb = false;
    private Vector3 moveMent = new Vector3();
    private Rigidbody2D r_2d;
    private bool fire;
    private bool isPicking = false;
    private GameObject item;
    private Animator _animator;

    private FixableTrigger fixable;
    public bool Repairing;
    private Tweener tweener;
    public FixableTrigger PartnerFixable;
    public ParticleSystem NoiceParticle;

    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool initialized;
    private string lastMovement = "right";

    // Start is called before the first frame update
    void Start()
    {
        r_2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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

    private void Move()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize(); // Reinitialize after a recompile in the editor
        Debug.Log("Move");
        if (canClimb)
        {
            if (_animator)
            {
                _animator.SetBool("canClimb", true);
                _animator.SetBool("running", false);
                var emission = NoiceParticle.emission;
                emission.rateOverTime = 0;
            }

            moveMent = new Vector3
            {
                x = player.GetAxis("Move Horizontal"),
                y = player.GetAxis("Move Vertical"),
            }.normalized;

            if (moveMent.y == 0)
            {
                _animator.SetBool("canClimb", false);
                var emission = NoiceParticle.emission;
                emission.rateOverTime = 0;
            }
        }
        else
        {
            if (_animator)
            {
                _animator.SetBool("canClimb", false);
                _animator.SetBool("running", true);
                var emission = NoiceParticle.emission;
                emission.rateOverTime = 10;
            }
            moveMent = new Vector3
            {
                x = player.GetAxis("Move Horizontal"),
            }.normalized;
        }

        if (moveMent.x == 0)
        {
            _animator.SetBool("running", false);
            var emission = NoiceParticle.emission;
            emission.rateOverTime = 0;
        }

        //Debug.Log(" mInput.x" + mInput.x);
        transform.Translate(moveMent * speed * Time.deltaTime);
        //GetComponent<Rigidbody2D>().pos

        string face = null;
        if (moveMent.x > 0)
        {
            transform.localScale = new Vector2(-0.279f, 0.279f);
            lastMovement = "right";
        }
        else if (moveMent.x < 0)
        {
            transform.localScale = new Vector2(0.279f, 0.279f);
            lastMovement = "left";
        }
        else if (moveMent.x == 0)
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
        if (player.GetButtonDown("Fire")) Debug.Log("Fire");
        if (fire && item && !isPicking)
        {
            Debug.Log("do Pick up tool");
            item.GetComponent<ToolPick>().BePickUp();
            item.transform.SetParent(this.gameObject.transform);
            tweener = item.transform.DOLocalMove(new Vector3(0, 1.91f, 0), 0.2f);
            isPicking = true;
        }
        else if (fire && item && isPicking)
        {
            if (fixable) return;
            tweener.Kill();
            Debug.Log("do Throw tool away");
            item.GetComponent<ToolPick>().BeThrowAway();
            item.transform.SetParent(null);
            isPicking = false;
        }
        if (fixable && item)
        {
            if (fixable.m_ToolType == item.GetComponent<ToolPick>().m_ToolType)
            {
                if (player.GetButton("Fire") && fixable.NeedRepair)
                {
                    if (!Repairing)
                    {
                        Repairing = !Repairing;
                        fixable.StartRepair();
                    }
                    fixable.Repairing();
                }
                if (player.GetButtonUp("Fire"))
                {
                    Repairing = false;
                    fixable.RepairGiveup();
                }
            }
        }
        if (fixable && !item)
        {
            if (fixable.m_ToolType == ToolPick.ToolType.nut)
            {

            }
        }
    }

    #region Notification
    void AddNotificationObserver()
    {
        NotificationCenter.Default.AddObserver(this, NotificationKeys.InTheLadder);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.OutTheLadder);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.InTheTool);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.OutTheTool);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.InTheFixablePipe);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.OutTheFixablePipe);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.RepairFinish);
    }

    void RemoveNotificationObserver()
    {
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.InTheLadder);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.OutTheLadder);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.InTheTool);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.OutTheTool);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.InTheFixablePipe);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.OutTheFixablePipe);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.RepairFinish);
    }

    public void OnNotify(Notification _noti)
    {
        if (_noti.name == NotificationKeys.InTheLadder)
        {
            Debug.Log("(string)_noti.data" + (string)_noti.data);
            if ((string)_noti.data == this.gameObject.name)
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
        if (_noti.name == NotificationKeys.InTheFixablePipe)
        {
            if ((string)_noti.data == this.gameObject.name)
            {
                FixableTrigger _item = (FixableTrigger)_noti.sender;
                fixable = _item;
            }
        }
        if (_noti.name == NotificationKeys.OutTheFixablePipe)
        {
            if ((string)_noti.data == this.gameObject.name)
            {
                Debug.Log("Exit Pipe");
                fixable = null;
            }
        }
    }
    #endregion
}
