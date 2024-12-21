using UnityEngine;

public class Fading : MonoBehaviour
{
    public CanvasGroup group;
    public GameObject obj;

    public float speed = 2;

    private bool fadeIn = false;
    private bool fadeOut = false;
    
    public void FadeIn()
    {
        obj.SetActive(true);
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    private void Update()
    {
        if (fadeIn)
        {           
            if (group.alpha < 1)
            {
                group.interactable = false;
                group.alpha += Time.deltaTime * speed;
            }
            else
            {
                group.interactable = true;
                fadeIn = false;
            }
        }

        if (fadeOut)
        {
            if (group.alpha > 0)
            {
                group.interactable = false;
                group.alpha -= Time.deltaTime * speed;
            }
            else
            {
                group.interactable = true;
                fadeOut = false;
                obj.SetActive(false);
            }
        }
    }
}
