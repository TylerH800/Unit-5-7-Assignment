using UnityEngine;

public class Fading : MonoBehaviour
{
    public CanvasGroup group;
    public GameObject obj; //the title interface

    public float speed = 2;

    private bool fadeIn = false;
    private bool fadeOut = false;
    
    //these methods are called to start the fading of the title screen
    public void FadeIn()
    {
        obj.SetActive(true);
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    void Update()
    {
        if (fadeIn)
        {           
            if (group.alpha < 1)
            {
                //fades in the interface
                group.alpha += Time.deltaTime * speed;
            }
            else
            {
                //when it is fully faded in, it can be interacted with
                group.interactable = true;
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            if (group.alpha > 0)
            {
                //stops interaction and fades out
                group.interactable = false;
                group.alpha -= Time.deltaTime * speed;
            }
            else
            {
                //if fully faded out, the object is deactivated                
                fadeOut = false;
                obj.SetActive(false);
            }
        }
    }
}
