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

    int hoverI = 0;
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
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
                InvestigateClicked();
                interactionPieGO.SetActive(false);
                hoverI = 0;
            }
        }
        else if (hoverI == 2)
        {
            if (Input.GetMouseButtonUp(1))
            {
                UseClicked();
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
        //name = name.Replace("_", "");
        // if (name.Contains("patio"))
        //     return "patio";
        return name;
    }
    public void InvestigateHover()
    {
        hoverI = 1;
    }
    void InvestigateClicked()
    {
        string rcHit = formatInfo(RayFromMouse().name);
        print(rcHit + "has been Investigated");
        if (rcHit == "Patio")
        {
            mainTextSpeak.text = "Hmm... Just a thing to stand on.";
        }
        else if (rcHit == "HotDogStand")
        {
            mainTextSpeak.text = "Yummy, I want a Hotdog!";
        }
    }
    public void UseHover()
    {
        hoverI = 2;
    }
    void UseClicked()
    {
        GameObject rcHitGO = RayFromMouse();
        if (rcHitGO.layer != 9)
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
        string rcHit = formatInfo(RayFromMouse().name);
        print(rcHit + "has been Used");
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
