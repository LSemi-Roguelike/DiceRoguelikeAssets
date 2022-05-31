using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yubunen.Mapmaker.Components {
    class Point {
        protected Vector2 point;
        public List<Point> connentPoints { get; protected set; } = new List<Point>();
        public List<Point> arrowPoints { get; protected set; } = new List<Point>();

        public int state { get; private set; }
        public float realCost { get; private set; }
        public float heurCost { get; private set; }
        public float score { get; private set; }
        public Point parent { get; private set; }

        public Point() {
            state = 0;
        }

        public Point(float x, float y) : this(){
            point.Set(x, y);
        }

        public Point(Vector2 point) : this(){
            this.point = point;
        }

        public void Isolate() {
            this.connentPoints.Clear();
        }

        public void Arrow(Point point) {
            if (!connentPoints.Contains(point))
                connentPoints.Add(point);
        }

        public void Arrowed(Point point) {
            point.Arrow(this);
        }

        public static void Connect(Point p1, Point p2) {
            if (p2.connentPoints.Contains(p1) || p1.connentPoints.Contains(p2) || p1 == p2) {
                return;
            }
            p1.connentPoints.Add(p2);
            p2.connentPoints.Add(p1);
        }

        public static void DisConect(Point p1, Point p2) {
            p1.connentPoints.Remove(p2);
            p2.connentPoints.Remove(p1);
        }

        public GameObject Draw(GameObject pointObj, float size = 0.2f) {
            GameObject obj = GameObject.Instantiate(pointObj, point, Quaternion.identity);
            Vector3 scale = new Vector3(size, size, size);
            obj.transform.localScale = scale;
            return obj;
        }

        public void Opened(Point goal) {
            state = 1;
            realCost = 0;
            heurCost = Vector2.Distance(point, goal.point);
            score = realCost + heurCost;
            parent = null;
        }

        public void Opened(Point parent, Point goal) {
            state = 1;
            realCost = parent.realCost + Vector2.Distance(parent.point, point);
            heurCost = Vector2.Distance(point, goal.point);
            score = realCost + heurCost;
            this.parent = parent;
        }

        public List<Point> OpenAround(Point goal) {
            List<Point> addList = new List<Point>();
            foreach(Point point in connentPoints) {
                if (point.state == 0 || point.realCost > this.realCost + Vector2.Distance(point.vector2, this.vector2)) {
                    point.Opened(this, goal);
                    addList.Add(point);
                }
            }
            this.state = 2;

            return addList;
        }

        public static void ResetState(List<Point> points) {
            foreach(Point point in points) {
                point.state = 0;
            }
        }

        public int CompareTo(Point a) {
            return this.score.CompareTo(a.score);
        }

        public bool IsThis(Vector2 vec) {
            return vec == point;
        }

        public Vector2 vector2 {
            get { return point; }
        }

        public float x {
            get { return point.x; }
        }

        public float y {
            get { return point.y; }
        }

        public Point this[int i] {
            get { return connentPoints[i]; }
        }

        public int Count {
            get { return connentPoints.Count; }
        }
    }
}
