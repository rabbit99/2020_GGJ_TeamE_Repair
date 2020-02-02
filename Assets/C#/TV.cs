using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.Video;

public class TV : MonoBehaviour, INotification
{
    public VideoClip[] clip = new VideoClip[10];
    public VideoPlayer VP;
    public VideoClip MonsterHurtClip;
    public VideoClip RobotHurtClip;

    private bool playResult = false;
    // Start is called before the first frame update
    void Start()
    {
        VP = gameObject.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playResult)
            return;
        if ((ulong)VP.frame >= VP.frameCount-10 && (ulong)VP.frame<1000)
        {
            print("1");
            VideoClip temp = VP.clip;
            while(VP.clip == temp)
            {
                VP.clip = clip[Random.Range(0, clip.Length)];
                VP.frame = 1;
            }
            print(VP.clip);
        }
    }

    private void PlayMonsterHurtClip() {
        if (MonsterHurtClip == null)
            return;
        VP.clip = MonsterHurtClip;
        VP.frame = 1;
        StartCoroutine(callBack());
    }

    private void PlayRobotHurtClip()
    {
        if (RobotHurtClip == null)
            return;
        VP.clip = RobotHurtClip;
        VP.frame = 1;
        StartCoroutine(callBack());
    }

    IEnumerator callBack()
    {
        while ((ulong)VP.frame >= VP.frameCount - 10 && (ulong)VP.frame < 1000) // while the movie is playing
        {
            yield return new WaitForEndOfFrame();
        }
        // after movie is not playing / has stopped.
        setFlag();
    }

    private void setFlag()
    {
        playResult = false;
    }

    #region Notification
    void AddNotificationObserver()
    {
        NotificationCenter.Default.AddObserver(this, NotificationKeys.MonsterHurt);
        NotificationCenter.Default.AddObserver(this, NotificationKeys.RobotHurt);
    }

    void RemoveNotificationObserver()
    {
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.MonsterHurt);
        NotificationCenter.Default.RemoveObserver(this, NotificationKeys.RobotHurt);
    }

    public void OnNotify(Notification _noti)
    {
        if (_noti.name == NotificationKeys.InTheLadder)
        {
            playResult = true;
        }
        if (_noti.name == NotificationKeys.InTheLadder)
        {
        }
    }
    #endregion
}
