using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //Editable Stuff:
    public  int numberOfLevels;
    public  float scrollSpeed, zoomSpeed;

    //Code Stuff:
    private bool mapOpen;
    private int level = 0;
    private float zoom = .5f;
    private Vector2 position;
    private GameObject mapStuff;

    private float relativeScrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mapStuff = GameObject.Find("Map");
        mapOpen = false;

        position = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapOpen = !mapOpen;
        }

        if (mapOpen)
        {
            
            MapInputs();
            UpdateMap();
        }
        else
        {
            
            CloseMap();
        }
    }

    void MapInputs()
    {
        relativeScrollSpeed = scrollSpeed * (1 / zoom);

        //The directions are backwards because when you hold the UP key the picture itself moves DOWN in your eyes, because in this case we are controlling the camera not the pic
        //Time.deltaTime is just time since last frame and keeps speeds consistent from frame rate
        if (Input.GetKey(KeyCode.UpArrow))
        {
            position = position + (Vector2.down * relativeScrollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            position = position + (Vector2.up * relativeScrollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position = position + (Vector2.right * relativeScrollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position = position + (Vector2.left * relativeScrollSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            zoom = Mathf.Clamp(zoom + (zoomSpeed * Time.deltaTime), 0.1f, 1);

        }
        if (Input.GetKey(KeyCode.S))
        {
            zoom = Mathf.Clamp(zoom - (zoomSpeed * Time.deltaTime), 0.1f, 1);

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            level = Mathf.Clamp(level - 1, 0, numberOfLevels);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            level = Mathf.Clamp(level + 1, 0, numberOfLevels);
        }
    }

    private void UpdateMap()
    {
        mapStuff.SetActive(true);

        //update map
    }

    private void CloseMap()
    {
        mapStuff.SetActive(false);

    }
}
