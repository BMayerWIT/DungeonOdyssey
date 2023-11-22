using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using static UnityEngine.UI.Image;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject[] startRoomPrefabs;
    public GameObject[] tilePrefabs;
    public GameObject[] blockedPrefabs;
    public GameObject[] doorPrefabs;
    public GameObject[] exitPrefabs;

    [Header("Debugging Options")]
    public bool useBoxColliders;
    public bool useLightsForDebugging;
    public bool restoreLightsAfterDebugging;
    public bool drawGizmos;

    [Header("Keybinding options")]
    public KeyCode toggleMapKey = KeyCode.M;

    [Header("Generation Limits")]
    [Range(2, 100)] public int mainLength = 10;
    [Range(0, 50)] public int branchLength = 5;
    [Range(0, 25)] public int numberOfBranches = 10;
    [Range(0, 100)] public int doorSpawnPercent = 25;
    [Range(0,1)] public float constructionDelay;

    [Header("Available At Runtime")]
    public List<Tile> generatedTiles = new List<Tile>();


    private GameObject goCamera, goPlayer;
    private List<Connector> availableConnectors = new List<Connector>();
    private Color startLightColor = Color.white;
    private Transform tileFrom, tileTo, tileRoot;
    private Transform container;
    private int attempts;
    private int maxAttempts = 50;
    

    private void Start()
    {
        goCamera = GameObject.Find("OverHeadCamera");
        goPlayer = GameObject.FindGameObjectWithTag("Player"); 
        StartCoroutine(DungeonBuilder());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("ProcGen");
        }
        if (Input.GetKeyDown(toggleMapKey))
        {
            goCamera.SetActive(!goCamera.activeInHierarchy);
            goPlayer.SetActive(!goPlayer.activeInHierarchy);
        }
    }

    private IEnumerator DungeonBuilder()
    {
        goCamera.SetActive(true);
        goPlayer.SetActive(false);
        GameObject objectContainer = new GameObject("Main Path");
        container = objectContainer.transform;
        container.SetParent(transform);
        tileRoot = CreateStartTile();
        tileTo = tileRoot;
        DebugRoomLighting(tileRoot, Color.cyan);
        for (int i = 0; i < mainLength - 1; i++)
        {
            yield return new WaitForSeconds(constructionDelay);
            tileFrom = tileTo;
            tileTo = CreateTile();
            DebugRoomLighting(tileTo, Color.red);
            ConnectTiles();
            CollisionCheck();
            if (attempts >= maxAttempts)
            {
                attempts = 0;
                break;
            }
        }

        // Get all connectors within main path container that are NOT already connected
        foreach (Connector connector in container.GetComponentsInChildren<Connector>())
        {
            if (!connector.isConnected)
            {
                if (!availableConnectors.Contains(connector))
                {
                    availableConnectors.Add(connector);
                }
                
            }
        }
        // Branching
        for (int b = 0; b < numberOfBranches; b++)
        {
            if (availableConnectors.Count > 0)
            {
                objectContainer = new GameObject("Branch " + (b + 1));
                container = objectContainer.transform;
                container.SetParent(transform);
                int availIndex = Random.Range(0, availableConnectors.Count);
                tileRoot = availableConnectors[availIndex].transform.parent.parent;
                availableConnectors.RemoveAt(availIndex);
                tileTo = tileRoot;
                for (int i = 0; i < branchLength - 1; i++)
                {
                    yield return new WaitForSeconds(constructionDelay);
                    tileFrom = tileTo;
                    tileTo = CreateTile();
                    DebugRoomLighting(tileTo, Color.green);
                    ConnectTiles();
                    CollisionCheck();
                    if (attempts >= maxAttempts)
                    {
                        attempts = 0;
                        break;
                    }
                }
            }
            else { break; }
        }
        LightRestoration();
        CleanUpBoxes();
        goCamera.SetActive(false);
        goPlayer.SetActive(true);
    }

    private void CollisionCheck()
    {
        BoxCollider box = tileTo.GetComponent<BoxCollider>();
        Vector3 offset = (tileTo.right * box.center.x) + (tileTo.up * box.center.y) + (tileTo.forward * box.center.z);  
        Vector3 halfExtents = box.bounds.extents;

        List<Collider> hits = Physics.OverlapBox(tileTo.position + offset, halfExtents, Quaternion.identity, LayerMask.GetMask("Tile")).ToList();
        if (hits.Count > 0)
        {
            if (hits.Exists(x => x.transform != tileFrom && x.transform != tileTo) && attempts < maxAttempts)
            {
                // Hit something other than tileTo or tileFrom
                attempts++;
                Debug.Log(attempts);
                int toIndex = generatedTiles.FindIndex(x => x.tile == tileTo);
                if (generatedTiles[toIndex].connector != null)
                {
                    generatedTiles[toIndex].connector.isConnected = false;
                }
                generatedTiles.RemoveAt(toIndex);
                DestroyImmediate(tileTo.gameObject);
                
                //// Backtracking
                //if (attempts == maxAttempts)
                //{
                //    int fromIndex = generatedTiles.FindIndex(x => x.tile == tileFrom);
                //    Tile myTileFrom = generatedTiles[fromIndex];
                //    if (tileFrom != tileRoot) // If Tilefrom is not the root of a branch
                //    {
                //        if (myTileFrom.connector != null) // if newly generated tile from tilefrom contains a connector
                //        {
                //            myTileFrom.connector.isConnected = false; // set connector isconnected to false (is able to be connected to a different tile)
                //        }
                //        availableConnectors.RemoveAll(x => x.transform.parent.parent == tileFrom); // remove all connectors in the tile branching from, from the connectors list
                //        generatedTiles.RemoveAt(fromIndex); // remove tile at the index of the tile from
                //        DestroyImmediate(tileFrom.gameObject); // immediately destroy it
                //        // End 1st if (tileFrom is null)

                //        if (myTileFrom.origin != tileRoot) // if newly generated tile origin (its tile from) isnt the root of the branch
                //        {
                //            tileFrom = myTileFrom.origin; // tile from is set to that piece. ie. if its not the root its set back 
                //        }
                //        else if (container.name.Contains("Main"))
                //        {
                //            if (myTileFrom.origin != null)
                //            {
                //                tileRoot = myTileFrom.origin;
                //                tileFrom = tileRoot;
                //            }
                //        }
                //        else if (availableConnectors.Count > 0)
                //        {
                //            int availableIndex = Random.Range(0, availableConnectors.Count);
                //            tileRoot = availableConnectors[availableIndex].transform.parent.parent;
                //            availableConnectors.RemoveAt(availableIndex);
                //            tileFrom = tileRoot;
                //        }
                //        else { return; }

                //    }
                //    else if (container.name.Contains("Main"))
                //    {
                //        if (myTileFrom.origin != null)
                //        {
                //            tileRoot = myTileFrom.origin;
                //            tileFrom = tileRoot;
                //        }
                //    }
                //    else if (availableConnectors.Count > 0)
                //    {
                //        int availableIndex = Random.Range(0, availableConnectors.Count);
                //        tileRoot = availableConnectors[availableIndex].transform.parent.parent;
                //        availableConnectors.RemoveAt(availableIndex);
                //        tileFrom = tileRoot;
                //    }
                //    else { return; }
               // }
                // Retry
                if (tileFrom != null)
                {
                    tileTo = CreateTile();
                    Color retryColor = container.name.Contains("Branch") ? Color.green : Color.yellow;
                    DebugRoomLighting(tileTo, retryColor * 2);
                    ConnectTiles();
                    CollisionCheck();
                }

            }
            else { attempts = 0; } // Nothing other than tileTo and tileFrom was hit (Restore attempts back to 0);

        }
        
    }

    private void LightRestoration()
    {
        if (useLightsForDebugging && restoreLightsAfterDebugging && Application.isEditor)
        {
            Light[] lights = transform.GetComponentsInChildren<Light>();
            foreach (Light light in lights)
            {
                light.color = startLightColor;
            }
        }
    }

    private void CleanUpBoxes()
    {
        if (!useBoxColliders)
        {
            
            foreach (Tile tile in generatedTiles)
            {
               
                BoxCollider box = tile.tile.GetComponent<BoxCollider>();
                if (box != null)
                {
                    Destroy(box);
                }
            }
        }
    }

    private void DebugRoomLighting(Transform tile, Color lightColor)
    {
        if (useLightsForDebugging && Application.isEditor)
        {
            Light[] lights = tile.GetComponentsInChildren<Light>();
            if (lights.Length > 0) 
            {
                if(startLightColor == Color.white)
                {
                    startLightColor = lights[0].color;
                }
                foreach (Light light in lights)
                {
                    light.color = lightColor;
                }
            }
            
        }
    }

    private void ConnectTiles()
    {
        Transform connectFrom = GetRandomConnector(tileFrom);
        if (connectFrom == null )
        {
            return;
        }
        Transform connectTo = GetRandomConnector(tileTo);
        if (connectTo == null)
        {
            return;
        }

        connectTo.SetParent(connectFrom);
        tileTo.SetParent(connectTo);
        connectTo.localPosition = Vector3.zero;
        connectTo.localRotation = Quaternion.identity;
        connectTo.Rotate(0, 180f, 0);
        tileTo.SetParent(container);
        connectTo.SetParent(tileTo.Find("Connectors"));
        generatedTiles.Last().connector = connectFrom.GetComponent<Connector>();
    }

    private Transform GetRandomConnector(Transform tile)
    {
        if (tile == null)
        {
            return null;
        }

        List<Connector> connectorList = tile.GetComponentsInChildren<Connector>().ToList().FindAll(x => x.isConnected == false); //
        if (connectorList.Count > 0 )
        {
            int connectorIndex = Random.Range(0, connectorList.Count);
            connectorList[connectorIndex].isConnected = true;
            if (tile == tileFrom)
            {
                BoxCollider box = tile.GetComponent<BoxCollider>();
                if (box != null)
                {
                    box.isTrigger = true;
                }
            }
            return connectorList[connectorIndex].transform;

        }
        return null;
    }

    private Transform CreateTile()
    {
        int index = Random.Range(0, tilePrefabs.Length);
        GameObject objectTile = Instantiate(tilePrefabs[index], Vector3.zero, Quaternion.identity, container) as GameObject;
        objectTile.name = tilePrefabs[index].name;
        int tileFromIndex = generatedTiles.FindIndex(x => x.tile == tileFrom);

        // Check if the index is valid
        if (tileFromIndex != -1)
        {
            Transform origin = generatedTiles[tileFromIndex].tile;
            generatedTiles.Add(new Tile(objectTile.transform, origin));
            return objectTile.transform;
        }
        else
        {
            // Handle the case when the tile corresponding to tileFrom is not found
            Debug.LogError("Tile corresponding to tileFrom not found!");
            // You may choose to return null or handle the situation differently based on your needs
            generatedTiles.Add(new Tile(objectTile.transform, null));
            return objectTile.transform;
        }
    }

    private Transform CreateStartTile()
    {
        int index = Random.Range(0, startRoomPrefabs.Length);
        GameObject objectTile = Instantiate(startRoomPrefabs[index], Vector3.zero, Quaternion.identity, container) as GameObject;
        objectTile.name = "Start Room";
        float yRotation = Random.Range(0, 4) * 90f;
        objectTile.transform.Rotate(0, yRotation, 0);
        goPlayer.transform.LookAt(objectTile.GetComponentInChildren<Connector>().transform.position);
        // Add to generated tiles list
        generatedTiles.Add(new Tile(objectTile.transform, null));
        return objectTile.transform;
    }
}
