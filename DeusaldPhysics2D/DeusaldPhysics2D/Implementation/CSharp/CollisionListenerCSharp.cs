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

using Box2DSharp.Collision.Collider;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace DeusaldPhysics2D
{
    internal class CollisionListenerCSharp : IContactListener
    {
        #region Variables

        private readonly Physics2DCSharp _Physics2D;

        #endregion Private Variables

        #region Init Methods

        public CollisionListenerCSharp(Physics2DCSharp physics2DControl)
        {
            _Physics2D = physics2DControl;
        }

        #endregion Init Methods

        #region Public Methods

        public void BeginContact(Contact contact)
        {
            ICollisionData collisionData = GetCollisionData(contact);
            _Physics2D.ExecuteOnCollisionEnter(collisionData);
            ((ColliderCSharp)collisionData.ColliderA).ExecuteOnCollisionEnter(collisionData);
            ((CollisionDataCSharp)collisionData).Swap();
            ((ColliderCSharp)collisionData.ColliderA).ExecuteOnCollisionEnter(collisionData);
        }

        public void EndContact(Contact contact)
        {
            ICollisionData collisionData = GetCollisionData(contact);
            _Physics2D.ExecuteOnCollisionExit(collisionData);
            ((ColliderCSharp)collisionData.ColliderA).ExecuteOnCollisionExit(collisionData);
            ((CollisionDataCSharp)collisionData).Swap();
            ((ColliderCSharp)collisionData.ColliderA).ExecuteOnCollisionExit(collisionData);
        }

        public void PostSolve(Contact contact, in ContactImpulse impulse) { }

        public void PreSolve(Contact contact, in Manifold oldManifold)
        {
            ICollisionDataExtend collisionData = GetCollisionData(contact);
            _Physics2D.ExecutePreCollision(collisionData);
            contact.SetEnabled(!collisionData.ContactDisabled);
        }

        #endregion Public Methods

        #region Private Methods

        private CollisionDataCSharp GetCollisionData(Contact contact)
        {
            int colliderAId      = (int)contact.FixtureA.UserData;
            int colliderBId      = (int)contact.FixtureB.UserData;
            int physicsObjectAId = (int)contact.FixtureA.Body.UserData;
            int physicsObjectBId = (int)contact.FixtureB.Body.UserData;
            int childIndexA      = contact.ChildIndexA;
            int childIndexB      = contact.ChildIndexB;

            IPhysicsObject physicsObjectA = _Physics2D.GetPhysicsObject(physicsObjectAId);
            ICollider      colliderA      = physicsObjectA.GetCollider(colliderAId);
            IPhysicsObject physicsObjectB = _Physics2D.GetPhysicsObject(physicsObjectBId);
            ICollider      colliderB      = physicsObjectB.GetCollider(colliderBId);

            return new CollisionDataCSharp(contact, physicsObjectA, colliderA, childIndexA, physicsObjectB, colliderB, childIndexB);
        }

        #endregion Private Methods
    }
}