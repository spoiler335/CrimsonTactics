using System.Collections;
using System.Collections.Generic;

public interface IAI
{
    public const float moveSpeed = 4f;
    public void MoveTowardsPlayer(int x, int y);
    public void SetInitialCoordinates(int x, int y);

    public IEnumerator MoveToardsTarget(List<SingleTile> path);
    
}