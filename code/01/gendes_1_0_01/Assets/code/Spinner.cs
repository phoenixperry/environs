using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    private float radius = 1.0f;
    private int steps = 10;

    private LineRenderer line;
    Vector3 positions;
    private float multiplier;
    private float lineLength;
    public Vector3 lineStart;
    public List<Vector3> pointsOnCircle;
    float x, y, z = 0.0f;
    private Vector3 currentPoint;
    // Start is called before the first frame update

    public float xradius;
    public float yradius;

    void Start()
    {
 


        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = steps + 1;
        line.useWorldSpace = false;
        CreatePoints(steps);
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            steps++;
      

        }
        else if(Input.GetKeyUp(KeyCode.DownArrow)) {
            steps--;
    
        }
    
      
        Vector3 pt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        radius = 0.0f + pt.x/2;
        CreatePoints(steps);


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    void CreatePoints(int segments)
    {
        line.positionCount=0;
        line.positionCount=segments+1;
        float x;
        float y;
        float z = 0f;

        float angle = 20f;
        
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;


            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}

