using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickWebButton : MonoBehaviour
{
    public GameObject obj;

    public void WebButtonEvent() {
        Connection con = obj.GetComponent<Connection>();
        if(!con.isLocal)
            Application.OpenURL(Constants.MAIN_URL); 
        else 
            Application.OpenURL(Constants.LOCAL_URL);
    }

}
