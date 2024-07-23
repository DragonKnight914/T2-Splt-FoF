using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem instance;

    public GridLayout gridLayout;
    public Tilemap mainTile;
    public Tilemap tempTile;

    static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    [SerializeField] Tile t_White;
    [SerializeField] Tile t_Green;
    [SerializeField] Tile t_Red;

    Building temporary;
    Vector3 prevPos;
    private BoundsInt prevArea;


    #region Unity Methods
    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        tileBases.Add(TileType.White, t_White); 
        tileBases.Add(TileType.Green, t_Green);
        tileBases.Add(TileType.Red, t_Red);
    }

    // Update is called once per frame
    void Update()
    {
        if(!temporary)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }

            if(!temporary.Placed)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

                if (prevPos != cellPos)
                {
                    temporary.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(0f, 0f, 0f));
                    prevPos = cellPos;
                    FollowBuilding();
                }
            }
        }
    }

    #endregion
      
    #region Tilemap Management

    static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);

            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];

        FillTiles(tileArray, type);

        tilemap.SetTilesBlock(area, tileArray);
    }

    static void FillTiles(TileBase[] arr, TileType type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }

    #endregion

    #region Building Placement
    public void InitializeWithBuilding(GameObject building)
    {
        temporary = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        FollowBuilding();

    }

    void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.z];
        FillTiles(toClear, TileType.Empty);
        tempTile.SetTilesBlock(prevArea, toClear);
    }

    void FollowBuilding()
    {
        ClearArea();

        temporary.area.position = gridLayout.WorldToCell(temporary.gameObject.transform.position);
        BoundsInt buildingArea = temporary.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTile);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i = 0; i < baseArray.Length; i++)
        {

                if (baseArray[i] == tileBases[TileType.White]) 
                {
                    tileArray[i] = tileBases[TileType.Green];
                }

                else {
                    FillTiles(tileArray, TileType.Red);
                    break;
                }
        }

        tempTile.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTile);
        foreach(var b in baseArray)
        {
            if(b != tileBases[TileType.White])
            {
                Debug.Log("Cannot take this area");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, tempTile);
        SetTilesBlock(area, TileType.Green, mainTile);
    }

    public void Place()
    {
        if (temporary.CanBePlaced())
        {
            temporary.Place();
        }
    }

    #endregion

    #region Build Mode

    public void ActiveTilemapRenderer(bool hasToEnable)
    {
        TilemapRenderer renderer = mainTile.GetComponent<TilemapRenderer>();

        if(hasToEnable == false)
        {
            ClearArea();
            Destroy(temporary.gameObject);
        }

        renderer.enabled = hasToEnable;
    }

    #endregion

    public enum TileType
    { 
        Empty,
        White,
        Green,
        Red
    }

}
