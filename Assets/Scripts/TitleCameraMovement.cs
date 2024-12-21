using Mono.Cecil.Cil;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public enum CameraStates
{ 
    goingToTitle,
    options,
    instructions,
    levelSelect,
    rotating
    
}

public class TitleCameraMovement : MonoBehaviour
{
    CameraStates camState;

    public Transform title, options, instructions, levelSelect;
    //title rotation
    public float titleRotSpeed = 1f;
    public float rotAngleMin;
    public float rotAngleMax;

    float rY;
    float time;

    //movement between states
    Vector3 vel = Vector3.zero;

    public float speed = 3;
    public float rotSpeed = 9;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camState = CameraStates.goingToTitle;
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        
    }

    void StateMachine()
    {
        if (camState == CameraStates.rotating)
        {
            Rotating();
        }

        if (camState == CameraStates.goingToTitle)
        {
            TitleRotation();
        }

        if (camState == CameraStates.options)
        {
            Options();
        }

        if (camState == CameraStates.levelSelect)
        {
            LevelSelect();
        }

        if (camState == CameraStates.instructions)
        {
            Instructions();
        }

    }

    void Rotating()
    {
        time += Time.deltaTime;
        //Debug.Log(time);
        rY = Mathf.SmoothStep(rotAngleMin, rotAngleMax, Mathf.PingPong(time * titleRotSpeed, 1));
        transform.rotation = Quaternion.Euler(15, rY, 0);
    }

    public void TitleRotation()
    {        
        camState = CameraStates.goingToTitle;

        

        transform.position = Vector3.SmoothDamp(transform.position, title.position, ref vel, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, title.rotation, rotSpeed * Time.deltaTime);
        
        if(Vector3.Distance(transform.position, title.position) < 0.05f)
        {
            camState = CameraStates.rotating;
            time = 0;

        } 


    }

    public void LevelSelect()
    {
        camState = CameraStates.levelSelect;

        transform.position = Vector3.SmoothDamp(transform.position, levelSelect.position, ref vel, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, levelSelect.rotation, rotSpeed * Time.deltaTime);
    }

    public void Options()
    {
        camState = CameraStates.options;

        transform.position = Vector3.SmoothDamp(transform.position, options.position, ref vel, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, options.rotation, rotSpeed * Time.deltaTime);
    }

    public void Instructions()
    {
        camState = CameraStates.instructions;

        transform.position = Vector3.SmoothDamp(transform.position, instructions.position, ref vel, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, instructions.rotation, rotSpeed * Time.deltaTime);
        
    }


}
