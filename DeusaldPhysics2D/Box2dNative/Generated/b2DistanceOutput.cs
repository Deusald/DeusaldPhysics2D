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

internal class b2DistanceOutput : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2DistanceOutput(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2DistanceOutput obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2DistanceOutput() {
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
          Box2dPINVOKE.delete_b2DistanceOutput(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public b2Vec2 pointA {
    set {
      Box2dPINVOKE.b2DistanceOutput_pointA_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2DistanceOutput_pointA_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public b2Vec2 pointB {
    set {
      Box2dPINVOKE.b2DistanceOutput_pointB_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2DistanceOutput_pointB_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public float distance {
    set {
      Box2dPINVOKE.b2DistanceOutput_distance_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2DistanceOutput_distance_get(swigCPtr);
      return ret;
    } 
  }

  public int iterations {
    set {
      Box2dPINVOKE.b2DistanceOutput_iterations_set(swigCPtr, value);
    } 
    get {
      int ret = Box2dPINVOKE.b2DistanceOutput_iterations_get(swigCPtr);
      return ret;
    } 
  }

  public b2DistanceOutput() : this(Box2dPINVOKE.new_b2DistanceOutput(), true) {
  }

}

}
