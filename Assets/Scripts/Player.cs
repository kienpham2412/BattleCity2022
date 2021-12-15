using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerControl controls;
    
    void Awake() {
        controls = new PlayerControl();
        // controls.UINavigation.Up.performed += arg => CalledFunc();
    }

    void CalledFunc(){
        Debug.Log("You pressed a key");
    }

    private void OnEnable() {
        // controls.UINavigation.Enable();
    }

    private void OnDisable() {
        // controls.UINavigation.Disable();
    }
}
