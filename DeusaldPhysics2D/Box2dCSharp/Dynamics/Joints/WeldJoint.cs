using System;
using Vector2 = DeusaldSharp.Vector2;
using Box2DSharp.Common;
using DeusaldSharp;
using MathUtils = Box2DSharp.Common.MathUtils;

namespace Box2DSharp.Dynamics.Joints
{
    /// A weld joint essentially glues two bodies together. A weld joint may
    /// distort somewhat because the island constraint solver is approximate.
    internal class WeldJoint : Joint
    {
        // Solver shared
        private readonly Vector2 _localAnchorA;

        private readonly Vector2 _localAnchorB;

        private readonly float _referenceAngle;

        private float _bias;

        public float Stiffness = 0.0f;

        public float Damping = 0.0f;

        private float _gamma;

        private Vector3 _impulse;

        // Solver temp
        private int _indexA;

        private int _indexB;

        private float _invIa;

        private float _invIb;

        private float _invMassA;

        private float _invMassB;

        private Vector2 _localCenterA;

        private Vector2 _localCenterB;

        private Matrix3x3 _mass;

        private Vector2 _rA;

        private Vector2 _rB;

        internal WeldJoint(WeldJointDef def)
            : base(def)
        {
            _localAnchorA = def.LocalAnchorA;
            _localAnchorB = def.LocalAnchorB;
            _referenceAngle = def.ReferenceAngle;
            Damping = def.Damping;
            Stiffness = def.Stiffness;

            _impulse.SetZero();
        }

        /// The local anchor point relative to bodyA's origin.
        public Vector2 GetLocalAnchorA()
        {
            return _localAnchorA;
        }

        /// The local anchor point relative to bodyB's origin.
        public Vector2 GetLocalAnchorB()
        {
            return _localAnchorB;
        }

        /// Get the reference angle.
        public float GetReferenceAngle()
        {
            return _referenceAngle;
        }

        /// <inheritdoc />
        public override Vector2 GetAnchorA()
        {
            return BodyA.GetWorldPoint(_localAnchorA);
        }

        /// <inheritdoc />
        public override Vector2 GetAnchorB()
        {
            return BodyB.GetWorldPoint(_localAnchorB);
        }

        /// <inheritdoc />
        public override Vector2 GetReactionForce(float inv_dt)
        {
            var P = new Vector2(_impulse.x, _impulse.y);
            return inv_dt * P;
        }

        /// <inheritdoc />
        public override float GetReactionTorque(float inv_dt)
        {
            return inv_dt * _impulse.z;
        }

        /// Dump to Logger.Log
        public override void Dump()
        {
           // Todo
        }

        /// <inheritdoc />
        internal override void InitVelocityConstraints(in SolverData data)
        {
            _indexA = BodyA.IslandIndex;
            _indexB = BodyB.IslandIndex;
            _localCenterA = BodyA.Sweep.LocalCenter;
            _localCenterB = BodyB.Sweep.LocalCenter;
            _invMassA = BodyA.InvMass;
            _invMassB = BodyB.InvMass;
            _invIa = BodyA.InverseInertia;
            _invIb = BodyB.InverseInertia;

            var aA = data.Positions[_indexA].Angle;
            var vA = data.Velocities[_indexA].V;
            var wA = data.Velocities[_indexA].W;

            var aB = data.Positions[_indexB].Angle;
            var vB = data.Velocities[_indexB].V;
            var wB = data.Velocities[_indexB].W;

            var qA = new Rotation(aA);
            var qB = new Rotation(aB);

            _rA = MathUtils.Mul(qA, _localAnchorA - _localCenterA);
            _rB = MathUtils.Mul(qB, _localAnchorB - _localCenterB);

            // J = [-I -r1_skew I r2_skew]
            //     [ 0       -1 0       1]
            // r_skew = [-ry; rx]

            // Matlab
            // K = [ mA+r1y^2*iA+mB+r2y^2*iB,  -r1y*iA*r1x-r2y*iB*r2x,          -r1y*iA-r2y*iB]
            //     [  -r1y*iA*r1x-r2y*iB*r2x, mA+r1x^2*iA+mB+r2x^2*iB,           r1x*iA+r2x*iB]
            //     [          -r1y*iA-r2y*iB,           r1x*iA+r2x*iB,                   iA+iB]

            float mA = _invMassA, mB = _invMassB;
            float iA = _invIa, iB = _invIb;

            var K = new Matrix3x3();
            K.Ex.x = mA + mB + _rA.y * _rA.y * iA + _rB.y * _rB.y * iB;
            K.Ey.x = -_rA.y * _rA.x * iA - _rB.y * _rB.x * iB;
            K.Ez.x = -_rA.y * iA - _rB.y * iB;
            K.Ex.y = K.Ey.x;
            K.Ey.y = mA + mB + _rA.x * _rA.x * iA + _rB.x * _rB.x * iB;
            K.Ez.y = _rA.x * iA + _rB.x * iB;
            K.Ex.z = K.Ez.x;
            K.Ey.z = K.Ez.y;
            K.Ez.z = iA + iB;

            if (Stiffness > 0.0f)
            {
                K.GetInverse22(ref _mass);

                var invM = iA + iB;

                var C = aB - aA - _referenceAngle;

                // Damping coefficient
                var d = Damping;

                // Spring stiffness
                var k = Stiffness;

                // magic formulas
                var h = data.Step.Dt;
                _gamma = h * (d + h * k);
                _gamma = !_gamma.Equals(0.0f) ? 1.0f / _gamma : 0.0f;
                _bias = C * h * k * _gamma;

                invM += _gamma;
                _mass.Ez.z = !invM.Equals(0.0f) ? 1.0f / invM : 0.0f;
            }
            else if (K.Ez.z.Equals(0.0f))
            {
                K.GetInverse22(ref _mass);
                _gamma = 0.0f;
                _bias = 0.0f;
            }
            else
            {
                K.GetSymInverse33(ref _mass);
                _gamma = 0.0f;
                _bias = 0.0f;
            }

            if (data.Step.WarmStarting)
            {
                // Scale impulses to support a variable time step.
                _impulse *= data.Step.DtRatio;

                var P = new Vector2(_impulse.x, _impulse.y);

                vA -= mA * P;
                wA -= iA * (MathUtils.Cross(_rA, P) + _impulse.z);

                vB += mB * P;
                wB += iB * (MathUtils.Cross(_rB, P) + _impulse.z);
            }
            else
            {
                _impulse.SetZero();
            }

            data.Velocities[_indexA].V = vA;
            data.Velocities[_indexA].W = wA;
            data.Velocities[_indexB].V = vB;
            data.Velocities[_indexB].W = wB;
        }

