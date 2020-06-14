// MIT License

// SharpBox2D:
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

using System;
using System.Collections.Generic;
using Box2D;
using DeusaldSharp;

namespace SharpBox2D
{
    public class Physics2DControl : Physics2D, IPhysics2DControl
    {
        #region Variables

        public static readonly Version Version = new Version(1, 0, 0);

        private readonly float _PhysicsTimeStep;

        private const int _VelocityIterations = 8;
        private const int _PositionIterations = 3;

        #endregion Variables

        #region Properties

        public Vector2 Gravity
        {
            get
            {
                b2Vec2 gravity = _World.GetGravity();
                return new Vector2(gravity.x, gravity.y);
            }
            set => _World.SetGravity(new b2Vec2(value.x, value.y));
        }

        public Dictionary<int, IPhysicsObject>.Enumerator PhysicsObjects => _PhysicsObjects.GetEnumerator();

        #endregion Properties

        #region Init Methods

        public Physics2DControl(uint physicsStepsPerSec, Vector2 gravity) : base(physicsStepsPerSec, gravity)
        {
            _PhysicsTimeStep = 1f / physicsStepsPerSec;
            _World.SetContactListener(new CollisionListener(this));
        }

        #endregion Init Methods

        #region Public Methods

        public void Step()
        {
            _UpdateLinearVelocity?.Invoke();
            _World.Step(_PhysicsTimeStep, _VelocityIterations, _PositionIterations);
        }

        public IPhysicsObject GetPhysicsObject(int physicsObjectId)
        {
            return !_PhysicsObjects.ContainsKey(physicsObjectId) ? null : _PhysicsObjects[physicsObjectId];
        }

        #endregion Public Methods
    }
}