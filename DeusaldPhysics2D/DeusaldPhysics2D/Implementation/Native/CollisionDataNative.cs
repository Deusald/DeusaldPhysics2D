// MIT License

// DeusaldPhysics2D:
// Copyright (c) 2020 Adam "Deusald" Orliński

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Box2D;
using DeusaldSharp;

namespace DeusaldPhysics2D
{
    internal class CollisionDataNative : ICollisionData, ICollisionDataExtend
    {
        #region Variables

        private bool                 _DetailsCalculated;
        private TypeVector2<Vector2> _ContactPoints;
        private Vector2              _Normal;
        private bool                 _Swapped;

        private readonly b2Contact      _Contact;
        private readonly IPhysicsObject _PhysicsObjectA;
        private readonly ICollider      _ColliderA;
        private readonly int            _ChildIndexA;
        private readonly IPhysicsObject _PhysicsObjectB;
        private readonly ICollider      _ColliderB;
        private readonly int            _ChildIndexB;

        #endregion Variables

        #region Properties

        public IPhysicsObject PhysicsObjectA  => _Swapped ? _PhysicsObjectB : _PhysicsObjectA;
        public ICollider      ColliderA       => _Swapped ? _ColliderB : _ColliderA;
        public int            ChildIndexA     => _Swapped ? _ChildIndexB : _ChildIndexA;
        public IPhysicsObject PhysicsObjectB  => _Swapped ? _PhysicsObjectA : _PhysicsObjectB;
        public ICollider      ColliderB       => _Swapped ? _ColliderA : _ColliderB;
        public int            ChildIndexB     => _Swapped ? _ChildIndexA : _ChildIndexB;
        public bool           ContactDisabled { get; private set; }

        public TypeVector2<Vector2> ContactPoints
        {
            get
            {
                if (!_DetailsCalculated)
                    CalculateDetails();

                return _ContactPoints;
            }
        }

        public Vector2 Normal
        {
            get
            {
                if (!_DetailsCalculated)
                    CalculateDetails();

                return _Swapped ? _Normal.Negated : _Normal;
            }
        }

        #endregion Properties

        #region Init Methods

        internal CollisionDataNative(b2Contact contact, IPhysicsObject physicsObjectA, ICollider colliderA, int childIndexA,
                               IPhysicsObject physicsObjectB, ICollider colliderB, int childIndexB)
        {
            _Contact           = contact;
            _PhysicsObjectA    = physicsObjectA;
            _ColliderA         = colliderA;
            _ChildIndexA       = childIndexA;
            _PhysicsObjectB    = physicsObjectB;
            _ColliderB         = colliderB;
            _ChildIndexB       = childIndexB;
            _DetailsCalculated = false;
            _Swapped           = false;
            ContactDisabled    = false;
        }

        #endregion Init Methods

        #region Public Methods

        internal void Swap()
        {
            _Swapped = true;
        }

        public void DisableContact()
        {
            ContactDisabled = true;
        }

        #endregion Public Methods

        #region Private Methods

        private void CalculateDetails()
        {
            _DetailsCalculated = true;
            _ContactPoints     = new TypeVector2<Vector2>();

            b2WorldManifold worldManifold = new b2WorldManifold();
            _Contact.GetWorldManifold(worldManifold);

            _Normal          = worldManifold.normal.ToVector2();
            _ContactPoints.x = Box2d.b2Vec2Array_getitem(worldManifold.points, 0).ToVector2();
            _ContactPoints.y = Box2d.b2Vec2Array_getitem(worldManifold.points, 1).ToVector2();
        }

        #endregion Private Methods
    }
}