using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColdDown : MonoBehaviour
{
    private Image target;
    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            target.fillAmount = target.fillAmount - Time.deltaTime;
        }
        else
        {
            target.fillAmount = 1;
        }
    }
    
    public void StartRepair()
    {
        flag = true;
    }

    public void EndRepair()
    {
        flag = false;
    }
}
