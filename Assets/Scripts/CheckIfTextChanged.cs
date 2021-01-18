using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckIfTextChanged : MonoBehaviour
{
    string text = "";
    string textLast;
    bool startAnim;
    float size;

    // Update is called once per frame
    void Update()
    {
        if(startAnim){
            AnimateFontSize();
        }
        // text = GetComponent<Text>().text;
        // if(text != textLast)
        // {
            
            
        // }
        // textLast = GetComponent<Text>().text;
    }
    void AnimateFontSize(){
        size-=0.01f;
        transform.localScale = new Vector3 (size, size, size);
        if(transform.localScale.x <= 1f){
            transform.localScale = new Vector3 (1, 1, 1);
            startAnim = false;
        }
    }  
    public void StartAnimating(){
        size = 1.2f;
        startAnim = true;
    } 
}
