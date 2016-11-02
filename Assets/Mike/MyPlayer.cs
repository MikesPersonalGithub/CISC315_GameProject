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

    public float x_thickness = 0.25f;
    public float z_thickness = 0.25f;

    // Members
    private Vector3 direction;                          // direction in which player is moving

    // Audio Members
    private AudioSource[] sounds;

    // Time Members
    float time_elapsed = 0.0f;

    // State Members
    float fstate = 0.0f;

    // Use this for initialization
    private void Start()
    {
        // initializations
        direction = new Vector3(1, 0, 0);
        sounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        // player motion
        Process_Horizontal_Motion();
        Process_Vertical_Motion();
    }

    private void Process_Horizontal_Motion()
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

    private void Process_Vertical_Motion()
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

    public void PlaySound_HitByVillain()
    {
        PlaySound(0);
    }

    private void PlaySound(int index)
    {
        sounds[index].Play();
    }


}
