using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<FixableTrigger> FixableObjs;
    public bool StartPlay = false;
    public int MonsterHp = 5;
    public int RobotHp = 5;
    public Text MonsterHpText;
    public Text RobotHpText;
    public SpriteRenderer Cover, Win, Lose;
    public GameObject RestartButton;

    private bool first = true;


    public void OnEnable()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        RestartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartPlay)
        {

        }
        MonsterHpText.text = "Monster: " + MonsterHp;
        RobotHpText.text = "Robot: " + RobotHp;
    }
    public void Play()
    {
        Debug.Log("Play");
        StartPlay = true;
        StartCoroutine(GameplayLoop());
        NotificationCenter.Default.Post(this, NotificationKeys.GameStart);
    }
    IEnumerator GameplayLoop()
    {
        float t = 5;
        if (!first)
            t = Random.Range(9f, 12f);
        yield return new WaitForSeconds(t);
        if (!StartPlay) yield break;

        List<FixableTrigger> GoodItems = new List<FixableTrigger>();
        foreach (var item in FixableObjs)
        {
            if (!item.NeedRepair)
                GoodItems.Add(item);
        }
        if (GoodItems.Count > 0)
        {
            int r = Random.Range(0, GoodItems.Count);
            GoodItems[r].Broken();
            CameraShake(1, new Vector3(0.3f, 0.3f, 0));
        }
        StartCoroutine(GameplayLoop());
        yield break;
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
        StartPlay = false;
        NotificationCenter.Default.Post(this, NotificationKeys.GameOver);
        CameraShake(5, new Vector3(0.3f, 0.3f, 0));
        Cover.DOFade(1, 5).OnComplete(() =>
        {
            Cover.DOFade(0, 2);
            if (MonsterHp > RobotHp)//lose
            {
                Win.DOFade(0, 0);
                Lose.DOFade(1, 0);
            }
            else if (MonsterHp < RobotHp)//win
            {
                Win.DOFade(1, 0);
                Lose.DOFade(0, 0);
            }
            RestartButton.SetActive(true);
        });
        
    }

    public void CameraShake(float t, Vector3 force)
    {
        Camera.main.transform.DOShakePosition(t, force);
    }
    public void MonsterHurt()
    {
        MonsterHp--;
        NotificationCenter.Default.Post(this, NotificationKeys.MonsterHurt);
        if (MonsterHp <= 0)
            GameOver();
    }
    public void RobotHurt()
    {
        RobotHp--;
        NotificationCenter.Default.Post(this, NotificationKeys.RobotHurt);
        if (RobotHp <= 0)
            GameOver();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("Map");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerInspector : Editor
{
    GameManager gm;
    private void OnEnable()
    {
        gm = (GameManager)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Start"))
        {
            gm.Play();
        }
    }
}
#endif
