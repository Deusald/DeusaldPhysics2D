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

internal class b2MotorJoint : b2Joint {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal b2MotorJoint(global::System.IntPtr cPtr, bool cMemoryOwn) : base(Box2dPINVOKE.b2MotorJoint_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2MotorJoint obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          Box2dPINVOKE.delete_b2MotorJoint(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public override b2Vec2 GetAnchorA() {
    b2Vec2 ret = new b2Vec2(Box2dPINVOKE.b2MotorJoint_GetAnchorA(swigCPtr), true);
    return ret;
  }

  public override b2Vec2 GetAnchorB() {
    b2Vec2 ret = new b2Vec2(Box2dPINVOKE.b2MotorJoint_GetAnchorB(swigCPtr), true);
    return ret;
  }

  public override b2Vec2 GetReactionForce(float inv_dt) {
    b2Vec2 ret = new b2Vec2(Box2dPINVOKE.b2MotorJoint_GetReactionForce(swigCPtr, inv_dt), true);
    return ret;
  }

  public override float GetReactionTorque(float inv_dt) {
    float ret = Box2dPINVOKE.b2MotorJoint_GetReactionTorque(swigCPtr, inv_dt);
    return ret;
  }

  public void SetLinearOffset(b2Vec2 linearOffset) {
    Box2dPINVOKE.b2MotorJoint_SetLinearOffset(swigCPtr, b2Vec2.getCPtr(linearOffset));
    if (Box2dPINVOKE.SWIGPendingException.Pending) throw Box2dPINVOKE.SWIGPendingException.Retrieve();
  }

  public b2Vec2 GetLinearOffset() {
    b2Vec2 ret = new b2Vec2(Box2dPINVOKE.b2MotorJoint_GetLinearOffset(swigCPtr), false);
    return ret;
  }

  public void SetAngularOffset(float angularOffset) {
    Box2dPINVOKE.b2MotorJoint_SetAngularOffset(swigCPtr, angularOffset);
  }

  public float GetAngularOffset() {
    float ret = Box2dPINVOKE.b2MotorJoint_GetAngularOffset(swigCPtr);
    return ret;
  }

  public void SetMaxForce(float force) {
    Box2dPINVOKE.b2MotorJoint_SetMaxForce(swigCPtr, force);
  }

  public float GetMaxForce() {
    float ret = Box2dPINVOKE.b2MotorJoint_GetMaxForce(swigCPtr);
    return ret;
  }

  public void SetMaxTorque(float torque) {
    Box2dPINVOKE.b2MotorJoint_SetMaxTorque(swigCPtr, torque);
  }

  public float GetMaxTorque() {
    float ret = Box2dPINVOKE.b2MotorJoint_GetMaxTorque(swigCPtr);
    return ret;
  }

  public void SetCorrectionFactor(float factor) {
    Box2dPINVOKE.b2MotorJoint_SetCorrectionFactor(swigCPtr, factor);
  }

  public float GetCorrectionFactor() {
    float ret = Box2dPINVOKE.b2MotorJoint_GetCorrectionFactor(swigCPtr);
    return ret;
  }

}

}
