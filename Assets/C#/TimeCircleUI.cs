using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCircleUI : MonoBehaviour
{
    public Image Circle;
    // Start is called before the first frame update
    void Start()
    {
        Circle = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
