using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
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
			
        }
		//if (doorIsOpening = false){
		//	closedoor();
		//}
    }
	void closedoor(){
		Door.transform.Translate(Vector3.down * Time.deltaTime * 180);
	}
	
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            doorIsOpening = true;
        }
    }
    void OnMouseDown()
    { //THIS FUNCTION WILL DETECT THE MOUSE CLICK ON A COLLIDER,IN OUR CASE WILL DETECT THE CLICK ON THE BUTTON
        doorIsOpening = false;
		closedoor();
        //if we click on the button door we must start to open
    }

}
