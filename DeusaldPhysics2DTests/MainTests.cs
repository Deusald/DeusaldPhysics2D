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

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using System;
using Box2D;
using DeusaldSharp;
using NUnit.Framework;
using DeusaldPhysics2D;

namespace DeusaldPhysics2DTests
{
    public class MainTests
    {
        [Test]
        [TestOf(nameof(Box2D))]
        public void XWTMN()
        {
            // Arrange
            Box2dNativeLoader.LoadNativeLibrary(Box2dNativeLoader.System.Windows);
            Console.Write(DeusaldPhysics2D.DeusaldPhysics2D.IsInitialized);
            IPhysics2DControl physics2D = DeusaldPhysics2D.DeusaldPhysics2D.CreateNewPhysics(20, Vector2.Down);

            int collisionObjectAId = -1;
            int collisionObjectBId = -1;
            Vector2 collisionNormal = Vector2.Zero;

            physics2D.OnCollisionEnter += data =>
            {
                collisionObjectAId = data.PhysicsObjectA.PhysicsObjectId;
                collisionObjectBId = data.PhysicsObjectB.PhysicsObjectId;
                collisionNormal    = data.Normal;
            };

            IPhysicsObject objOne = physics2D.CreatePhysicsObject(BodyType.Dynamic, Vector2.Zero, 0f);
            objOne.AddBoxCollider(1, 1);

            IPhysicsObject objTwo = physics2D.CreatePhysicsObject(BodyType.Static, new Vector2(0, -25f), 0f);
            objTwo.AddBoxCollider(1, 1);

            // Act
            for (int i = 0; i < 1000; ++i)
            {
                physics2D.Step();
            }
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, collisionObjectAId);
                Assert.AreEqual(2, collisionObjectBId);
                Assert.AreEqual(new Vector2(0, -1), collisionNormal);
            });
        }
    }
}