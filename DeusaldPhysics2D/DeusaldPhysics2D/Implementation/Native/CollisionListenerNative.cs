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
    internal class CollisionListenerNative : b2ContactListener
    {
        #region Variables

        private readonly Physics2DNative _Physics2D;

        #endregion Private Variables

        #region Init Methods

        public CollisionListenerNative(Physics2DNative physics2DControl)
        {
            _Physics2D = physics2DControl;
        }

        #endregion Init Methods

        #region Public Methods

        public override void BeginContact(b2Contact contact)
        {
            ICollisionData collisionData = GetCollisionData(contact);
            _Physics2D.ExecuteOnCollisionEnter(collisionData);
            ((ColliderNative) collisionData.ColliderA).ExecuteOnCollisionEnter(collisionData);
            ((CollisionDataNative) collisionData).Swap();
            ((ColliderNative) collisionData.ColliderA).ExecuteOnCollisionEnter(collisionData);
        }

        public override void EndContact(b2Contact contact)
        {
            ICollisionData collisionData = GetCollisionData(contact);
            _Physics2D.ExecuteOnCollisionExit(collisionData);
            ((ColliderNative) collisionData.ColliderA).ExecuteOnCollisionExit(collisionData);
            ((CollisionDataNative) collisionData).Swap();
            ((ColliderNative) collisionData.ColliderA).ExecuteOnCollisionExit(collisionData);
        }

        public override void PreSolve(b2Contact contact, b2Manifold oldManifold)
        {
            ICollisionDataExtend collisionData = GetCollisionData(contact);
            _Physics2D.ExecutePreCollision(collisionData);
            contact.SetEnabled(!collisionData.ContactDisabled);
        }

        #endregion Public Methods

        #region Private Methods

        private CollisionDataNative GetCollisionData(b2Contact contact)
        {
            int colliderAId      = contact.GetFixtureA().GetUserData().data;
            int colliderBId      = contact.GetFixtureB().GetUserData().data;
            int physicsObjectAId = contact.GetFixtureA().GetBody().GetUserData().data;
            int physicsObjectBId = contact.GetFixtureB().GetBody().GetUserData().data;
            int childIndexA      = contact.GetChildIndexA();
            int childIndexB      = contact.GetChildIndexB();

            IPhysicsObject physicsObjectA = _Physics2D.GetPhysicsObject(physicsObjectAId);
            ICollider      colliderA      = physicsObjectA.GetCollider(colliderAId);
            IPhysicsObject physicsObjectB = _Physics2D.GetPhysicsObject(physicsObjectBId);
            ICollider      colliderB      = physicsObjectB.GetCollider(colliderBId);

            return new CollisionDataNative(contact, physicsObjectA, colliderA, childIndexA, physicsObjectB, colliderB, childIndexB);
        }

        #endregion Private Methods
    }
}