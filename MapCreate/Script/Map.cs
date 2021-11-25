using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

public class Map {
    private List<GameObject> obj_list = new List<GameObject>();
    private GameObject mapObject = null;

    private List<Point> con_list = new List<Point>();

    private List<Triangle> tri_list = new List<Triangle>();

    private List<Line> line_list = new List<Line>();

    private Vector2 center;

    private Point start;
    private Point goal;

    //Huge triangle
    private Triangle HugeTriangle;

    //option
    private float r; //minimum radius;
    private float rRate = 0.5f;
    private int num_p = 30; //make point of numver in ring
    private float range = 10;

    private GameObject point_obj;

    public Map() : this(2f) {

    }
    public Map(float r) {
        this.r = r;
        
        float rad = 0;
        Vector2 p0 = new Vector2(2 * range * Mathf.Sin(rad), 2 * range * Mathf.Cos(rad));
        rad = Mathf.PI * 2 / 3;
        Vector2 p1 = new Vector2(2 * range * Mathf.Sin(rad), 2 * range * Mathf.Cos(rad));
        rad = Mathf.PI * 4 / 3;
        Vector2 p2 = new Vector2(2 * range * Mathf.Sin(rad), 2 * range * Mathf.Cos(rad));
        this.HugeTriangle = new Triangle(p0, p1, p2);
    }

    public void SetParent(GameObject obj) {
        mapObject = obj;
    }


    public void Make() {
        AllClear();

        center = new Vector2(0,0);
        start = new Point(center + PolarPoint(range, Mathf.PI));
        goal = new Point(center + PolarPoint(range, 0));

        Vector2 fpoint = Random_point(center);

        con_list.Add(new Point(fpoint));
        con_list.Add(start);
        con_list.Add(goal);

        Make_point();
        
        Delaunay();

        ExtractLine();
        
    }

