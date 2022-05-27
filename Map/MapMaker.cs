namespace Yubunen.Mapmaker{
    using Components;
    using UnityEngine;
    using System;
    using System.Collections.Generic;

    public class Map {
        private List<Point> points;
        private List<Line> lines;

        private Point start;
        private Point goal;

        public Vector2 center;

        public Map() {

        }

        public void Make(
            float minDistance = 2f,
            float maxDistance = 2.5f,
            int numP = 30,
            float range = 10,
            int routes = 5
            ) {
            points = new List<Point>();
            lines = new List<Line>();

            Vector2 center = Vector2.zero;
            start = new Point(center + PolarVec2(range, Mathf.PI));
            goal = new Point(center + PolarVec2(range, 0));

            Vector2 seed = RandomVector2(center, minDistance, maxDistance);

            points.Add(new Point(seed));
            points.Add(start);
            points.Add(goal);

            PoissonPoint(points);
            
            Delauney(points);

            MargeRoute(routes);

            ExtractHasArrow();

            lines = ExtractArrow(points);


            Debug.Log($"points.Count = {points.Count}");
            Debug.Log($"lines.Count = {lines.Count}");

        }

        private List<Point> PoissonPoint(
            List<Point> rtnList,
            float minDistance = 2f,
            float maxDistance = 2.5f,
            int numP = 30,
            float range = 10
            ) {
            List<Point> subList = new List<Point>();
            subList.Add(rtnList[0]);

            while (subList.Count > 0) {
                for (int i=0; i<numP; i++) {
                    Vector2 vec = RandomVector2(subList[subList.Count - 1].vector2, minDistance, maxDistance);
                    if (CanSetVector2(rtnList, vec, minDistance, range)) {
                        Point tmp = new Point(vec);
                        rtnList.Add(tmp);
                        subList.Add(tmp);
                    }
                }
                subList.RemoveAt(subList.Count - 1);
            }

            return rtnList;
        }

        private List<Line> Delauney(List<Point> points) {
            List<Triangle> triangles = new List<Triangle>();
            Triangle tmpTri;

            int len = points.Count;
            int i, j, k;
            int ii;

            for(i=0; i<len-2; i++) {
                for(j=i+1; j<len-1; j++) {
                    for(k=j+1; k<len; k++) {
                        tmpTri = new Triangle(points[i].vector2, points[j].vector2, points[k].vector2);
                        for(ii=0; ii<len; ii++) {
                            if (tmpTri.IsInCircle(points[ii].vector2)) {
                                break;
                            }
                        }
                        if(ii==len && !triangles.Contains(tmpTri)) {
                            triangles.Add(tmpTri);
                        }
                    }
                }
            }

            List<Line> lines = new List<Line>();
            Line[] tmpLines = new Line[3];

            foreach(Triangle triangle in triangles) {
                tmpLines[0] = new Line(triangle.a, triangle.b);
                tmpLines[1] = new Line(triangle.b, triangle.c);
                tmpLines[2] = new Line(triangle.c, triangle.a);
                foreach(Line line in tmpLines) {
                    bool canAdd = true;
                    foreach(Line alrLine in lines) {
                        if (line.Equals(alrLine)) {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd) {
                        lines.Add(line);
                        Point point1 = new Point();
                        Point point2 = new Point();
                        foreach(Point point in points) {
                            if (point.IsThis(line.a)) {
                                point1 = point;
                            }else if (point.IsThis(line.b)) {
                                point2 = point;
                            }
                        }
                        if(point1 != null && point2 != null) {
                            Point.Connect(point1, point2);
                        }
                    }
                }
            }

            return lines;
        }

        private List<Line> ExtractLines(List<Point> points) {
            List<Line> lines = new List<Line>();

            foreach(Point point in points) {
                foreach(Point conPoint in point.connentPoints) {
                    Line tmpLine = new Line(point.vector2, conPoint.vector2);
                    bool canAdd = true;
                    foreach(Line alrLine in lines) {
                        if (tmpLine.Equals(alrLine)) {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd) {
                        lines.Add(tmpLine);
                    }
                }
            }

            return lines;
        }

        private List<Line> ExtractArrow(List<Point> points) {
            List<Line> lines = new List<Line>();

            foreach (Point point in points) {
                foreach (Point arrPoint in point.arrowPoints) {
                    Line tmpLine = new Line(point.vector2, arrPoint.vector2);
                    bool canAdd = true;
                    foreach (Line alrLine in lines) {
                        if (tmpLine.Equals(alrLine)) {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd) {
                        lines.Add(tmpLine);
                    }
                }
            }

            return lines;
        }

        private List<Point> MargeRoute(int routes = 5) {
            List<Point> newPoints = new List<Point>();
            List<Point> ignoreList = new List<Point>();
            for (int i = 0; i < routes; i++) {
                List<Point> routePoints = FindRoutes(points, start, goal, ignoreList);
                ignoreList.Add(routePoints[UnityEngine.Random.Range(1, routePoints.Count - 1)]);
            }

            Debug.Log("ignoreList.Count = " + ignoreList.Count);

            return newPoints;
        }

        private List<Point> FindRoutes(List<Point> points, Point start, Point goal, List<Point> ignoreList) {
            List<Point> openPoints = new List<Point>();
            List<Point> routePoints = new List<Point>();

            Point current = start;

            Point.ResetState(points);

            start.Opened(goal);
            openPoints.Add(start);

            while (current != goal) {
                openPoints.Sort((a, b) => b.CompareTo(a));
                current = openPoints[openPoints.Count - 1];
                openPoints.Remove(current);
                List<Point> addList = current.OpenAround(goal);
                foreach (Point point in addList) {
                    if (!openPoints.Contains(point) && !ignoreList.Contains(point)) {
                        openPoints.Add(point);
                    }
                }
            }

            current = goal;
            routePoints.Add(current);
            while (current.parent != null) {
                current.parent.arrowPoints.Add(current);
                current = current.parent;
                routePoints.Add(current);
            }

            return routePoints;
        }

        private void ExtractHasArrow() {
            for(int i = 0; i < points.Count; i++) {
                if(points[i].arrowPoints.Count == 0 && points[i] != goal) {
                    points.RemoveAt(i);
                    i--;
                }
            }
        }

        private Vector2 RandomVector2(Vector2 center, float minDistance, float maxDistance) {
            float rad = 2 * Mathf.PI * UnityEngine.Random.value;
            float dis = minDistance + ((maxDistance - minDistance) * UnityEngine.Random.value);

            Vector2 rel = PolarVec2(dis, rad);
            return center + rel;
        }

        private Vector2 PolarVec2(float distance, float radian) {
            return new Vector2(distance * Mathf.Sin(radian), distance * Mathf.Cos(radian));
        }

        private bool CanSetVector2(List<Point> points, Vector2 vector, float minDistance, float range) {
            if (Vector2.Distance(vector, center) > range)
                return false;
            foreach(Point p in points) {
                if (Vector2.Distance(p.vector2, vector) < minDistance)
                    return false;
            }
            
            return true;
        }

        private void AllClear() {
            
        }


        public GameObject DrawMap(GameObject parentObj, GameObject pointObj, GameObject lineObj) {
            DrawPoint(parentObj, pointObj);
            DrawLine(parentObj, lineObj);

            return parentObj;
        }

        public GameObject DrawMap(GameObject parentObj) {
            GameObject pointObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject lineObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            pointObj.GetComponent<Renderer>().material.color = Color.cyan;
            lineObj.GetComponent<Renderer>().material.color = Color.magenta;

            DrawMap(parentObj, pointObj, lineObj);

            GameObject.Destroy(pointObj);
            GameObject.Destroy(lineObj);

            return parentObj;
        }

        private void DrawPoint(GameObject parentObj, GameObject pointObj) {
            GameObject.Destroy(GameObject.Find("PointObjects"));
            GameObject pointsObj = new GameObject("PointObjects");
            pointsObj.transform.parent = parentObj.transform;
            foreach (Point point in points) {
                Vector3 vec3 = point.vector2 + center;
                point.Draw(pointObj).transform.parent = pointsObj.transform;
            }
        }

        private void DrawLine(GameObject parentObj, GameObject lineObj) {
            GameObject.Destroy(GameObject.Find("LineObjects"));
            GameObject linesObj = new GameObject("LineObjects");
            linesObj.transform.parent = parentObj.transform;
            foreach (Line line in lines) {
                line.Draw(lineObj).transform.parent = linesObj.transform;
            }
        }

        public void Test() {    //This is Coding Test
            Line line = new Line(new Vector2(1, 2), new Vector2(3, 4));

            Vector2 vec = new Vector2(5, 6);
            float f = vec.x;
        }
    }

}
