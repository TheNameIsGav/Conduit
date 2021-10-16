using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SceneManagement;

public class Current : MonoBehaviour
{
    [SerializeField]
    private Tilemap ground;

    public int strength = 5; //Distance this current has traveled
    public int power; //Power of the current
    private float currLevel = 1;

    public static Current instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (TryMove(Vector2.up))
            {
                transform.position += (Vector3)Vector2.up;
                strength--;
                if (strength == 0)
                {
                    Respawn();
                }
                CheckCurrentTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (TryMove(Vector2.down))
            {
                transform.position += (Vector3)Vector2.down;
                strength--;
                if (strength == 0)
                {
                    Respawn();
                }
                CheckCurrentTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (TryMove(Vector2.left))
            {
                transform.position += (Vector3)Vector2.left;
                strength--;
                if (strength == 0)
                {
                    Respawn();
                }
                CheckCurrentTile();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (TryMove(Vector2.right))
            {
                transform.position += (Vector3)Vector2.right;
                strength--;
                if (strength == 0)
                {
                    Respawn();
                }
                CheckCurrentTile();
            }
        }

        GetComponent<Light>().intensity = strength;
        if(power == 1)
        {
            GetComponent<Light>().color = new Color(12, 200, 0);
        } else if (power == 2)
        {
            GetComponent<Light>().color = new Color(196, 0, 0);
        } else
        {
            GetComponent<Light>().color = new Color(0, 137, 196);
        }

        
    }

    private void Respawn()
    {
        if(currLevel == 4)
        {
            SceneManager.LoadScene("EndGame");
            return;
        }
        transform.position = GameObject.Find("Level" + currLevel + "Spawn").transform.position;
        strength = 5;
        power = 1;
    }

    private void FinishLevel()
    {
        currLevel++;
        Respawn();
    }

    private void CheckCurrentTile()
    {
        Vector3Int gridPosition = ground.WorldToCell(transform.position);

        TileBase standingOn = ground.GetTile(gridPosition);
        //Debug.Log(standingOn.name.Substring(standingOn.name.IndexOf('_')));
        int ID = Int32.Parse(standingOn.name.Substring(standingOn.name.IndexOf('_')+1));
        if(ID >= 15 && ID <= 25) //Standing on Rubber 
        {
            strength = 5;
        }

        if(ID >= 38 && ID <= 48) //Standing on Fiber Optic
        {
            power += 1;
        }

        if (ID == 53) //Standing on Step Down
        {
            power -= 1;
        }

        if (power == 1 && (ID == 49 || ID == 50 || ID == 36 || ID == 37)) //Finishes on Green
        {
            FinishLevel();
        }

        if(power == 2 && (ID >= 57 && ID <= 60)) //Finishes on Red
        {
            FinishLevel();
        }


    }

    private bool TryMove(Vector2 direction)
    {
        Vector3Int gridPosition = ground.WorldToCell(transform.position + (Vector3)direction);
        if (ground.HasTile(gridPosition))
        {
            return true;
        }
        return false;
        
    }
}
