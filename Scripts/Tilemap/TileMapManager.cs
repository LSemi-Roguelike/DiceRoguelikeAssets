using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public static TileMapManager manager;

    [SerializeField]
    Tilemap groudTileMap, unitTileMap;

    GameObject[][] groundTileArray;
    GameObject[][] unitTileArray;

    int x_anchor, y_anchor, x_size, y_size;

    private void Awake()
    {
        manager = this;
        SetTilemap(groudTileMap, unitTileMap);
    }

    public void SetTilemap(Tilemap gTile, Tilemap uTile)
    {
        groudTileMap = gTile;
        unitTileMap = uTile;
        int tileCount = groudTileMap.transform.childCount;
        int xMax = 0, xMin = 0, yMax = 0, yMin = 0;
        for (int i = 0; i < tileCount; i++)
        {
            Vector3Int tra = groudTileMap.WorldToCell(groudTileMap.transform.GetChild(i).position);

            xMax = xMax > tra.x ? xMax : tra.x;
            yMax = yMax > tra.y ? yMax : tra.y;

            xMin = xMin < tra.x ? xMin : tra.x;
            yMin = yMin < tra.y ? yMin : tra.y;
        }

        x_anchor = -xMin;
        y_anchor = -yMin;

        x_size = xMax - xMin + 1;
        y_size = yMax - yMin + 1;

        groundTileArray = new GameObject[x_size][];
        unitTileArray = new GameObject[x_size][];

        for (int i = 0; i < x_size; i++)
        {
            groundTileArray[i] = new GameObject[y_size];
            unitTileArray[i] = new GameObject[y_size];
        }

        for (int i = 0; i < tileCount; i++)
        {
            Transform child = groudTileMap.transform.GetChild(i);
            Vector3Int pos = WorldToCell(child.position);
            groundTileArray[pos.x + x_anchor][pos.y + y_anchor] = child.gameObject;
        }

        for (int i = 0; i < unitTileMap.transform.childCount; i++)
        {
            Transform child = unitTileMap.transform.GetChild(i);
            Vector3Int pos = WorldToCell(child.position);
            unitTileArray[pos.x + x_anchor][pos.y + y_anchor] = child.gameObject;
        }
    }

    public Vector3Int WorldToCell(Vector3 pos)
    {
        return unitTileMap.WorldToCell(pos);
    }
    public Vector3 CellToWorld(Vector3Int pos)
    {
        return unitTileMap.CellToWorld(pos) + new Vector3(0.5f, 0f, 0.5f);
    }

    public bool IsPosAvail(Vector3Int pos)
    {
        return (pos.x + x_anchor < x_size && pos.y + y_anchor < y_size &&
            pos.x + x_anchor >= 0 && pos.y + y_anchor >= 0);
    }

    public GameObject GetGroundTile(Vector3Int pos)
    {
        if (!IsPosAvail(pos))return null;

        return groundTileArray[pos.x + x_anchor][pos.y + y_anchor];
    }

    public GameObject GetUnitTile(Vector3Int pos)
    {
        if (!IsPosAvail(pos))return null;

        return unitTileArray[pos.x + x_anchor][pos.y + y_anchor];
    }


    public bool MoveUnit(Vector3Int origin, Vector3Int target)
    {
        if (!IsPosAvail(origin) || !IsPosAvail(target)
            ||unitTileArray[target.x + x_anchor][target.y + y_anchor] != null)
            return false;

        unitTileArray[target.x + x_anchor][target.y + y_anchor] = unitTileArray[origin.x + x_anchor][origin.y + y_anchor];
        unitTileArray[origin.x + x_anchor][origin.y + y_anchor] = null;
        return true;
    }

    public void RemoveUnit(Vector3Int pos)
    {
        if (!IsPosAvail(pos))
            return;
        unitTileArray[pos.x + x_anchor][pos.y + y_anchor] = null;
    }

    public bool CheckBlocked(Vector3Int pos)
    {
        if (!IsPosAvail(pos))return false;

        return unitTileArray[pos.x + x_anchor][pos.y + y_anchor] == null && groundTileArray[pos.x + x_anchor][pos.y + y_anchor] != null;
    }

    public List<Route> GetMoveableTiles(Vector3Int pos, int maxDist, bool cross = false)
    {
        pos.z = 0;
        List<Route> tiles = new List<Route>();

        Route root = new Route(pos, null, 0);
        List<Vector3Int> moveArr = new List<Vector3Int>{new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0)};
        if (cross)
        {
            moveArr.Add(new Vector3Int(1, 1, 0));
            moveArr.Add(new Vector3Int(1, -1, 0));
            moveArr.Add(new Vector3Int(-1, 1, 0));
            moveArr.Add(new Vector3Int(-1, -1, 0));
        }

        foreach (var move in moveArr)
        {
            if (CheckBlocked((Vector3Int)(root.pos + move)))
                tiles.Add(new Route(root.pos + move, root, root.dist + 1));
        }

        int counter = 0;
        while(tiles.Count > counter)
        {
            Route cur = tiles[counter];
            counter++;
            if (cur.dist >= maxDist)
                break;
                
            foreach(var move in moveArr)
            {
                if(cur.preRoute.pos == cur.pos + move)
                    continue;
                
                var temp = tiles.Find(a => {
                    return a.pos == cur.pos + move;
                });

                if(temp == null && CheckBlocked(cur.pos + move))
                {
                    tiles.Add(new Route(cur.pos + move, cur, cur.dist + 1));
                }
            }
        }
        return tiles;
    }
    /*
    public List<Route> GetAttackableTiles(Vector3Int pos, Range range)
    {
        pos.z = 0;
        List<Route> tiles = new List<Route>();
        Route root = new Route(pos, null, 0);

        List<Vector3Int> dirList = new List<Vector3Int>();

        if (range.rangeType != Range.RangeType.AXIS_DIAGONAL)
        {
            dirList.AddRange(new List<Vector3Int> { 
                new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0), 
                new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0) 
            });
        }
        
        if ((range.rangeType == Range.RangeType.DISTANCE && range.maxRange >= 2)
            || (range.rangeType != Range.RangeType.DISTANCE 
            && range.rangeType != Range.RangeType.AXIS_CROSS))
        {
            dirList.AddRange(new List<Vector3Int> { 
                new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0), 
                new Vector3Int(-1, 1, 0), new Vector3Int(-1, -1, 0) 
            });
        }

        List<Vector3Int> nonSight = new List<Vector3Int>();

        foreach (var dir in dirList)
        {
            Vector3Int next = pos + dir;
            if (dir.x == 0 || dir.y == 0)
            {
                if ((range.ignoreBlocked && IsPosAvail(next)) || CheckBlocked(next))
                    tiles.Add(new Route(next, root, 1));
                else if (IsPosAvail(next + dir))
                {
                    if (dir.x == 0)
                    {
                        nonSight.Add(next + dir + new Vector3Int(1, 0, 0));
                        nonSight.Add(next + dir + new Vector3Int(-1, 0, 0));
                    }
                    else
                    {
                        nonSight.Add(next + dir + new Vector3Int(0, 1, 0));
                        nonSight.Add(next + dir + new Vector3Int(0, -1, 0));
                    }
                }
            }
            if ((range.ignoreBlocked && IsPosAvail(next)) || CheckBlocked(next))
            {
                if(range.rangeType == Range.RangeType.DISTANCE)
                    tiles.Add(new Route(next, root, 
                        Mathf.Abs(dir.x) + Mathf.Abs(dir.y)));
                else 
                    tiles.Add(new Route(next, root, 1));
            }
        }


        int counter = 0;
        while (tiles.Count > counter)
        {
            Route cur = tiles[counter];
            Vector3Int dir = cur.pos - cur.preRoute.pos;
            Vector3Int next = cur.pos + dir;
            counter++;

            if (cur.dist == range.maxRange)
                continue;

            if (dir.x == 0 || dir.y == 0)
            {
                if((range.ignoreBlocked && IsPosAvail(next)) || CheckBlocked(next))
                    tiles.Add(new Route(next, cur, cur.dist + 1));
                else if (IsPosAvail(next + dir))
                {
                    if (dir.x == 0)
                    {
                        nonSight.Add(next + dir + new Vector3Int(1, 0, 0));
                        nonSight.Add(next + dir + new Vector3Int(-1, 0, 0));
                    }
                    else
                    {
                        nonSight.Add(next + dir + new Vector3Int(0, 1, 0));
                        nonSight.Add(next + dir + new Vector3Int(0, -1, 0));
                    }
                }
            }
            else
            {
                if (range.rangeType == Range.RangeType.DISTANCE || range.rangeType == Range.RangeType.SQUARE)
                {
                    Vector3Int temp = cur.pos + new Vector3Int(1, 0, 0) * dir;
                    if (((range.ignoreBlocked && IsPosAvail(temp)) || CheckBlocked(temp))
                        && !nonSight.Exists(x => { return x == temp; }))
                        tiles.Add(new Route(temp, cur, cur.dist + 1));
                    else if(IsPosAvail(next + dir))
                    {
                        nonSight.Add(next + dir + new Vector3Int(0, 1, 0));
                        nonSight.Add(next + dir + new Vector3Int(0, -1, 0));
                    }


                    temp = cur.pos + new Vector3Int(0, 1, 0) * dir;
                    if (((range.ignoreBlocked && IsPosAvail(temp)) || CheckBlocked(temp))
                        && !nonSight.Exists(x => { return x == temp; }))
                        tiles.Add(new Route(temp, cur, cur.dist + 1));
                    else if(IsPosAvail(next + dir))
                    {
                        nonSight.Add(next + dir + new Vector3Int(1, 0, 0));
                        nonSight.Add(next + dir + new Vector3Int(-1, 0, 0));
                    }
                }
                if ((range.ignoreBlocked && IsPosAvail(next)) || CheckBlocked(next))
                {
                    if (range.rangeType != Range.RangeType.DISTANCE)
                        tiles.Add(new Route(next, cur, cur.dist + 1));
                    else if (cur.dist + 2 <= range.maxRange)
                        tiles.Add(new Route(next, cur, cur.dist + 2));
                }
            }

            if (cur.dist < range.minRange)
            {
                counter--;
                tiles.RemoveAt(counter);
            }
        }

        foreach (var v in nonSight)
        {
            Debug.Log(v);
        }

        return tiles;
    }
    */
    public List<Route> GetAttackableTiles(Vector3Int pos, Range range)
    {
        pos.z = 0;
        List<Route> tiles = new List<Route>();
        Route root = new Route(pos, null, 0);

        List<Vector3Int> nonSight = new List<Vector3Int>();
        int tempMin = range.minRange > 0 ? range.minRange : 1;

        if (range.rangeType != Range.RangeType.AXIS_DIAGONAL)
        {
            List<Vector3Int> dirList = new List<Vector3Int> {
                new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0),
                new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0)
            };

            foreach (var dir in dirList)
            {
                Route preRoute = root;
                for (int i = 1; i <= range.maxRange; i++)
                {
                    Vector3Int next = pos + dir * i;
                    if ((range.ignoreBlocked && IsPosAvail(next)) || CheckBlocked(next))
                    {
                        preRoute = new Route(next, preRoute, i);
                        tiles.Add(preRoute);
                    }
                    else if (IsPosAvail(next + dir))
                    {
                        if (dir.x == 0)
                        {
                            nonSight.Add(next + dir + new Vector3Int(1, 0, 0));
                            nonSight.Add(next + dir + new Vector3Int(-1, 0, 0));
                        }
                        else
                        {
                            nonSight.Add(next + dir + new Vector3Int(0, 1, 0));
                            nonSight.Add(next + dir + new Vector3Int(0, -1, 0));
                        }
                        break;
                    }
                }
            }
        }
        int counter = tiles.Count;

        if ((range.rangeType == Range.RangeType.DISTANCE && range.maxRange >= 2)
            || (range.rangeType != Range.RangeType.DISTANCE && range.rangeType != Range.RangeType.AXIS_CROSS))
        {
            List<Vector3Int> dirList = new List<Vector3Int> {
                new Vector3Int(1, 1, 0), new Vector3Int(1, -1, 0),
                new Vector3Int(-1, 1, 0), new Vector3Int(-1, -1, 0)
            };

            foreach (var dir in dirList)
            {
                Route preRoute = root;
                for (int i = 1; i <= range.maxRange; i++)
                {
                    Vector3Int next = pos + dir * i;
                    if ((range.ignoreBlocked && IsPosAvail(next)) || CheckBlocked(next))
                    {
                        if (range.rangeType == Range.RangeType.DISTANCE)
                        {
                            if (i * 2 > range.maxRange)
                                break;
                            preRoute = new Route(next, preRoute, i * 2);
                        }
                        else
                            preRoute = new Route(next, preRoute, i);
                        tiles.Add(preRoute);
                    }
                    else
                        break;
                }
            }
        }

        if (range.rangeType == Range.RangeType.DISTANCE || range.rangeType == Range.RangeType.SQUARE)
        {
            while (tiles.Count > counter)
            {
                Route cur = tiles[counter];
                Vector3Int dir = cur.pos - cur.preRoute.pos;
                Vector3Int next = cur.pos + dir;
                counter++;

                if (cur.dist == range.maxRange)
                    continue;
                if (dir.x == 0 || dir.y == 0)
                {
                    if ((range.ignoreBlocked && IsPosAvail(next)) || CheckBlocked(next)
                        && !nonSight.Exists(x => { return x == next; }))
                    {
                        tiles.Insert(counter, new Route(next, cur, cur.dist + 1));
                    }
                    else if (IsPosAvail(next + dir))
                    {
                        if (dir.x == 0)
                        {
                            if (next.x - pos.x > 0)
                                nonSight.Add(next + dir + new Vector3Int(1, 0, 0));
                            else
                                nonSight.Add(next + dir + new Vector3Int(-1, 0, 0));
                        }
                        else
                        {
                            if (next.y - pos.y > 0)
                                nonSight.Add(next + dir + new Vector3Int(0, 1, 0));
                            else
                                nonSight.Add(next + dir + new Vector3Int(0, -1, 0));
                        }
                    }
                }
                else
                {
                    Vector3Int temp = cur.pos + new Vector3Int(1, 0, 0) * dir;
                    if (((range.ignoreBlocked && IsPosAvail(temp)) || CheckBlocked(temp))
                        && !nonSight.Exists(x => { return x == temp; }))
                    {
                        tiles.Insert(counter, new Route(temp, cur, cur.dist + 1));
                    }
                    else if (IsPosAvail(temp))
                    {
                        if (temp.y - pos.y > 0)
                            nonSight.Add(temp + new Vector3Int(1, 0, 0) * dir + new Vector3Int(0, 1, 0));
                        else
                            nonSight.Add(temp + new Vector3Int(1, 0, 0) * dir + new Vector3Int(0, -1, 0));
                    }


                    temp = cur.pos + new Vector3Int(0, 1, 0) * dir;
                    if (((range.ignoreBlocked && IsPosAvail(temp)) || CheckBlocked(temp))
                        && !nonSight.Exists(x => { return x == temp; }))
                    {
                        tiles.Insert(counter, new Route(temp, cur, cur.dist + 1));
                    }
                    else if (IsPosAvail(temp))
                    {
                        if (temp.x - pos.x > 0)
                            nonSight.Add(temp + new Vector3Int(0, 1, 0) * dir + new Vector3Int(1, 0, 0));
                        else
                            nonSight.Add(temp + new Vector3Int(0, 1, 0) * dir + new Vector3Int(-1, 0, 0));
                    }
                }
            }
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].dist < range.minRange)
            {
                tiles.RemoveAt(i);
                i--;
            }
        }

        if (range.minRange == 0)
            tiles.Insert(0, root);

        return tiles;
    }
}

public class Route
{
    public Vector3Int pos; 
    public Route preRoute;
    public int dist;


    public Route(Vector3Int pos, Route preRoute, int dist)
    {
        this.pos = pos;
        this.preRoute = preRoute;
        this.dist = dist;
    }
}