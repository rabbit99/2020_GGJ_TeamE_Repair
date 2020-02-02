using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCircleUI : MonoBehaviour
{
    public Image Circle;
    private void Awake()
    {
        Circle = transform.GetChild(0).GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
