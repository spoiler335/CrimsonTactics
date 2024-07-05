using UnityEngine.Events;

public static class EventsModel
{
    public static UnityAction<int, int> SET_OBSTACLE; // This is so set the obstable sphere on the tile (x,y)

    public static UnityAction GRID_GENERATED; // This is called when the grid is generated

    public static UnityAction<int, int> TILE_CLICKED; // This is called when the user clicks on the tile(x,y)

    public static UnityAction<int, int> PLAYER_MOVEMNT_STARTED; // This is called when the player moves
    public static UnityAction PLAYER_MOVEMNT_COMPLETED; // This is called when the player stops at the specified tile

    public static UnityAction SAME_TILE_CLICKED; // This is called when the user clicks on the same tile as the player

    public static UnityAction PLAYER_HAS_NO_PATH; // This is called when the player cannot reach the desier tile
}