#pragma SWIG nowarn=201,312,516
%include "attribute.i"
%include "carrays.i"
%include "arrays_csharp.i"

%module(directors="1") Box2d
%{
#include <box2d.h>
%}

%rename(Equal) operator =;
%rename(PlusEqual) operator +=;
%rename(MinusEqual) operator -=;
%rename(MultiplyEqual) operator *=;
%rename(DivideEqual) operator /=;
%rename(PercentEqual) operator %=;
%rename(Plus) operator +;
%rename(Minus) operator -;
%rename(Multiply) operator *;
%rename(Divide) operator /;
%rename(Percent) operator %;
%rename(Not) operator !;
%rename(IndexIntoConst) operator[](unsigned idx) const;
%rename(IndexInto) operator[](unsigned idx);
%rename(Functor) operator ();
%rename(EqualEqual) operator ==;
%rename(NotEqual) operator !=;
%rename(LessThan) operator <;
%rename(LessThanEqual) operator <=;
%rename(GreaterThan) operator >;
%rename(GreaterThanEqual) operator >=;
%rename(And) operator &&;
%rename(Or) operator ||;
%rename(PlusPlusPrefix) operator++();
%rename(PlusPlusPostfix) operator++(int);
%rename(MinusMinusPrefix) operator--();
%rename(MinusMinusPostfix) operator--(int);

//Directors
%feature("director") b2ContactListener;
%feature("director") b2ContactFilter;
%feature("director") b2DestructionListener;
%feature("director") b2QueryCallback;
%feature("director") b2RayCastCallback;

//Change void* to System.IntPtr
%apply void *VOID_INT_PTR { void * }

%typemap(csvarout, excode=SWIGEXCODE2) void* %{
		get {
				System.IntPtr cPtr = $imcall;$excode
				return cPtr;
		}
%}

//FloatArray
%typemap(csclassmodifiers) FloatArray "internal class"

//Create class for managing the float arrays
%array_class(float, FloatArray);

//Create methods for managing b2Vec arrays
%array_functions(b2Vec2,b2Vec2Array)

//b2AABB
%typemap(csclassmodifiers) b2AABB "internal class"

//b2BlockAllocator
%ignore b2BlockAllocator;

//b2Body
%typemap(csclassmodifiers) b2Body "internal class"
%attribute(b2Body, b2BodyType, Type, GetType, SetType);
%ignore b2Body::GetWorld;
%ignore b2Body::GetContactList;
%ignore b2Body::GetJointList;
%ignore b2Body::GetNext;
%ignore b2Body::Dump;

//b2BodyDef
%typemap(csclassmodifiers) b2BodyDef "internal class"

//b2BodyType
%typemap(csclassmodifiers) b2BodyType "internal enum"

//b2BroadPhase
%ignore b2BroadPhase;

//b2ChainShape
%typemap(csclassmodifiers) b2ChainShape "internal class"
%ignore b2ChainShape::Clone;
%ignore b2ChainShape::m_vertices;

//b2CircleShape
%typemap(csclassmodifiers) b2CircleShape "internal class"
%ignore b2CircleShape::Clone;

//b2ClipVertex
%ignore b2ClipVertex;

//b2Color
%ignore b2Color;

//b2Contact
%typemap(csclassmodifiers) b2Contact "internal class"
%ignore b2Contact::GetNext;
%ignore b2Contact::Evaluate;

//b2ContactEdge
%ignore b2ContactEdge;

//b2ContactFeature
%ignore b2ContactFeature;

//b2ContactFilter
%typemap(csclassmodifiers) b2ContactFilter "internal class"

//b2ContactID
%ignore b2ContactID;

//b2ContactImpulse
%typemap(csclassmodifiers) b2ContactImpulse "internal class"

//b2ContactListener
%typemap(csclassmodifiers) b2ContactListener "internal class"

//b2ContactManager
%typemap(csclassmodifiers) b2ContactManager "internal class"
%ignore b2ContactManager::AddPair;
%ignore b2ContactManager::FindNewContacts;
%ignore b2ContactManager::Destroy;
%ignore b2ContactManager::Collide;
%ignore b2ContactManager::m_contactList;
%ignore b2ContactManager::m_contactCount;
%ignore b2ContactManager::m_broadPhase;
%ignore b2ContactManager::m_allocator;

