using UnityEngine;
using System.Collections;

public class MyPlayer : MonoBehaviour
{
    // Tweaks
    public char RightMove = 'd';
    public char LeftMove = 'a';
    public char UpMove = 'w';
    public char DownMove = 's';
    public float speed = 1.0f;

    // Members
    private Vector3 direction;          // direction in which player is moving

    // Use this for initialization
    void Start()
    {
        // initializations
        direction = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // player motion
        Process_Horizontal_Motion();
        Process_Vertical_Motion();
    }

    void Process_Horizontal_Motion()
    {
        if (Input.GetKey(RightMove.ToString()))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(LeftMove.ToString()))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
    }

    void Process_Vertical_Motion()
    {
        if (Input.GetKey(UpMove.ToString()))
        {
            transform.Translate(0, 0,speed * Time.deltaTime);
        }
        else if (Input.GetKey(DownMove.ToString()))
        {
            transform.Translate(0, 0,-speed * Time.deltaTime);
        }
    }
}
