using System;
using UnityEngine;

namespace Yubunen.Mapmaker.Components {
    class Triangle {
        public Vector2 a { get; private set; }
        public Vector2 b { get; private set; }
        public Vector2 c { get; private set; }

        public Circle circumCircle { get; private set; }

        public Triangle(Vector2 a, Vector2 b, Vector2 c) {
            this.a = a;
            this.b = b;
            this.c = c;

            CalcCircmCircle();
        }

        private void CalcCircmCircle() {
            // lines from a to b and a to c
            var AB = b - a;
            var AC = c - a;
            // perpendicular vector on triangle
            var N = Vector3.Normalize(Vector3.Cross(AB, AC));
            // find the points halfway on AB and AC
            var halfAB = a + AB * 0.5f;
            var halfAC = a + AC * 0.5f;
            // build vectors perpendicular to AB and AC
            var perpAB = Vector3.Cross(AB, N);
            var perpAC = Vector3.Cross(AC, N);
            // find intersection between the two lines
            // D: halfAB + t*perpAB
            // E: halfAC + s*perpAC
            var center = LineLineIntersection(halfAB, perpAB, halfAC, perpAC);
            // the radius is the distance between center and any point
            // distance(A, B) = length(A-B)
            var radius = Vector3.Distance(center, a);

            circumCircle = new Circle(center, radius);
        }

        public bool IsInCircle(Vector2 vec) {
            if (HasVector2(vec))
                return false;
            return circumCircle.IsInCircle(vec);
        }

        public bool HasVector2(Vector2 vec) {
            if (a == vec || b == vec || c == vec) 
                return true;
            return false;
        }

        /// <summary>
        ///     Calculates the intersection point between two lines, assuming that there is such a point.
        /// </summary>
        /// <param name="originD">The origin of the first line</param>
        /// <param name="directionD">The direction of the first line.</param>
        /// <param name="originE">The origin of the second line.</param>
        /// <param name="directionE">The direction of the second line.</param>
        /// <returns>The point at which the two lines intersect.</returns>
        private Vector3 LineLineIntersection(Vector3 originD, Vector3 directionD, Vector3 originE, Vector3 directionE) {
            directionD.Normalize(); directionE.Normalize();
            var N = Vector3.Cross(directionD, directionE);
            var SR = originD - originE; var absX = Math.Abs(N.x);
            var absY = Math.Abs(N.y); var absZ = Math.Abs(N.z);
            float t;
            if (absZ > absX && absZ > absY) {
                t = (SR.x * directionE.y - SR.y * directionE.x) / N.z;
            } else if (absX > absY) {
                t = (SR.y * directionE.z - SR.z * directionE.y) / N.x;
            } else {
                t = (SR.z * directionE.x - SR.x * directionE.z) / N.y;
            }
            return originD - t * directionD;
        }
    }
}