//b2DestructionListener
%typemap(csclassmodifiers) b2DestructionListener "internal class"

//b2DistanceInput
%typemap(csclassmodifiers) b2DistanceInput "internal class"

//b2DistanceJoint
%typemap(csclassmodifiers) b2DistanceJoint "internal class"
%ignore b2DistanceJoint::Dump;

//b2DistanceJointDef
%typemap(csclassmodifiers) b2DistanceJointDef "internal class"

//b2DistanceOutput
%typemap(csclassmodifiers) b2DistanceOutput "internal class"

//b2DistanceProxy
%typemap(csclassmodifiers) b2DistanceProxy "internal class"
%ignore b2DistanceProxy::GetSupport;
%ignore b2DistanceProxy::GetSupportVertex;
%ignore b2DistanceProxy::GetVertexCount;
%ignore b2DistanceProxy::GetVertex;
%ignore b2DistanceProxy::m_buffer;
%ignore b2DistanceProxy::m_vertices;
%ignore b2DistanceProxy::m_count;
%ignore b2DistanceProxy::m_radius;

//b2Draw
%ignore b2Draw;

//b2DynamicTree
%ignore b2DynamicTree;

//b2EdgeShape
%typemap(csclassmodifiers) b2EdgeShape "internal class"
%ignore b2EdgeShape::Clone;

//b2Filter
%typemap(csclassmodifiers) b2Filter "internal class"

//b2Fixture
%typemap(csclassmodifiers) b2Fixture "internal class"
%ignore b2Fixture::Dump;
%rename(GetShapeType) b2Fixture::GetType;

//b2FixtureDef
%typemap(csclassmodifiers) b2FixtureDef "internal class"

//b2FixtureProxy
%ignore b2FixtureProxy;

//b2FrictionJoint
%typemap(csclassmodifiers) b2FrictionJoint "internal class"
%ignore b2FrictionJoint::Dump;

//b2FrictionJointDef
%typemap(csclassmodifiers) b2FrictionJointDef "internal class"

//b2GearJoint
%typemap(csclassmodifiers) b2GearJoint "internal class"
%ignore b2GearJoint::Dump;

//b2GearJointDef
%typemap(csclassmodifiers) b2GearJointDef "internal class"

//b2Jacobian
%ignore b2Jacobian;

//b2Joint
%typemap(csclassmodifiers) b2Joint "internal class"
%ignore b2Joint::Dump;
%ignore b2Joint::Draw;
%rename(GetJointType) b2Joint::GetType;

//b2JointDef
%typemap(csclassmodifiers) b2JointDef "internal class"

//b2JointType
%typemap(csclassmodifiers) b2JointType "internal enum"

//b2LimitState
%typemap(csclassmodifiers) b2LimitState "internal enum"

//b2JointEdge
%ignore b2JointEdge;

//b2Manifold
%typemap(csclassmodifiers) b2Manifold "internal class"

//b2ManifoldPoint
%typemap(csclassmodifiers) b2ManifoldPoint "internal class"
%ignore b2ManifoldPoint::id;

//b2MassData
%typemap(csclassmodifiers) b2MassData "internal class"

//b2Mat22
%ignore b2Mat22;

//b2Mat33
%ignore b2Mat33;

//b2MotorJoint
%typemap(csclassmodifiers) b2MotorJoint "internal class"

//b2MotorJointDef
%typemap(csclassmodifiers) b2MotorJointDef "internal class"

//b2MouseJoint
%typemap(csclassmodifiers) b2MouseJoint "internal class"

//b2MouseJointDef
%typemap(csclassmodifiers) b2MouseJointDef "internal class"

//b2Pair
%ignore b2Pair;

//b2PointState
%ignore b2PointState;

//b2PolygonShape
%typemap(csclassmodifiers) b2PolygonShape "internal class"
%ignore b2PolygonShape::Clone;

//b2Position
%ignore b2Position;

//b2PrismaticJoint
%typemap(csclassmodifiers) b2PrismaticJoint "internal class"

//b2PrismaticJointDef
%typemap(csclassmodifiers) b2PrismaticJointDef "internal class"

//b2Profile
%typemap(csclassmodifiers) b2Profile "internal class"

