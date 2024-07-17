using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem instance;

    public GridLayout gridLayout;
    public Tilemap main;
    public Tilemap temp;

    static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    [SerializeField] Tile t_White;
    [SerializeField] Tile t_Green;
    [SerializeField] Tile t_Red;


    #region Unity Methods
    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, t_White); 
        tileBases.Add(TileType.Green, t_Green);
        tileBases.Add(TileType.Red, t_Red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Tilemap Management

#endregion

    public enum TileType
    { 
        Empty,
        White,
        Green,
        Red
    }
}
