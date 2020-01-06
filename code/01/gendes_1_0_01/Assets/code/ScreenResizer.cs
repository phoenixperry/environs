using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResizer : MonoBehaviour
{
    public double screenRatio = 1.6;  
    private Color colors;
    // Start is called before the first frame update

    public GameObject scalingCube; 

    void Start()
    {
        setObjectToWidthAndHeightOfScreen(gameObject);
        Renderer r = gameObject.GetComponent<Renderer>();
        
        colors = r.material.color; 
    }

    // Update is called once per frame
    void Update()
    {
       GameObject scaledBG =  setObjectToWidthAndHeightOfScreen(gameObject);
        float H, S, V; 
        Color.RGBToHSV(new Color(colors.r, colors.b, colors.g), out H, out S, out V); 
        S= ((float)Input.mousePosition.x/(float)Screen.width);
        H = ((float)Input.mousePosition.y / (float)Screen.height);
       // V = ((float)Input.mousePosition.y / (float)Screen.height);

        //Debug.Log(Input.mousePosition.x + " I am the size of the screen width " + Input.mousePosition.x/Screen.width);

        gameObject.GetComponent<Renderer>().material.color= Color.HSVToRGB(H,.88f, .88f);
        //Debug.Log(gameObject.GetComponent<Renderer>().material.color);

        scalingCube.GetComponent<Transform>().localScale = new Vector3(((float)Input.mousePosition.x /(float)Screen.width*scaledBG.GetComponent<Transform>().localScale.x), ((float)Input.mousePosition.x / (float)Screen.width * scaledBG.GetComponent<Transform>().localScale.x), 1.0f);

        scalingCube.GetComponent<Renderer>().material.color =Color.HSVToRGB(1.0f*Input.mousePosition.y/2 / Screen.height, .82f, .82f);
    }

   
    //Creative Coding helper functions 
    Vector3 GetSizeOfObjectInUnits(GameObject go)
    {
        Mesh cubeMesh = go.GetComponent<MeshFilter>().mesh;
        Bounds bounds = cubeMesh.bounds;
        // X size in units
        float boundsX = transform.localScale.x * bounds.size.x;
        //y size in units 
        float boundsY = transform.localScale.y * bounds.size.y;
        Debug.Log("I am the size in x,y pixels " + boundsX + " " + boundsY);
        //z size in units 
        float boundsZ = transform.localScale.z * bounds.size.z;
        return (new Vector3(boundsX, boundsY, boundsZ));
    }

    GameObject setObjectToWidthAndHeightOfScreen(GameObject go) {
        // function assumes a local scale of 1  
        go.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f); 
        //For an orthographic camera, the Camera.orthographicSize is 1 / 2 of the height of the window seen by the camera. Multiplying by 2 gives you the total width 
        double width = Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height;
        //So to make a non - rotated cube the screen width(assuming no parent, or parents with localScale of (1,1,1)), you can set the localSize:
        //the height can be found as a ratio of the width as we're working with a 16:10 aspect ratio 
        double height = (Camera.main.orthographicSize * 2.0 * Screen.width / Screen.height) / screenRatio;

        //you need to convert to float for localscale Vector 3 - it was giving me static in the call itself so I just did it above it
        float h2 = (float)height;
        float w2 = (float)width;
        gameObject.transform.localScale = new Vector3(w2, h2, 1.0f);
        return go; 
    }
}
