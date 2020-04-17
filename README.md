# SharpBox2D
Box2d C# port using native C++ library
This port is more like an interface to interact with box2d
Interface is build to be eazy in use
Interface give you also some additional functionality:
- CoRoutines - class for creating methods that can suspend its execution (yield) until the given YieldInstruction finishes.
- Vector2/Vector3 - vector structs with handy methods
- Nav2D - 2d pathfinding

Attention! Before making any call to dll first you must invoke Box2dNativeLoader.LoadNativeLibrary method!