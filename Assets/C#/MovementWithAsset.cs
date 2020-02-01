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
    void Update() => Move();

    // Start is called before the first frame update
    void Start()
    {
        AddNotificationObserver();
        //NotificationCenter.Default.Post(this, NotificationKeys.MissionInfoRefresh);
    }

    // Update is called once per frame


    private void Move() {
        var mInput = newControls.Newactionmap.Newaction.ReadValue<Vector2>();

        var moveMent = new Vector3
        {
            x = mInput.x,
            y = mInput.y,
        }.normalized;
        //Debug.Log(" mInput.x" + mInput.x);
        transform.Translate(moveMent * speed * Time.deltaTime);
    }

    #region Notification
    void AddNotificationObserver()
    {
        //NotificationCenter.Default.AddObserver(this, NotificationKeys.LeaveShop);
        //NotificationCenter.Default.AddObserver(this, NotificationKeys.StageCountChange);
        //NotificationCenter.Default.AddObserver(this, NotificationKeys.QueryStagesFinished);
    }

    void RemoveNotificationObserver()
    {
        //NotificationCenter.Default.RemoveObserver(this, NotificationKeys.LeaveShop);
        //NotificationCenter.Default.RemoveObserver(this, NotificationKeys.StageCountChange);
        //NotificationCenter.Default.RemoveObserver(this, NotificationKeys.QueryStagesFinished);
    }

    public void OnNotify(Notification _noti)
    {
        //if (_noti.name == NotificationKeys.StageCountChange)
        //{
        //    //Debug.Log("StageCountChange " + (int)_noti.data);
        //    InputStageCount_ValueChanged((int)_noti.data);

        //}
        //else if (_noti.name == NotificationKeys.LeaveShop)
        //{
        //    mSetStage.RefreshLimit();
        //    moneyLabel.text = Main.instance.ParseDataMgr.userData.Money.ToString();

        //}
        //else if (_noti.name == NotificationKeys.QueryStagesFinished)
        //{
        //    //更新stage count ui
        //    //Debug.Log("QueryStagesFinished " + (int)_noti.data);
        //    InputStageCount_ValueChanged((int)_noti.data);
        //    isSaveAsDraft = true;
        //}
    }
    #endregion
}
