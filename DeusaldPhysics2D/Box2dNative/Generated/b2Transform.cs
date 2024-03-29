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

internal class b2Transform : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2Transform(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2Transform obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2Transform() {
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
          Box2dPINVOKE.delete_b2Transform(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public b2Transform() : this(Box2dPINVOKE.new_b2Transform__SWIG_0(), true) {
  }

  public b2Transform(b2Vec2 position, b2Rot rotation) : this(Box2dPINVOKE.new_b2Transform__SWIG_1(b2Vec2.getCPtr(position), b2Rot.getCPtr(rotation)), true) {
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetIdentity() {
    Box2dPINVOKE.b2Transform_SetIdentity(swigCPtr);
  }

  public void Set(b2Vec2 position, float angle) {
    Box2dPINVOKE.b2Transform_Set(swigCPtr, b2Vec2.getCPtr(position), angle);
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public b2Vec2 p {
    set {
      Box2dPINVOKE.b2Transform_p_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2Transform_p_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public b2Rot q {
    set {
      Box2dPINVOKE.b2Transform_q_set(swigCPtr, b2Rot.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2Transform_q_get(swigCPtr);
      b2Rot ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Rot(cPtr, false);
      return ret;
    } 
  }

}

}
