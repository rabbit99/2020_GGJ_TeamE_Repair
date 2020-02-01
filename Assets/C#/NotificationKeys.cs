
public class NotificationKeys
{
	public const string RefreshMoney = "RefreshMoney";						//Money刷新

    // Map
	public const string MapEnable = "MapEnable";            				//Map Enable
	public const string MapDisable = "MapDisable";							//Map Disable
	public const string SwitchMapMode = "SwitchMapMode";					//轉換地圖模式 2D/3D
	public const string MarkerEnable = "MarkerEnable";						//Marker Enable
	public const string MarkerDisable = "MarkerDisable";					//Marker Disable
	public const string ShowSelectLocationUI = "ShowSelectLocationUI";      //開 選位置UI
	public const string CloseSelectLocationUI = "CloseSelectLocationUI";    //關 選位置UI
	
	// Mission
	public const string MissionItemDelete = "MissionItemDelete";            //Mission>前3
	public const string MissionInfoRefresh = "MissionInfoRefresh";          //設定mission小圖
	public const string DeleteMessage = "DeleteMessage";                    //刪除留言

	//TreasureMap
	public const string TreasureMapRefresh = "TreasureMapRefresh";						//藏寶圖重新整理
	public const string TreasureMapFinish = "TreasureMapFinish";                        //藏寶圖秀Finish徽章
	public const string TreasureMapInfoOpen = "TreasureMapInfoOpen";                    //藏寶圖打開關卡簡單資訊
	public const string TreasureMapInfoClose = "TreasureMapInfoClose";					//藏寶圖關閉關卡簡單資訊

	//Stage
	public const string StageCountChange = "StageCountChange";								//Mission要AddStageCount
	public const string EditingStageFail = "EditingStageFail";                          //Stage編輯失敗
	public const string DeleteStage = "DeleteStage";								    //Stage刪除

	//Shop
	public const string LeaveShop = "LeaveShop";										//離開商店
	public const string Purchase = "Purchase";											//購買物品

	//User
	public const string EditCompleted = "EditCompleted";								//編輯完成

	//MissionCreate
	public const string CloseMissionCreateWindow = "CloseMissionCreateWindow";			//關閉了MissionCreateWindow
	public const string QueryStagesFinished = "QueryStagesFinished";                    //query 這個mission的stage

	//MissionInfo
	public const string CloseMissionInfoWindow = "CloseMissionInfoWindow";              //關閉了MissionInfoWindow
    public const string ListOfPassesObjectRefreshData = "ListOfPassesObjectRefreshData";//刷新過關名單

    //MissionList
    public const string MyMissionItemObjectRefresh = "MyMissionItemObjectRefresh";      //更新MyMissionItemObject
	public const string ShowDeleteMode = "ShowDeleteMode";								//欄位開刪除按鈕
	public const string CloseDeleteMode = "CloseDeleteMode";                            //欄位關刪除按鈕
	public const string SetMissionOpenLabelt = "SetMissionOpenLabelt";					//重設任務計算開關數

    //Notice
    public const string NoticeItemDelete = "NoticeItemDelete";                          //刪除通知
    public const string ShowAnnounceDetails = "ShowAnnounceDetails";                    //打開活動細節視窗

    //Badge
    public const string BadgeRefresh = "BadgeRefresh";                                  //刷新紅點

    //Gift
    public const string GiftRefresh = "GiftRefresh";                                  //刷新紅點

    //Ladder
    public const string LadderTrigger = "LadderTrigger";                            //觸碰到梯子
    public const string LadderLeave = "LadderLeave";                                //離開梯子
}