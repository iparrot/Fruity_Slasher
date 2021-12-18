using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSlasher : MonoBehaviour
{
    Game game;
    [SerializeField] Camera cam;
    [SerializeField] ParticleSystem touch_PS;
    [SerializeField] HealthManager healthManager;
    
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("EventSystem").GetComponent<Game>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.isGameStarted)
        {
            SlashCheck();
        }
    }

    void SlashCheck()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit, 40))
            {
                //Debug.Log("Mouse down + " + hit.point + " Object: " + hit.transform.name + "\t Mouse position: " + Input.mousePosition + "\t Ray position: " + ray.origin);
                if (hit.transform.CompareTag("Fruit"))
                {
                    if (hit.transform.TryGetComponent<Fruit>(out Fruit _comp))
                    {
                        hit.transform.GetComponent<Fruit>().SlashMe();
                    }
                    else
                    {
                        hit.transform.parent.GetComponent<Fruit>().SlashMe();
                    }
                    
                }
                else if(hit.transform.CompareTag("Bomb"))
                {
                    hit.transform.GetComponent<Bomb>().SlashMe();
                }
            }

            touch_PS.transform.position = hit.point;
            if (touch_PS.transform.position == Vector3.zero)
            {
                touch_PS.transform.position = transform.position;
            }
            //Debug.Log(Input.mousePosition.)
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            touch_PS.Play();
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            touch_PS.Stop();
        }
    }
}
