using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Raycast : MonoBehaviour
{
    public float raycastDistance = 10f;
    public bool raycastActive = true;
    public Transform centerPoint;
    public PlayerController playerScript;

    private NPCDialogue npc;


    void Start()
    {
            
    }

    void Update()
    {

        // Centro da tela (mira)
        Vector3 centroTela = new Vector3(Screen.width / 2, Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(centroTela);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {



            if (hit.collider.tag == "Collectibles")
            {
                Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.yellow);

                // PEGANDO UM ITEM PARA O INVETÁRIO
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Pegou o item " + hit.collider.name);
                    Debug.Log("parent -> gameobject: " + hit.collider.gameObject);
                    playerScript.addItemInventory(hit.collider.gameObject);
                }

            }
            else if (!hit.collider.GetComponent<SphereCollider>() && hit.collider.tag == "NPC")
            {
                Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.yellow);

                npc = hit.collider.GetComponent<NPCDialogue>();
                npc.canTalk = true;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.blue);
            }

        }
        else
        {
            if (npc)
            {
                npc.canTalk = false;
            }

            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
        }


        // ADICIONADO ITEM NO CHÃO
        if (Input.GetMouseButtonDown(1))
        {
            List<GameObject> inventory = playerScript.inventory;

            if (inventory.Count > 0)
            {
                GameObject item = inventory[0];
                Instantiate(item);
                item.SetActive(true);
                item.transform.position = new Vector3(-5, 5, -10);

            }
        }

    }
}
