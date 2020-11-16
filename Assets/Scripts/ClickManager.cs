using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Globalization;
public class ClickManager : MonoBehaviour
{
    [SerializeField] GameObject interactionPieGO;
    [SerializeField] Text mainTextSpeak;
    [SerializeField] Text mainTextInfo;

    [SerializeField] GameObject playerControllerGO;
    [SerializeField] GameObject brokenCableColGO;
    [SerializeField] GameObject brokenCablePEGO;

    int hoverI = 0;
    string rcHitGlob;
    void LateUpdate()
    {
        //Right click
        if (Input.GetMouseButtonDown(1))
        {
            rcHitGlob = formatInfo(RayFromMouse().name);
            Debug.Log(rcHitGlob);

            interactionPieGO.transform.position = Input.mousePosition;
            interactionPieGO.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1) && hoverI == 0)
        {
            interactionPieGO.SetActive(false);
        }
        else if (hoverI == 1)
        {
            if (Input.GetMouseButtonUp(1))
            {
                InvestigateClicked(rcHitGlob);
                interactionPieGO.SetActive(false);
                hoverI = 0;
            }
        }
        else if (hoverI == 2)
        {
            if (Input.GetMouseButtonUp(1))
            {
                UseClicked(rcHitGlob);
                interactionPieGO.SetActive(false);
                hoverI = 0;
            }
        }
        mainTextInfo.text = formatInfo(RayFromMouse().name);
    }
    string formatInfo(string name)
    {
        name = Regex.Replace(name, @"[\d-]", string.Empty);
        name = Regex.Replace(name, @"[_()]", string.Empty);
        char[] nameA = name.ToCharArray();
        nameA[0] = char.ToUpper(nameA[0]);
        name = new string(nameA);
        return name;
    }
    public void InvestigateHover()
    {
        hoverI = 1;
    }
    int personsTalkedTo;
    bool watchSelled;
    bool ingredientsPickedUp;
    bool cableFixed;
    void InvestigateClicked(string rcHit)
    {
        print(rcHit + " has been Investigated");
        if (rcHit == "Patio" || rcHit == "Ground")
        {
            mainTextSpeak.text = "Hmm... Just a thing to stand on.";
        }
        else if (rcHit == "HotDogStand")
        {
            if (!watchSelled)
            {
                mainTextSpeak.text = "Yummy, I want a Hotdog! But I need money...";
            }
            else if (watchSelled && !ingredientsPickedUp)
            {
                mainTextSpeak.text = "Now I got money, but the cook says he does not have enough ingredients. I should help him";
            }
            else if (watchSelled && ingredientsPickedUp)
            {
                mainTextSpeak.text = "Now he got the ingredients but says that some cable is broken. When will I get my hotdog!?!?";
            }
            else if (watchSelled && ingredientsPickedUp && cableFixed)
            {
                mainTextSpeak.text = "Now I have to get rid of the people";
            }
        }
        else if (rcHit == "Person")
        {
            mainTextSpeak.text = "Just a normal person like me...";
        }
        else if (rcHit == "Ingredients")
        {
            ingredientsPickedUp = true;
            mainTextSpeak.text = "Now I have to bring them to the hotdogstand!";
        }
        else if (rcHit == "BrokenCable")
        {
            if (ingredientsPickedUp)
            {
                mainTextSpeak.text = "I could repair this";
            }
            else
            {
                mainTextSpeak.text = "I have to do other things first";
            }
        }
    }
    public void UseHover()
    {
        hoverI = 2;
    }
    void UseClicked(string rcHit)
    {
        GameObject rcHitGO = RayFromMouse();
        print(rcHit + " has been Used");
        if (rcHit == "Person")
        {
            personsTalkedTo++;
            if (personsTalkedTo <= 2)
            {
                mainTextSpeak.text = "This guy doesn't want to sell anything, maybe I should try somewhere else";
            }
            else if (personsTalkedTo <= 2)
            {
                mainTextSpeak.text = "I don't have more watches!";
            }
            else
            {
                watchSelled = true;
                mainTextSpeak.text = "Yes, this guy wanted my watch! Now I have money.";
            }
        }
        else if (rcHit == "Ingredients")
        {
            if (watchSelled)
            {
                mainTextSpeak.text = "Maybe I should take this";
            }
            else
            {
                SetRandomDenial();
            }
        }
        else if (rcHit == "BrokenCable")
        {
            if (ingredientsPickedUp)
            {
                cableFixed = true;
                Destroy(brokenCableColGO);
                Destroy(brokenCablePEGO);
                mainTextSpeak.text = "Nice, it works now!";
            }
            else
            {
                SetRandomDenial();
            }
        }
        else if (rcHitGO.layer != 9)
        {
            SetRandomDenial();
        }
    }
    void SetRandomDenial()
    {
        int random = Random.Range(0, 4);
        if (random == 0)
        {
            mainTextSpeak.text = "Can't do that";

        }
        else if (random == 1)
        {
            mainTextSpeak.text = "No, sorry!";

        }
        else if (random == 2)
        {
            mainTextSpeak.text = "This doesn't want to be touched";

        }
    }
    GameObject RayFromMouse()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out raycastHit, 100.0f))
        {
            return raycastHit.transform.gameObject;
        }
        return null;
    }
}
