//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Box2D {

internal class b2ChainShape : b2Shape {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal b2ChainShape(global::System.IntPtr cPtr, bool cMemoryOwn) : base(Box2dPINVOKE.b2ChainShape_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2ChainShape obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          Box2dPINVOKE.delete_b2ChainShape(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public b2ChainShape() : this(Box2dPINVOKE.new_b2ChainShape(), true) {
  }

  public void Clear() {
    Box2dPINVOKE.b2ChainShape_Clear(swigCPtr);
  }

  public void CreateLoop(b2Vec2 vertices, int count) {
    Box2dPINVOKE.b2ChainShape_CreateLoop(swigCPtr, b2Vec2.getCPtr(vertices), count);
  }

  public void CreateChain(b2Vec2 vertices, int count, b2Vec2 prevVertex, b2Vec2 nextVertex) {
    Box2dPINVOKE.b2ChainShape_CreateChain(swigCPtr, b2Vec2.getCPtr(vertices), count, b2Vec2.getCPtr(prevVertex), b2Vec2.getCPtr(nextVertex));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public override int GetChildCount() {
    int ret = Box2dPINVOKE.b2ChainShape_GetChildCount(swigCPtr);
    return ret;
  }

  public void GetChildEdge(b2EdgeShape edge, int index) {
    Box2dPINVOKE.b2ChainShape_GetChildEdge(swigCPtr, b2EdgeShape.getCPtr(edge), index);
  }

  public override bool TestPoint(b2Transform transform, b2Vec2 p) {
    bool ret = Box2dPINVOKE.b2ChainShape_TestPoint(swigCPtr, b2Transform.getCPtr(transform), b2Vec2.getCPtr(p));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool RayCast(b2RayCastOutput output, b2RayCastInput input, b2Transform transform, int childIndex) {
    bool ret = Box2dPINVOKE.b2ChainShape_RayCast(swigCPtr, b2RayCastOutput.getCPtr(output), b2RayCastInput.getCPtr(input), b2Transform.getCPtr(transform), childIndex);
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void ComputeAABB(b2AABB aabb, b2Transform transform, int childIndex) {
    Box2dPINVOKE.b2ChainShape_ComputeAABB(swigCPtr, b2AABB.getCPtr(aabb), b2Transform.getCPtr(transform), childIndex);
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ComputeMass(b2MassData massData, float density) {
    Box2dPINVOKE.b2ChainShape_ComputeMass(swigCPtr, b2MassData.getCPtr(massData), density);
  }

  public int m_count {
    set {
      Box2dPINVOKE.b2ChainShape_m_count_set(swigCPtr, value);
    } 
    get {
      int ret = Box2dPINVOKE.b2ChainShape_m_count_get(swigCPtr);
      return ret;
    } 
  }

  public b2Vec2 m_prevVertex {
    set {
      Box2dPINVOKE.b2ChainShape_m_prevVertex_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2ChainShape_m_prevVertex_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public b2Vec2 m_nextVertex {
    set {
      Box2dPINVOKE.b2ChainShape_m_nextVertex_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2ChainShape_m_nextVertex_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

}

}
