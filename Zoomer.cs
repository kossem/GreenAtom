using UnityEngine;
using System.Collections;

public class Zoomer : MonoBehaviour {
    public float zoomAmount, maxToClamp, rotSpeed;

    // Use this for initialization
    void Start () {
	    
	}

    // Update is called once per frame
    void Update()
    {
        zoomAmount += Input.GetAxis("Mouse ScrollWheel");
        zoomAmount = Mathf.Clamp(zoomAmount, -maxToClamp, maxToClamp);
        var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), maxToClamp - Mathf.Abs(zoomAmount));
        gameObject.transform.Translate(0, 0, translate * rotSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")));
    }
}
