using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public Transform player1pos;
    public Transform player2pos;
    public GameObject player1;
    public GameObject player2;
    // Start is called before the first frame update
    void Awake()
    {
        if(player1 && player2)
        {
            GameObject p1 = Instantiate(player1, player1pos);
            p1.transform.position = player1pos.position;
            GameObject p2 = Instantiate(player2, player2pos);
            p2.transform.position = player2pos.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
