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

namespace DeusaldPhysics2D
{
    internal class OverlapCallbackNative : b2QueryCallback
    {
        #region Variables

        private readonly ushort                        _CollisionMask;
        private readonly IPhysics2DControl             _Physics2DControl;
        private readonly Delegates.OverlapAreaCallback _Callback;

        #endregion Variables

        #region Init Methods

        internal OverlapCallbackNative(IPhysics2DControl physics2DControl, Delegates.OverlapAreaCallback callback, ushort collisionMask)
        {
            _Physics2DControl = physics2DControl;
            _CollisionMask    = collisionMask;
            _Callback         = callback;
        }

        #endregion Init Methods

        #region Public Methods

        public override bool ReportFixture(b2Fixture fixture)
        {
            if ((fixture.GetFilterData().categoryBits & _CollisionMask) == 0) return true;
            ICollider collider = _Physics2DControl.GetPhysicsObject(fixture.GetBody().GetUserData().data).GetCollider(fixture.GetUserData().data);
            return _Callback.Invoke(collider);
        }

        #endregion Public Methods
    }
}