        /// <inheritdoc />
        internal override void SolveVelocityConstraints(in SolverData data)
        {
            var vA = data.Velocities[_indexA].V;
            var wA = data.Velocities[_indexA].W;
            var vB = data.Velocities[_indexB].V;
            var wB = data.Velocities[_indexB].W;

            float mA = _invMassA, mB = _invMassB;
            float iA = _invIa, iB = _invIb;

            if (Stiffness > 0.0f)
            {
                var Cdot2 = wB - wA;

                var impulse2 = -_mass.Ez.z * (Cdot2 + _bias + _gamma * _impulse.z);
                _impulse.z += impulse2;

                wA -= iA * impulse2;
                wB += iB * impulse2;

                var Cdot1 = vB + MathUtils.Cross(wB, _rB) - vA - MathUtils.Cross(wA, _rA);

                var impulse1 = -MathUtils.Mul22(_mass, Cdot1);
                _impulse.x += impulse1.x;
                _impulse.y += impulse1.y;

                var P = impulse1;

                vA -= mA * P;
                wA -= iA * MathUtils.Cross(_rA, P);

                vB += mB * P;
                wB += iB * MathUtils.Cross(_rB, P);
            }
            else
            {
                var cdot1 = vB + MathUtils.Cross(wB, _rB) - vA - MathUtils.Cross(wA, _rA);
                var cdot2 = wB - wA;
                var cdot = new Vector3(cdot1.x, cdot1.y, cdot2);

                var impulse = -MathUtils.Mul(_mass, cdot);
                _impulse += impulse;

                var P = new Vector2(impulse.x, impulse.y);

                vA -= mA * P;
                wA -= iA * (MathUtils.Cross(_rA, P) + impulse.z);

                vB += mB * P;
                wB += iB * (MathUtils.Cross(_rB, P) + impulse.z);
            }

            data.Velocities[_indexA].V = vA;
            data.Velocities[_indexA].W = wA;
            data.Velocities[_indexB].V = vB;
            data.Velocities[_indexB].W = wB;
        }

        /// <inheritdoc />
        internal override bool SolvePositionConstraints(in SolverData data)
        {
            var cA = data.Positions[_indexA].Center;
            var aA = data.Positions[_indexA].Angle;
            var cB = data.Positions[_indexB].Center;
            var aB = data.Positions[_indexB].Angle;

            var qA = new Rotation(aA);
            var qB = new Rotation(aB);

            float mA = _invMassA, mB = _invMassB;
            float iA = _invIa, iB = _invIb;

            var rA = MathUtils.Mul(qA, _localAnchorA - _localCenterA);
            var rB = MathUtils.Mul(qB, _localAnchorB - _localCenterB);

            float positionError, angularError;

            var K = new Matrix3x3();
            K.Ex.x = mA + mB + rA.y * rA.y * iA + rB.y * rB.y * iB;
            K.Ey.x = -rA.y * rA.x * iA - rB.y * rB.x * iB;
            K.Ez.x = -rA.y * iA - rB.y * iB;
            K.Ex.y = K.Ey.x;
            K.Ey.y = mA + mB + rA.x * rA.x * iA + rB.x * rB.x * iB;
            K.Ez.y = rA.x * iA + rB.x * iB;
            K.Ex.z = K.Ez.x;
            K.Ey.z = K.Ez.y;
            K.Ez.z = iA + iB;

            if (Stiffness > 0.0f)
            {
                var C1 = cB + rB - cA - rA;

                positionError = C1.Length();
                angularError = 0.0f;

                var P = -K.Solve22(C1);

                cA -= mA * P;
                aA -= iA * MathUtils.Cross(rA, P);

                cB += mB * P;
                aB += iB * MathUtils.Cross(rB, P);
            }
            else
            {
                var C1 = cB + rB - cA - rA;
                var C2 = aB - aA - _referenceAngle;

                positionError = C1.Length();
                angularError = Math.Abs(C2);

                var C = new Vector3(C1.x, C1.y, C2);

                var impulse = new Vector3();
                if (K.Ez.z > 0.0f)
                {
                    impulse = -K.Solve33(C);
                }
                else
                {
                    var impulse2 = -K.Solve22(C1);
                    impulse.Set(impulse2.x, impulse2.y, 0.0f);
                }

                var P = new Vector2(impulse.x, impulse.y);

                cA -= mA * P;
                aA -= iA * (MathUtils.Cross(rA, P) + impulse.z);

                cB += mB * P;
                aB += iB * (MathUtils.Cross(rB, P) + impulse.z);
            }

            data.Positions[_indexA].Center = cA;
            data.Positions[_indexA].Angle = aA;
            data.Positions[_indexB].Center = cB;
            data.Positions[_indexB].Angle = aB;

            return positionError <= Settings.LinearSlop && angularError <= Settings.AngularSlop;
        }
    }
}