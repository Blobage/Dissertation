using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    CursorLockMode CurrentMode;
    bool stateSelect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       if(Input.GetKey(KeyCode.Escape))
        {
            stateSelect = true;
        }
        CursorState();

	}

    void CursorState()
    {
        if(stateSelect == true)
        {
            CurrentMode = CursorLockMode.Confined;
            Cursor.lockState = CurrentMode;
            Cursor.visible = false;
        }
        else
        {
            CurrentMode = CursorLockMode.None;
            Cursor.lockState = CurrentMode;
            Cursor.visible = true;
        }
        
    }
}