//b2PulleyJoint
%typemap(csclassmodifiers) b2PulleyJoint "internal class"

//b2PulleyJointDef
%typemap(csclassmodifiers) b2PulleyJointDef "internal class"

//b2QueryCallback
%typemap(csclassmodifiers) b2QueryCallback "internal class"

//b2RayCastCallback
%typemap(csclassmodifiers) b2RayCastCallback "internal class"

//b2RayCastInput
%typemap(csclassmodifiers) b2RayCastInput "internal class"

//b2RayCastOutput
%typemap(csclassmodifiers) b2RayCastOutput "internal class"

//b2RevoluteJoint
%typemap(csclassmodifiers) b2RevoluteJoint "internal class"

//b2RevoluteJointDef
%typemap(csclassmodifiers) b2RevoluteJointDef "internal class"

//b2RopeJoint
%typemap(csclassmodifiers) b2RopeJoint "internal class"

//b2RopeJointDef
%typemap(csclassmodifiers) b2RopeJointDef "internal class"

//b2Rot
%typemap(csclassmodifiers) b2Rot "internal class"

//b2Shape
%typemap(csclassmodifiers) b2Shape "internal class"
%ignore b2Shape::Clone;
%rename(GetShapeType) b2Shape::GetType;

//b2ShapeCastInput
%typemap(csclassmodifiers) b2ShapeCastInput "internal class"

//b2ShapeCastOutput
%typemap(csclassmodifiers) b2ShapeCastOutput "internal class"

//b2SimplexCache
%typemap(csclassmodifiers) b2SimplexCache "internal class"
%ignore b2SimplexCache::indexA;
%ignore b2SimplexCache::indexB;
%ignore b2SimplexCache::metric;

//b2SolverData
%ignore b2SolverData;

//b2StackAllocator
%ignore b2StackAllocator;

//b2StackEntry
%ignore b2StackEntry;

//b2Sweep
%typemap(csclassmodifiers) b2Sweep "internal class"

//b2Timer
%ignore b2Timer;

//b2TimeStep
%ignore b2TimeStep;

//b2Transform
%typemap(csclassmodifiers) b2Transform "internal class"

//b2TreeNode
%ignore b2TreeNode;

//b2Vec2
%typemap(csclassmodifiers) b2Vec2 "internal class"

//b2Vec3
%typemap(csclassmodifiers) b2Vec3 "internal class"

//b2Velocity
%ignore b2Velocity;

//b2Version
%typemap(csclassmodifiers) b2Version "internal class"

//b2WeldJoint
%typemap(csclassmodifiers) b2WeldJoint "internal class"

//b2WeldJointDef
%typemap(csclassmodifiers) b2WeldJointDef "internal class"

//b2WheelJoint
%typemap(csclassmodifiers) b2WheelJoint "internal class"

//b2WheelJointDef
%typemap(csclassmodifiers) b2WheelJointDef "internal class"

//b2World
%typemap(csclassmodifiers) b2World "internal class"
%ignore b2World::SetDebugDraw;
%ignore b2World::DebugDraw;
%ignore b2World::Dump;
%ignore b2World::GetContactList;

//b2WorldManifold
%typemap(csclassmodifiers) b2WorldManifold "internal class"

//Box2D
%pragma(csharp) moduleclassmodifiers="internal class"
%ignore b2ContactRegister;
%ignore b2GetPointStates;
%ignore b2Alloc;
%ignore b2Free;
%ignore b2Log;
%ignore b2IsValid;
%ignore b2Mul;
%ignore operator + (const b2Mat22& A, const b2Mat22& B);
%ignore b2MulT;
%ignore b2Mul22;
%ignore b2Abs;
%ignore b2NextPowerOfTwo;
%ignore b2IsPowerOfTwo;
%ignore b2_nullFeature;
%ignore b2CollideCircles;
%ignore b2CollidePolygonAndCircle;
%ignore b2CollidePolygons;
%ignore b2CollideEdgeAndCircle;
%ignore b2CollideEdgeAndPolygon;
%ignore b2ClipSegmentToLine;
%ignore b2_blockSizeCount;
%ignore b2_stackSize;
%ignore b2_maxStackEntries;
%ignore b2_minPulleyLength;

#include "box2d.h"
