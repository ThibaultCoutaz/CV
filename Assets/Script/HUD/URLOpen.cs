using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOpen : MonoBehaviour {

    public string URL;
    
    public void Open()
    {
        Application.ExternalEval("window.open('"+URL+"','_blank')");
    }
}
