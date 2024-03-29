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

internal class b2BodyDef : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2BodyDef(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2BodyDef obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2BodyDef() {
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
          Box2dPINVOKE.delete_b2BodyDef(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public b2BodyDef() : this(Box2dPINVOKE.new_b2BodyDef(), true) {
  }

  public b2BodyType type {
    set {
      Box2dPINVOKE.b2BodyDef_type_set(swigCPtr, (int)value);
    } 
    get {
      b2BodyType ret = (b2BodyType)Box2dPINVOKE.b2BodyDef_type_get(swigCPtr);
      return ret;
    } 
  }

  public b2Vec2 position {
    set {
      Box2dPINVOKE.b2BodyDef_position_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2BodyDef_position_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public float angle {
    set {
      Box2dPINVOKE.b2BodyDef_angle_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2BodyDef_angle_get(swigCPtr);
      return ret;
    } 
  }

  public b2Vec2 linearVelocity {
    set {
      Box2dPINVOKE.b2BodyDef_linearVelocity_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2BodyDef_linearVelocity_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public float angularVelocity {
    set {
      Box2dPINVOKE.b2BodyDef_angularVelocity_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2BodyDef_angularVelocity_get(swigCPtr);
      return ret;
    } 
  }

  public float linearDamping {
    set {
      Box2dPINVOKE.b2BodyDef_linearDamping_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2BodyDef_linearDamping_get(swigCPtr);
      return ret;
    } 
  }

  public float angularDamping {
    set {
      Box2dPINVOKE.b2BodyDef_angularDamping_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2BodyDef_angularDamping_get(swigCPtr);
      return ret;
    } 
  }

  public bool allowSleep {
    set {
      Box2dPINVOKE.b2BodyDef_allowSleep_set(swigCPtr, value);
    } 
    get {
      bool ret = Box2dPINVOKE.b2BodyDef_allowSleep_get(swigCPtr);
      return ret;
    } 
  }

  public bool awake {
    set {
      Box2dPINVOKE.b2BodyDef_awake_set(swigCPtr, value);
    } 
    get {
      bool ret = Box2dPINVOKE.b2BodyDef_awake_get(swigCPtr);
      return ret;
    } 
  }

  public bool fixedRotation {
    set {
      Box2dPINVOKE.b2BodyDef_fixedRotation_set(swigCPtr, value);
    } 
    get {
      bool ret = Box2dPINVOKE.b2BodyDef_fixedRotation_get(swigCPtr);
      return ret;
    } 
  }

  public bool bullet {
    set {
      Box2dPINVOKE.b2BodyDef_bullet_set(swigCPtr, value);
    } 
    get {
      bool ret = Box2dPINVOKE.b2BodyDef_bullet_get(swigCPtr);
      return ret;
    } 
  }

  public bool enabled {
    set {
      Box2dPINVOKE.b2BodyDef_enabled_set(swigCPtr, value);
    } 
    get {
      bool ret = Box2dPINVOKE.b2BodyDef_enabled_get(swigCPtr);
      return ret;
    } 
  }

  public b2BodyUserData userData {
    set {
      Box2dPINVOKE.b2BodyDef_userData_set(swigCPtr, b2BodyUserData.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2BodyDef_userData_get(swigCPtr);
      b2BodyUserData ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2BodyUserData(cPtr, false);
      return ret;
    } 
  }

  public float gravityScale {
    set {
      Box2dPINVOKE.b2BodyDef_gravityScale_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2BodyDef_gravityScale_get(swigCPtr);
      return ret;
    } 
  }

}

}
