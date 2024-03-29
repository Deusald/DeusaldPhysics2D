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

internal class b2Manifold : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2Manifold(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2Manifold obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2Manifold() {
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
          Box2dPINVOKE.delete_b2Manifold(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public b2ManifoldPoint points {
    set {
      Box2dPINVOKE.b2Manifold_points_set(swigCPtr, b2ManifoldPoint.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2Manifold_points_get(swigCPtr);
      b2ManifoldPoint ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2ManifoldPoint(cPtr, false);
      return ret;
    } 
  }

  public b2Vec2 localNormal {
    set {
      Box2dPINVOKE.b2Manifold_localNormal_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2Manifold_localNormal_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public b2Vec2 localPoint {
    set {
      Box2dPINVOKE.b2Manifold_localPoint_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2Manifold_localPoint_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public b2Manifold.Type type {
    set {
      Box2dPINVOKE.b2Manifold_type_set(swigCPtr, (int)value);
    } 
    get {
      b2Manifold.Type ret = (b2Manifold.Type)Box2dPINVOKE.b2Manifold_type_get(swigCPtr);
      return ret;
    } 
  }

  public int pointCount {
    set {
      Box2dPINVOKE.b2Manifold_pointCount_set(swigCPtr, value);
    } 
    get {
      int ret = Box2dPINVOKE.b2Manifold_pointCount_get(swigCPtr);
      return ret;
    } 
  }

  public b2Manifold() : this(Box2dPINVOKE.new_b2Manifold(), true) {
  }

  public enum Type {
    e_circles,
    e_faceA,
    e_faceB
  }

}

}
