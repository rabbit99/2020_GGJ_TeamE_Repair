using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterController : MonoBehaviour
{
    public float Rate = 30;
    public int lv = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        lv = Mathf.Clamp(lv, 0, 2);
        var emission = GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = Rate*(float)lv/2;
    }
    public void SetLv(int i)
    {
        lv = i;
    }

}
