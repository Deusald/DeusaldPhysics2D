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

internal class b2Vec3 : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2Vec3(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2Vec3 obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2Vec3() {
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
          Box2dPINVOKE.delete_b2Vec3(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public b2Vec3() : this(Box2dPINVOKE.new_b2Vec3__SWIG_0(), true) {
  }

  public b2Vec3(float xIn, float yIn, float zIn) : this(Box2dPINVOKE.new_b2Vec3__SWIG_1(xIn, yIn, zIn), true) {
  }

  public void SetZero() {
    Box2dPINVOKE.b2Vec3_SetZero(swigCPtr);
  }

  public void Set(float x_, float y_, float z_) {
    Box2dPINVOKE.b2Vec3_Set(swigCPtr, x_, y_, z_);
  }

  public b2Vec3 Minus() {
    b2Vec3 ret = new b2Vec3(Box2dPINVOKE.b2Vec3_Minus(swigCPtr), true);
    return ret;
  }

  public void PlusEqual(b2Vec3 v) {
    Box2dPINVOKE.b2Vec3_PlusEqual(swigCPtr, b2Vec3.getCPtr(v));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public void MinusEqual(b2Vec3 v) {
    Box2dPINVOKE.b2Vec3_MinusEqual(swigCPtr, b2Vec3.getCPtr(v));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public void MultiplyEqual(float s) {
    Box2dPINVOKE.b2Vec3_MultiplyEqual(swigCPtr, s);
  }

  public float x {
    set {
      Box2dPINVOKE.b2Vec3_x_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2Vec3_x_get(swigCPtr);
      return ret;
    } 
  }

  public float y {
    set {
      Box2dPINVOKE.b2Vec3_y_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2Vec3_y_get(swigCPtr);
      return ret;
    } 
  }

  public float z {
    set {
      Box2dPINVOKE.b2Vec3_z_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2Vec3_z_get(swigCPtr);
      return ret;
    } 
  }

}

}
