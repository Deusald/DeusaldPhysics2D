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

internal class b2ContactManager : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2ContactManager(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2ContactManager obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2ContactManager() {
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
          Box2dPINVOKE.delete_b2ContactManager(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public b2ContactManager() : this(Box2dPINVOKE.new_b2ContactManager(), true) {
  }

  public b2ContactFilter m_contactFilter {
    set {
      Box2dPINVOKE.b2ContactManager_m_contactFilter_set(swigCPtr, b2ContactFilter.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2ContactManager_m_contactFilter_get(swigCPtr);
      b2ContactFilter ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2ContactFilter(cPtr, false);
      return ret;
    } 
  }

  public b2ContactListener m_contactListener {
    set {
      Box2dPINVOKE.b2ContactManager_m_contactListener_set(swigCPtr, b2ContactListener.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = Box2dPINVOKE.b2ContactManager_m_contactListener_get(swigCPtr);
      b2ContactListener ret = (cPtr == global::System.IntPtr.Zero) ? null : new b2ContactListener(cPtr, false);
      return ret;
    } 
  }

}

}
