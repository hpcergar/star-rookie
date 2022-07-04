using UnityEngine;

public class UserInterfaceInput : MonoBehaviour
{
    void Start() 
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
