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

internal class b2ContactListener : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal b2ContactListener(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(b2ContactListener obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~b2ContactListener() {
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
          Box2dPINVOKE.delete_b2ContactListener(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public virtual void BeginContact(b2Contact contact) {
    if (SwigDerivedClassHasMethod("BeginContact", swigMethodTypes0)) Box2dPINVOKE.b2ContactListener_BeginContactSwigExplicitb2ContactListener(swigCPtr, b2Contact.getCPtr(contact)); else Box2dPINVOKE.b2ContactListener_BeginContact(swigCPtr, b2Contact.getCPtr(contact));
  }

  public virtual void EndContact(b2Contact contact) {
    if (SwigDerivedClassHasMethod("EndContact", swigMethodTypes1)) Box2dPINVOKE.b2ContactListener_EndContactSwigExplicitb2ContactListener(swigCPtr, b2Contact.getCPtr(contact)); else Box2dPINVOKE.b2ContactListener_EndContact(swigCPtr, b2Contact.getCPtr(contact));
  }

  public virtual void PreSolve(b2Contact contact, b2Manifold oldManifold) {
    if (SwigDerivedClassHasMethod("PreSolve", swigMethodTypes2)) Box2dPINVOKE.b2ContactListener_PreSolveSwigExplicitb2ContactListener(swigCPtr, b2Contact.getCPtr(contact), b2Manifold.getCPtr(oldManifold)); else Box2dPINVOKE.b2ContactListener_PreSolve(swigCPtr, b2Contact.getCPtr(contact), b2Manifold.getCPtr(oldManifold));
  }

  public virtual void PostSolve(b2Contact contact, b2ContactImpulse impulse) {
    if (SwigDerivedClassHasMethod("PostSolve", swigMethodTypes3)) Box2dPINVOKE.b2ContactListener_PostSolveSwigExplicitb2ContactListener(swigCPtr, b2Contact.getCPtr(contact), b2ContactImpulse.getCPtr(impulse)); else Box2dPINVOKE.b2ContactListener_PostSolve(swigCPtr, b2Contact.getCPtr(contact), b2ContactImpulse.getCPtr(impulse));
  }

  public b2ContactListener() : this(Box2dPINVOKE.new_b2ContactListener(), true) {
    SwigDirectorConnect();
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("BeginContact", swigMethodTypes0))
      swigDelegate0 = new SwigDelegateb2ContactListener_0(SwigDirectorMethodBeginContact);
    if (SwigDerivedClassHasMethod("EndContact", swigMethodTypes1))
      swigDelegate1 = new SwigDelegateb2ContactListener_1(SwigDirectorMethodEndContact);
    if (SwigDerivedClassHasMethod("PreSolve", swigMethodTypes2))
      swigDelegate2 = new SwigDelegateb2ContactListener_2(SwigDirectorMethodPreSolve);
    if (SwigDerivedClassHasMethod("PostSolve", swigMethodTypes3))
      swigDelegate3 = new SwigDelegateb2ContactListener_3(SwigDirectorMethodPostSolve);
    Box2dPINVOKE.b2ContactListener_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2, swigDelegate3);
  }

  private bool SwigDerivedClassHasMethod(string methodName, global::System.Type[] methodTypes) {
    global::System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(methodName, global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Instance, null, methodTypes, null);
    bool hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(b2ContactListener));
    return hasDerivedMethod;
  }

  private void SwigDirectorMethodBeginContact(global::System.IntPtr contact) {
    BeginContact((contact == global::System.IntPtr.Zero) ? null : new b2Contact(contact, false));
  }

  private void SwigDirectorMethodEndContact(global::System.IntPtr contact) {
    EndContact((contact == global::System.IntPtr.Zero) ? null : new b2Contact(contact, false));
  }

  private void SwigDirectorMethodPreSolve(global::System.IntPtr contact, global::System.IntPtr oldManifold) {
    PreSolve((contact == global::System.IntPtr.Zero) ? null : new b2Contact(contact, false), (oldManifold == global::System.IntPtr.Zero) ? null : new b2Manifold(oldManifold, false));
  }

  private void SwigDirectorMethodPostSolve(global::System.IntPtr contact, global::System.IntPtr impulse) {
    PostSolve((contact == global::System.IntPtr.Zero) ? null : new b2Contact(contact, false), (impulse == global::System.IntPtr.Zero) ? null : new b2ContactImpulse(impulse, false));
  }

  public delegate void SwigDelegateb2ContactListener_0(global::System.IntPtr contact);
  public delegate void SwigDelegateb2ContactListener_1(global::System.IntPtr contact);
  public delegate void SwigDelegateb2ContactListener_2(global::System.IntPtr contact, global::System.IntPtr oldManifold);
  public delegate void SwigDelegateb2ContactListener_3(global::System.IntPtr contact, global::System.IntPtr impulse);

  private SwigDelegateb2ContactListener_0 swigDelegate0;
  private SwigDelegateb2ContactListener_1 swigDelegate1;
  private SwigDelegateb2ContactListener_2 swigDelegate2;
  private SwigDelegateb2ContactListener_3 swigDelegate3;

  private static global::System.Type[] swigMethodTypes0 = new global::System.Type[] { typeof(b2Contact) };
  private static global::System.Type[] swigMethodTypes1 = new global::System.Type[] { typeof(b2Contact) };
  private static global::System.Type[] swigMethodTypes2 = new global::System.Type[] { typeof(b2Contact), typeof(b2Manifold) };
  private static global::System.Type[] swigMethodTypes3 = new global::System.Type[] { typeof(b2Contact), typeof(b2ContactImpulse) };
}

}
