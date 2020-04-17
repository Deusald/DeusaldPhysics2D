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

namespace SharpBox2D
{
    using Box2D;

    internal class OverlapCallback : b2QueryCallback
    {
        #region Public Methods

        internal OverlapCallback(IPhysics2DControl physics2DControl, Physics2D.OverlapAreaCallback callback, ushort collisionMask)
        {
            _Physics2DControl = physics2DControl;
            _CollisionMask    = collisionMask;
            _Callback         = callback;
        }

        public override bool ReportFixture(b2Fixture fixture)
        {
            if ((fixture.GetFilterData().categoryBits & _CollisionMask) == 0) return true;
            ICollider collider = _Physics2DControl.GetPhysicsObject(fixture.GetBody().GetUserData().ToInt32()).GetCollider(fixture.GetUserData().ToInt32());
            return _Callback.Invoke(collider);
        }

        #endregion Public Methods

        #region Private Variables

        private readonly ushort                         _CollisionMask;
        private readonly IPhysics2DControl              _Physics2DControl;
        private readonly Physics2D.OverlapAreaCallback _Callback;

        #endregion Private Variables
    }
}