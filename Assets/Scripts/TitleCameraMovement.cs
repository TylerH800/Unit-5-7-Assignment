using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public enum CameraStates
{ 
    main,
    options,
    instructions,
    levelSelect
}

public class TitleCameraMovement : MonoBehaviour
{
    CameraStates camState;

    public Transform title, options, instructions, levelSelect;
    //title rotation
    public float titleRotSpeed = 1f;
    public float rotAngleMin;
    public float rotAngleMax;
 

    //movement between states
    Vector3 vel = Vector3.zero;


    public float speed = 3;
    public float rotSpeed = 9;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camState = CameraStates.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    void StateMachine()
    {
        
        if (camState == CameraStates.main)
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

 
    public void TitleRotation()
    {
        camState = CameraStates.main;

        
        transform.position = Vector3.SmoothDamp(transform.position, title.position, ref vel, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, title.rotation, rotSpeed * Time.deltaTime);

  
        float rY = Mathf.SmoothStep(rotAngleMin, rotAngleMax, Mathf.PingPong(Time.time * titleRotSpeed, 1));
        transform.rotation = Quaternion.Euler(15, rY, 0);


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