    public void AllClear() {
        con_list.Clear();
        tri_list.Clear();
        line_list.Clear();

        foreach(Transform child in mapObject.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void Make_point() {
        List<Point> pre_list = new List<Point>();
        pre_list.Add(con_list[0]);

        while (pre_list.Count > 0) {
            for (int i = 0; i < num_p; i++) {
                Vector2 p = Random_point(pre_list[pre_list.Count - 1].Get_vector2());
                if (Check_dis(p) && Check_range(p)) {
                    con_list.Add(new Point(p));
                    pre_list.Add(new Point(p));
                }
            }
            pre_list.RemoveAt(pre_list.Count - 1);
        }
    }

    private void ExtractLine() {
        foreach (Triangle triangle in tri_list) {
            for (int i = 0; i < 3; i++) {
                Line line = new Line(triangle.GetVector2(i), triangle.GetVector2((i + 1) % 3));
                Line tesL = new Line(line.GetVector2(0), line.GetVector2(1));
                bool check = true;
                foreach(Line tmp in line_list) {
                    if (line.Equals(tmp)) {
                        check = false;
                        break;
                    }
                }
                if(check) {
                    line_list.Add(line);
                    Point point1 = new Point();
                    Point point2 = new Point();
                    foreach(Point point in con_list) {
                        if (point.IsThis(line.GetVector2(0))) {
                            point1 = point;
                        }else if (point.IsThis(line.GetVector2(1))) {
                            point2 = point;
                        }
                    }
                    if (point1 != null && point2 != null) {
                        Point.Connect(point1, point2);
                    }
                }
            }
        }
    }

    private void MakeLine() {
        List<List<Point>> route = new List<List<Point>>();
        
    }

    private List<Point> findRoute (List<Point> List) {
        List<Point> result = new List<Point>();
        

        return result;
    }

    private Vector2 Random_point(Vector2 p) {
        float rad = 2 * Mathf.PI * Random.value;
        float dis = r + (r * Random.value * rRate); //r ~ r*rRate

        Vector2 rel = PolarPoint(dis, rad);  //relative vector
        return p + rel;
    }

    private Vector2 PolarPoint(float distance, float radian) {
       return new Vector2(distance * Mathf.Sin(radian), distance * Mathf.Cos(radian));
    }

    private bool Check_dis(Vector2 p) {
        foreach(Point l in con_list) {
            if (Vector2.Distance(l.Get_vector2(), p) < r) {
                return false;
            }
        }
        return true;
    }

    private void Delaunay() {
        tri_list.Clear();

        int len = con_list.Count;
        int i, j, k;
        int ii;

        Triangle tmpTri = new Triangle();


        for(i = 0  ;i < len-2; i++) {
            for(j = i+1; j < len-1; j++) {
                for(k = j+1; k < len  ; k++) {
                    tmpTri = new Triangle(con_list[i].Get_vector2(), con_list[j].Get_vector2(), con_list[k].Get_vector2());
                    ii = 0;
                    for(ii = 0; ii < len; ii++) {
                        if (tmpTri.inCircle(con_list[ii].Get_vector2())){
                            break;
                        }
                    }
                    if(ii == len && !tri_list.Contains(tmpTri)) {
                        tri_list.Add(tmpTri);
                    }
                }
            }
        }

    }

    public void Draw(GameObject pointObj, GameObject LineObj) {
        DrawPoint(pointObj);
        DrawLine(LineObj);
    }

    public void DrawPoint(GameObject pointObj) {
        GameObject oldObj = GameObject.Find("PointObjects");
        GameObject.Destroy(oldObj);
        GameObject pointsObj = new GameObject("PointObjcts");
        pointsObj.transform.parent = mapObject.transform;
        for (int i = 0; i < length(); i++) {
            Vector3 vec3 = con_list[i].Get_vector2() + center;
            GameObject.Instantiate(pointObj, vec3, Quaternion.identity).transform.parent = pointsObj.transform;
        }
    }

    public void DrawLine(GameObject lineObj) {
        GameObject oldObj = GameObject.Find("LineObjects");
        GameObject.Destroy(oldObj);
        GameObject linesObj = new GameObject("LineObjects");
        linesObj.transform.parent = mapObject.transform;
        foreach (Line line in line_list) {
            line.draw(lineObj).transform.parent = linesObj.transform;
        }
    }

    public void DrawTriangle(GameObject lineObj) {
        GameObject oldObj = GameObject.Find("TriangleObjcts");
        GameObject.Destroy(oldObj);
        GameObject trianglesObj = new GameObject("TriangleObjcts");
        trianglesObj.transform.parent = mapObject.transform;
        for (int i = 0; i < tri_list.Count; i++) {
            tri_list[i].draw(lineObj).transform.parent = trianglesObj.transform;
        }
    }

    private bool Check_range(Vector2 p) {
        return Vector2.Distance(p, center) < range;
    }

    public Vector2 Get_list(int i) {
        return con_list[i].Get_vector2() + center;
    }

    public Vector2 GetTriList(int i, int j) {
        return tri_list[i].GetVector2(j);
    }

    public int GetTriCount() {
        return tri_list.Count;
    }

    public void InstHugeTri(GameObject point_prefab) {
        for (int i = 0; i < 3; i++) {
            Vector3 vec3 = HugeTriangle.GetVector2(i);
            obj_list.Add(GameObject.Instantiate(point_prefab, vec3, Quaternion.identity));
        }
    }

    public void DrawHugiTri(GameObject lineObj) {
        this.HugeTriangle.draw(lineObj);
    }

    public void Dest_prefab() {
        for (int i = 0; i < obj_list.Count; i++) {
            GameObject.Destroy(obj_list[i]);
        }
        obj_list.Clear();
    }

    public void Remake(GameObject point_prefab) {
        Dest_prefab();
        Make();
        DrawPoint(point_prefab);
    }

    public int length() {
        return con_list.Count;
    }

    public void Sortcon_list() {
        con_list.Sort((a, b) => a.Gety().CompareTo(b.Gety()));
    }
    public string Debug_print() {
        StringBuilder sb = new StringBuilder();
        //foreach (Point l in con_list) {
        //    Vector2 p = l.Get_vector2();
        //    sb.Append(p);
        //}
        sb.Append("\r\nNumber of Triangle: " + tri_list.Count);
        sb.Append("\r\nNumber of Point: " + con_list.Count);
        sb.Append("\r\nNumber of Line: " + line_list.Count);
        sb.Append("\r\nStart has conect: " + start.CountConnect());
        sb.Append("\r\nGoal has conect: " + goal.CountConnect());
        string str = sb.ToString();
        return str;
    }

}

public class Point {
    Vector2 point;

    private List<Point> point_connect = new List<Point>();

    public Point() {

    }

    public Point(float x, float y) {
        point.Set(x, y);
    }

    public Point(Vector2 point) {
        this.point = point;
    }

    public Vector2 Get_vector2() {
        return point;
    }


    public static void Connect(Point p1, Point p2) {
        if (p2.point_connect.Contains(p1) || p1.point_connect.Contains(p2) || p1 == p2) {
            return;
        }
        p1.point_connect.Add(p2);
        p2.point_connect.Add(p1);
    }

    public void ConnectDraw(GameObject lineObj) {
        foreach(Point p in point_connect) {
            Line line = new Line(point, p.Get_vector2());
            line.draw(lineObj);
        }
    }

    public bool IsThis(Vector2 vec) {
        return vec == point;
    }

    public float Getx() {
        return point.x;
    }

    public float Gety() {
        return point.y;
    }

    public Point GetConect(int i) {
        return point_connect[i];
    }

    public int CountConnect() {
        return point_connect.Count;
    }
}

public class Line {
    private Vector2 a;
    private Vector2 b;

    public Line(Vector2 a, Vector2 b) {
        if (a.x > b.x)
            (a, b) = (b, a);
        this.a = a;
        this.b = b;
    }

    public Vector2 GetVector2(int i) {
        if(i == 0) {
            return a;
        } else {
            return b;
        }
    }

    public bool Equals(Line obj) {
        if((obj.a == this.a && obj.b == this.b) || (obj.a == this.b && obj.b == this.a)) {
            return true;
        }
        return false;
    }

    public GameObject draw(GameObject lineObj, float width = 0.1f) {
        Vector2 pos = (a + b) / 2;
        float z = Vector2.Angle(Vector2.up, a - b);
        Quaternion qur = Quaternion.Euler(0, 0, z);
        GameObject obj = GameObject.Instantiate(lineObj, pos, qur);
        obj.transform.localScale = new Vector3(width, Vector2.Distance(a, b), width);
        return obj;
    }
}


class Triangle {
    private Vector2 p0;
    private Vector2 p1;
    private Vector2 p2;

    private Circle cir;

    public Triangle() {

    }

    public Triangle(Vector2 p0, Vector2 p1, Vector2 p2) {
        this.p0 = p0;
        this.p1 = p1;
        this.p2 = p2;

        CircumscribedCircle();
    }

    public Vector2 GetVector2(int i) {
        switch (i) {
            case 0:
                return p0;
            case 1:
                return p1;
            default:
                return p2;
        }
        
    }

    public void CircumscribedCircle() {
        float x1 = p0.x;
        float y1 = p0.y;
        float x2 = p1.x;
        float y2 = p1.y;
        float x3 = p2.x;
        float y3 = p2.y;

        float c = 2.0f * ((x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1));
        float x = ((y3 - y1) * (x2 * x2 - x1 * x1 + y2 * y2 - y1 * y1)
                 + (y1 - y2) * (x3 * x3 - x1 * x1 + y3 * y3 - y1 * y1)) / c;
        float y = ((x1 - x3) * (x2 * x2 - x1 * x1 + y2 * y2 - y1 * y1)
                 + (x2 - x1) * (x3 * x3 - x1 * x1 + y3 * y3 - y1 * y1)) / c;

        Vector2 center = new Vector2(x, y);
        float r = Vector2.Distance(center, p0);

        //Debug.Log("c = 2.0f * ((" + x2 + " - " + x1+") * (" + y3 + " - " + y1 + ") - (" + y2 + " - " + y1 + ") * (" + x3 + " - " +  x1+ "))");

        cir = new Circle(center, r);
    }

    public bool inCircle(Vector2 vec) {
        if (p0 == vec || p1 == vec || p2 == vec) {
            return false;
        }
        return cir.inCircle(vec);
    }

    public bool hasVector2(Vector2 vec) {
        if(p0 == vec || p1 == vec || p2 == vec) {
            return true;
        }
        return false;
    }

    public string GetCirString() {
        return cir.GetToString();
    }

    public GameObject draw(GameObject lineObj) {
        GameObject obj = new GameObject("Triangle");
        Line l1 = new Line(p0, p1);
        Line l2 = new Line(p1, p2);
        Line l3 = new Line(p2, p0);
        l1.draw(lineObj).transform.parent = obj.transform;
        l2.draw(lineObj).transform.parent = obj.transform;
        l3.draw(lineObj).transform.parent = obj.transform;

        return obj;
    }

}

public class Circle {
    Vector2 center;
    float radius;

    public Circle(Vector2 c, float r) {
        this.center = c;
        this.radius = r;
    }

    public Vector2 GetVector2() {
        return center;
    }

    public bool inCircle(Vector2 vec) {
        return radius >= Vector2.Distance(vec, center);
    }

    public string GetToString() {
        return (center + ", " + radius);
    }
}
