using UnityEngine.Events;

public static class EventsModel
{
    public static UnityAction<int, int> SET_OBSTACLE;

    public static UnityAction GRID_GENERATED;

    public static UnityAction<int, int> TILE_CLICKED;

    public static UnityAction PLAYER_MOVEMNT_STARTED;
    public static UnityAction PLAYER_MOVEMNT_COMPLETED;
}