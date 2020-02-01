using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithAsset : MonoBehaviour, INotification

{
    public float speed = 1;
    private NewControls newControls = null;

    private void Awake() => newControls = new NewControls();
    private void OnEnable() => newControls.Newactionmap.Enable();
    private void OnDisable() => newControls.Newactionmap.Disable();
    void FixedUpdate() => Move();
    

    private bool canClimb = false;
    private Vector3 moveMent = new Vector3();
    private Rigidbody2D r_2d;
    // Start is called before the first frame update
    void Start()
    {
        r_2d = GetComponent<Rigidbody2D>();
        AddNotificationObserver();
        //NotificationCenter.Default.Post(this, NotificationKeys.MissionInfoRefresh);
    }

    // Update is called once per frame


    private void Move() {
        var mInput = newControls.Newactionmap.Newaction.ReadValue<Vector2>();
        
        if (canClimb)
        {
            moveMent = new Vector3
            {
                x = mInput.x,
                y = mInput.y,
            }.normalized;
        }
        else
        {
            moveMent = new Vector3
            {
                x = mInput.x,
            }.normalized;
        }
       
        //Debug.Log(" mInput.x" + mInput.x);
        transform.Translate(moveMent * speed * Time.deltaTime);
        //GetComponent<Rigidbody2D>().pos
    }

    #region Notification
    void AddNotificationObserver()
    {
        NotificationCenter.Default.AddObserver(this, NotificationKeys.InTheLadder);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.OutTheLadder);
    }

    void RemoveNotificationObserver()
    {
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.InTheLadder);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.OutTheLadder);
    }

    public void OnNotify(Notification _noti)
    {
        if (_noti.name == NotificationKeys.InTheLadder)
        {
            canClimb = true;
            r_2d.gravityScale = 0;
        }
        if (_noti.name == NotificationKeys.OutTheLadder)
        {
            canClimb = false;
            r_2d.gravityScale = 1;
        }
    }
    #endregion
}
