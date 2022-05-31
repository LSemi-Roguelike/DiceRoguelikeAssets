using UnityEngine;

namespace Yubunen.Mapmaker.Components {
    class Circle {
        public Vector2 center { get; private set; }
        private float radius;

        public Circle(Vector2 center, float radius) {
            this.center = center;
            this.radius = radius;
        }

        public bool IsInCircle(Vector2 vec) {
            return radius >= Vector2.Distance(vec, center);
        }

        public override string ToString() {
            return $"{center}, {radius}";
        }
    }
}