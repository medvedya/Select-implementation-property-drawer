# Select-implementation-property-drawer
Unity property drawer to choose implementation in an inspector for marked field as SerializeReference.
## Example
### Code
```c#
using UnityEngine;
using System;

public interface IMyInterface { }

[Serializable]
public class MyImplementInterfaceClass : IMyInterface { public int myImplementInterfaceClass; }

public abstract class MyAbstractClass { public string myAbstractClass; }

[Serializable]
public class MyInheritedFromAbstractClass : MyAbstractClass { public float myInheritedFromAbstractClass; }

[Serializable]
public class MyInheritedFromAbstractClassAndInterface : MyAbstractClass, IMyInterface { public bool myInheritedFromAbstractClassAndInterface; }

public class SelectIimplementationDrawerExample : MonoBehaviour
{
    [SerializeReference, SelectImplementation(typeof(IMyInterface))]
    public IMyInterface interfaceField;
    
    [SerializeReference, SelectImplementation(typeof(MyAbstractClass))]
    public MyAbstractClass abstractClassField;
}
```
### And in the inspector it will be like that
![](https://github.com/medvedya/Select-implementation-property-drawer/blob/description_files/SelectIimplementationDrawer_example.gif?raw=true)

## Installation
You can install this package via Package manager by link to the current repo https://github.com/medvedya/Select-implementation-property-drawer.git 
