using System.ComponentModel;
using System.Diagnostics;
using Vector2 = DeusaldSharp.Vector2;
using Box2DSharp.Collision.Collider;
using Box2DSharp.Common;

namespace Box2DSharp.Dynamics.Contacts
{
    internal struct PositionSolverManifold
    {
        public Vector2 Normal;

        public Vector2 Point;

        public float Separation;

        public void Initialize(in ContactPositionConstraint pc, in Transform xfA, in Transform xfB, int index)
        {
            Debug.Assert(pc.PointCount > 0);

            switch (pc.Type)
            {
            case ManifoldType.Circles:
            {
                //var pointA = MathUtils.Mul(xfA, pc.LocalPoint); // inline
                var x = xfA.Rotation.Cos * pc.LocalPoint.x - xfA.Rotation.Sin * pc.LocalPoint.y + xfA.Position.x;
                var y = xfA.Rotation.Sin * pc.LocalPoint.x + xfA.Rotation.Cos * pc.LocalPoint.y + xfA.Position.y;
                var pointA = new Vector2(x, y);

                // var pointB = MathUtils.Mul(xfB, pc.LocalPoints.Value0); // inline
                x = xfB.Rotation.Cos * pc.LocalPoints.Value0.x - xfB.Rotation.Sin * pc.LocalPoints.Value0.y + xfB.Position.x;
                y = xfB.Rotation.Sin * pc.LocalPoints.Value0.x + xfB.Rotation.Cos * pc.LocalPoints.Value0.y + xfB.Position.y;
                var pointB = new Vector2(x, y);

                Normal = pointB - pointA;
                Normal.Normalize();
                Point = 0.5f * (pointA + pointB);
                Separation = Vector2.Dot(pointB - pointA, Normal) - pc.RadiusA - pc.RadiusB;
            }
                break;

            case ManifoldType.FaceA:
            {
                // Normal = MathUtils.Mul(xfA.Rotation, pc.LocalNormal); // inline
                Normal = new Vector2(
                    xfA.Rotation.Cos * pc.LocalNormal.x - xfA.Rotation.Sin * pc.LocalNormal.y,
                    xfA.Rotation.Sin * pc.LocalNormal.x + xfA.Rotation.Cos * pc.LocalNormal.y);

                // var planePoint = MathUtils.Mul(xfA, pc.LocalPoint); // inline
                var x = xfA.Rotation.Cos * pc.LocalPoint.x - xfA.Rotation.Sin * pc.LocalPoint.y + xfA.Position.x;
                var y = xfA.Rotation.Sin * pc.LocalPoint.x + xfA.Rotation.Cos * pc.LocalPoint.y + xfA.Position.y;
                var planePoint = new Vector2(x, y);

                // var clipPoint = MathUtils.Mul(xfB, pc.LocalPoints[index]); // inline

                if (index == 0)
                {
                    x = xfB.Rotation.Cos * pc.LocalPoints.Value0.x - xfB.Rotation.Sin * pc.LocalPoints.Value0.y + xfB.Position.x;
                    y = xfB.Rotation.Sin * pc.LocalPoints.Value0.x + xfB.Rotation.Cos * pc.LocalPoints.Value0.y + xfB.Position.y;
                }
                else
                {
                    x = xfB.Rotation.Cos * pc.LocalPoints.Value1.x - xfB.Rotation.Sin * pc.LocalPoints.Value1.y + xfB.Position.x;
                    y = xfB.Rotation.Sin * pc.LocalPoints.Value1.x + xfB.Rotation.Cos * pc.LocalPoints.Value1.y + xfB.Position.y;
                }

                var clipPoint = new Vector2(x, y);

                Separation = Vector2.Dot(clipPoint - planePoint, Normal) - pc.RadiusA - pc.RadiusB;
                Point = clipPoint;
            }
                break;

            case ManifoldType.FaceB:
            {
                // Normal = MathUtils.Mul(xfB.Rotation, pc.LocalNormal); // inline
                Normal = new Vector2(
                    xfB.Rotation.Cos * pc.LocalNormal.x - xfB.Rotation.Sin * pc.LocalNormal.y,
                    xfB.Rotation.Sin * pc.LocalNormal.x + xfB.Rotation.Cos * pc.LocalNormal.y);

                // var planePoint = MathUtils.Mul(xfB, pc.LocalPoint); // inline
                var x = xfB.Rotation.Cos * pc.LocalPoint.x - xfB.Rotation.Sin * pc.LocalPoint.y + xfB.Position.x;
                var y = xfB.Rotation.Sin * pc.LocalPoint.x + xfB.Rotation.Cos * pc.LocalPoint.y + xfB.Position.y;
                var planePoint = new Vector2(x, y);

                // var clipPoint = MathUtils.Mul(xfA, pc.LocalPoints[index]); // inline
                if (index == 0)
                {
                    x = xfA.Rotation.Cos * pc.LocalPoints.Value0.x - xfA.Rotation.Sin * pc.LocalPoints.Value0.y + xfA.Position.x;
                    y = xfA.Rotation.Sin * pc.LocalPoints.Value0.x + xfA.Rotation.Cos * pc.LocalPoints.Value0.y + xfA.Position.y;
                }
                else
                {
                    x = xfA.Rotation.Cos * pc.LocalPoints.Value1.x - xfA.Rotation.Sin * pc.LocalPoints.Value1.y + xfA.Position.x;
                    y = xfA.Rotation.Sin * pc.LocalPoints.Value1.x + xfA.Rotation.Cos * pc.LocalPoints.Value1.y + xfA.Position.y;
                }

                var clipPoint = new Vector2(x, y);

                Separation = Vector2.Dot(clipPoint - planePoint, Normal) - pc.RadiusA - pc.RadiusB;
                Point = clipPoint;

                // Ensure normal points from A to B
                Normal = -Normal;
            }
                break;
            default:
                throw new InvalidEnumArgumentException($"Invalid ManifoldType: {pc.Type}");
            }
        }
    }
}