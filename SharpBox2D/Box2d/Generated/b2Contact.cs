//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Box2D {

internal class b2Contact : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2Contact(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2Contact obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2Contact() {
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
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public b2Manifold GetManifold() {
    global::System.IntPtr cPtr = Box2dPINVOKE.b2Contact_GetManifold__SWIG_0(swigCPtr);
    b2Manifold ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Manifold(cPtr, false);
    return ret;
  }

  public void GetWorldManifold(b2WorldManifold worldManifold) {
    Box2dPINVOKE.b2Contact_GetWorldManifold(swigCPtr, b2WorldManifold.getCPtr(worldManifold));
  }

  public bool IsTouching() {
    bool ret = Box2dPINVOKE.b2Contact_IsTouching(swigCPtr);
    return ret;
  }

  public void SetEnabled(bool flag) {
    Box2dPINVOKE.b2Contact_SetEnabled(swigCPtr, flag);
  }

  public bool IsEnabled() {
    bool ret = Box2dPINVOKE.b2Contact_IsEnabled(swigCPtr);
    return ret;
  }

  public b2Fixture GetFixtureA() {
    global::System.IntPtr cPtr = Box2dPINVOKE.b2Contact_GetFixtureA__SWIG_0(swigCPtr);
    b2Fixture ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Fixture(cPtr, false);
    return ret;
  }

  public int GetChildIndexA() {
    int ret = Box2dPINVOKE.b2Contact_GetChildIndexA(swigCPtr);
    return ret;
  }

  public b2Fixture GetFixtureB() {
    global::System.IntPtr cPtr = Box2dPINVOKE.b2Contact_GetFixtureB__SWIG_0(swigCPtr);
    b2Fixture ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Fixture(cPtr, false);
    return ret;
  }

  public int GetChildIndexB() {
    int ret = Box2dPINVOKE.b2Contact_GetChildIndexB(swigCPtr);
    return ret;
  }

  public void SetFriction(float friction) {
    Box2dPINVOKE.b2Contact_SetFriction(swigCPtr, friction);
  }

  public float GetFriction() {
    float ret = Box2dPINVOKE.b2Contact_GetFriction(swigCPtr);
    return ret;
  }

  public void ResetFriction() {
    Box2dPINVOKE.b2Contact_ResetFriction(swigCPtr);
  }

  public void SetRestitution(float restitution) {
    Box2dPINVOKE.b2Contact_SetRestitution(swigCPtr, restitution);
  }

  public float GetRestitution() {
    float ret = Box2dPINVOKE.b2Contact_GetRestitution(swigCPtr);
    return ret;
  }

  public void ResetRestitution() {
    Box2dPINVOKE.b2Contact_ResetRestitution(swigCPtr);
  }

  public void SetTangentSpeed(float speed) {
    Box2dPINVOKE.b2Contact_SetTangentSpeed(swigCPtr, speed);
  }

  public float GetTangentSpeed() {
    float ret = Box2dPINVOKE.b2Contact_GetTangentSpeed(swigCPtr);
    return ret;
  }

}

}