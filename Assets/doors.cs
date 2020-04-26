using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doors : MonoBehaviour
{

    public GameObject Door;
    public bool doorIsOpening;

    void Update()
    {
        if (doorIsOpening == true)
        {
            Door.transform.Translate(Vector3.up * Time.deltaTime * 5);
            //if the bool is true open the door

        }
        if (Door.transform.position.y > 7f)
        {
            doorIsOpening = false;
            //if the y of the door is > than 7 we want to stop the door
			Door.transform.Translate(Vector3.down * Time.deltaTime * 5);

        }
    }
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            doorIsOpening = true;
        }
    }


}

