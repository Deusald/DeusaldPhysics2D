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

internal class b2MotorJointDef : b2JointDef {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal b2MotorJointDef(global::System.IntPtr cPtr, bool cMemoryOwn) : base(Box2dPINVOKE.b2MotorJointDef_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2MotorJointDef obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          Box2dPINVOKE.delete_b2MotorJointDef(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public b2MotorJointDef() : this(Box2dPINVOKE.new_b2MotorJointDef(), true) {
  }

  public void Initialize(b2Body bodyA, b2Body bodyB) {
    Box2dPINVOKE.b2MotorJointDef_Initialize(swigCPtr, b2Body.getCPtr(bodyA), b2Body.getCPtr(bodyB));
  }

  public b2Vec2 linearOffset {
    set {
      Box2dPINVOKE.b2MotorJointDef_linearOffset_set(swigCPtr, b2Vec2.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2MotorJointDef_linearOffset_get(swigCPtr);
      b2Vec2 ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Vec2(cPtr, false);
      return ret;
    } 
  }

  public float angularOffset {
    set {
      Box2dPINVOKE.b2MotorJointDef_angularOffset_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2MotorJointDef_angularOffset_get(swigCPtr);
      return ret;
    } 
  }

  public float maxForce {
    set {
      Box2dPINVOKE.b2MotorJointDef_maxForce_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2MotorJointDef_maxForce_get(swigCPtr);
      return ret;
    } 
  }

  public float maxTorque {
    set {
      Box2dPINVOKE.b2MotorJointDef_maxTorque_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2MotorJointDef_maxTorque_get(swigCPtr);
      return ret;
    } 
  }

  public float correctionFactor {
    set {
      Box2dPINVOKE.b2MotorJointDef_correctionFactor_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2MotorJointDef_correctionFactor_get(swigCPtr);
      return ret;
    } 
  }

}

}
