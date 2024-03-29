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

internal class b2Shape : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2Shape(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2Shape obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2Shape() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          Box2dPINVOKE.delete_b2Shape(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public b2Shape.Type GetShapeType() {
    b2Shape.Type ret = (b2Shape.Type)Box2dPINVOKE.b2Shape_GetShapeType(swigCPtr);
    return ret;
  }

  public virtual int GetChildCount() {
    int ret = Box2dPINVOKE.b2Shape_GetChildCount(swigCPtr);
    return ret;
  }

  public virtual bool TestPoint(b2Transform xf, b2Vec2 p) {
    bool ret = Box2dPINVOKE.b2Shape_TestPoint(swigCPtr, b2Transform.getCPtr(xf), b2Vec2.getCPtr(p));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool RayCast(b2RayCastOutput output, b2RayCastInput input, b2Transform transform, int childIndex) {
    bool ret = Box2dPINVOKE.b2Shape_RayCast(swigCPtr, b2RayCastOutput.getCPtr(output), b2RayCastInput.getCPtr(input), b2Transform.getCPtr(transform), childIndex);
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void ComputeAABB(b2AABB aabb, b2Transform xf, int childIndex) {
    Box2dPINVOKE.b2Shape_ComputeAABB(swigCPtr, b2AABB.getCPtr(aabb), b2Transform.getCPtr(xf), childIndex);
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void ComputeMass(b2MassData massData, float density) {
    Box2dPINVOKE.b2Shape_ComputeMass(swigCPtr, b2MassData.getCPtr(massData), density);
  }

  public b2Shape.Type m_type {
    set {
      Box2dPINVOKE.b2Shape_m_type_set(swigCPtr, (int)value);
    } 
    get {
      b2Shape.Type ret = (b2Shape.Type)Box2dPINVOKE.b2Shape_m_type_get(swigCPtr);
      return ret;
    } 
  }

  public float m_radius {
    set {
      Box2dPINVOKE.b2Shape_m_radius_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2Shape_m_radius_get(swigCPtr);
      return ret;
    } 
  }

  public enum Type {
    e_circle = 0,
    e_edge = 1,
    e_polygon = 2,
    e_chain = 3,
    e_typeCount = 4
  }

}

}
