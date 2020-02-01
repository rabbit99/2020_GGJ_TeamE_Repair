using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithAsset : MonoBehaviour
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
}
