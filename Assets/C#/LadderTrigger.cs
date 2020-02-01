using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.InTheLadder, collision.gameObject.name);
            Debug.Log("Ladder Trigger In, " + collision.gameObject.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        MovementWithAsset movement = collision.GetComponent<MovementWithAsset>();
        if (movement != null)
        {
            NotificationCenter.Default.Post(this, NotificationKeys.OutTheLadder);
            Debug.Log("Ladder Trigger Out, " + collision.gameObject.name);
        }
    }
}
