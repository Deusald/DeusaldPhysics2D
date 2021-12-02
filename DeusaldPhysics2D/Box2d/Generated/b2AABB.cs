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

internal class b2AABB : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2AABB(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2AABB obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2AABB() {
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
          Box2dPINVOKE.delete_b2AABB(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public bool IsValid() {
    bool ret = Box2dPINVOKE.b2AABB_IsValid(swigCPtr);
    return ret;
  }

  public b2Vec2 GetCenter() {
    b2Vec2 ret = new b2Vec2(Box2dPINVOKE.b2AABB_GetCenter(swigCPtr), true);
    return ret;
  }

  public b2Vec2 GetExtents() {
    b2Vec2 ret = new b2Vec2(Box2dPINVOKE.b2AABB_GetExtents(swigCPtr), true);
    return ret;
  }

  public float GetPerimeter() {
    float ret = Box2dPINVOKE.b2AABB_GetPerimeter(swigCPtr);
    return ret;
  }

  public void Combine(b2AABB aabb) {
    Box2dPINVOKE.b2AABB_Combine__SWIG_0(swigCPtr, b2AABB.getCPtr(aabb));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public void Combine(b2AABB aabb1, b2AABB aabb2) {
    Box2dPINVOKE.b2AABB_Combine__SWIG_1(swigCPtr, b2AABB.getCPtr(aabb1), b2AABB.getCPtr(aabb2));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public bool Contains(b2AABB aabb) {
    bool ret = Box2dPINVOKE.b2AABB_Contains(swigCPtr, b2AABB.getCPtr(aabb));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool RayCast(b2RayCastOutput output, b2RayCastInput input) {
    bool ret = Box2dPINVOKE.b2AABB_RayCast(swigCPtr, b2RayCastOutput.getCPtr(output), b2RayCastInput.getCPtr(input));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public b2Vec2 lowerBound {
    set {
      Box2dPINVOKE.b2AABB_lowerBound_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2AABB_lowerBound_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public b2Vec2 upperBound {
    set {
      Box2dPINVOKE.b2AABB_upperBound_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2AABB_upperBound_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public b2AABB() : this(Box2dPINVOKE.new_b2AABB(), true) {
  }

}

}