using UnityEngine;

namespace Yubunen.Mapmaker.Components {
    class Line {
        public Vector2 a { get; private set; }
        public Vector2 b { get; private set; }

        public Line(Vector2 a, Vector2 b) {
            if (a.x > b.x)
                (a, b) = (b, a); //Swaping
            this.a = a;
            this.b = b;
        }

        public bool Equals(Line obj) {
            if ((obj.a == this.a && obj.b == this.b) || (obj.a == this.b && obj.b == this.a)) {
                return true;
            }
            return false;
        }
        
        public GameObject Draw(GameObject lineObj, float width = 0.1f) {
            Vector2 pos = (a + b) / 2;
            float z = Vector2.Angle(Vector2.up, a - b);
            Quaternion qur = Quaternion.Euler(0, 0, z);
            GameObject obj = GameObject.Instantiate(lineObj, pos, qur);
            obj.transform.localScale = new Vector3(width, Vector2.Distance(a, b), width);
            return obj;
        }
    }
}
