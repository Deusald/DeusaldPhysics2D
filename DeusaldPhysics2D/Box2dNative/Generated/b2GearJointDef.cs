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

internal class b2GearJointDef : b2JointDef {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal b2GearJointDef(global::System.IntPtr cPtr, bool cMemoryOwn) : base(Box2dPINVOKE.b2GearJointDef_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2GearJointDef obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          Box2dPINVOKE.delete_b2GearJointDef(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public b2GearJointDef() : this(Box2dPINVOKE.new_b2GearJointDef(), true) {
  }

  public b2Joint joint1 {
    set {
      Box2dPINVOKE.b2GearJointDef_joint1_set(swigCPtr, b2Joint.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2GearJointDef_joint1_get(swigCPtr);
      b2Joint ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Joint(cPtr, false);
      return ret;
    } 
  }

  public b2Joint joint2 {
    set {
      Box2dPINVOKE.b2GearJointDef_joint2_set(swigCPtr, b2Joint.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2GearJointDef_joint2_get(swigCPtr);
      b2Joint ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2Joint(cPtr, false);
      return ret;
    } 
  }

  public float ratio {
    set {
      Box2dPINVOKE.b2GearJointDef_ratio_set(swigCPtr, value);
    } 
    get {
      float ret = Box2dPINVOKE.b2GearJointDef_ratio_get(swigCPtr);
      return ret;
    } 
  }

}

}
