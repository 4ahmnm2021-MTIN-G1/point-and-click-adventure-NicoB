using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    ThirdPersonCharacter character;
    [SerializeField] Text textSpeakGO;
    void Start()
    {
        cam = Camera.main;
        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        character = this.GetComponent<ThirdPersonCharacter>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string clickedObject = hit.transform.name;
                if (clickedObject.Contains("Patio") || clickedObject.Contains("road") || clickedObject.Contains("Ground"))
                {
                    agent.SetDestination(hit.point);
                }
                else
                {
                    textSpeakGO.text = "I can't walk on this";
                }
            }
        }
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }
}
