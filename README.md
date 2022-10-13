# SDGFall22
Fall 22 DOT SDG Internship

To set up the environment:

Make sure that you have the following Unity version installed and UNINSTALL any existing Unity version that you have installed:

Unity Version: [2019.1.1 (2019.1.1f1)](https://unity3d.com/get-unity/download/archive)

Next you need to install a text editor to view and write scripts. The recommended text editor is [Visual Studio Code](https://code.visualstudio.com/). For information on how to change the text editor for scripts in Unity, see [here](https://www.youtube.com/watch?v=0hgvbLOjlD8&t=140s)


After setting up the environment, you can git clone or download this repository and import this project to Unity. To import a project into Unity:
* Open Unity Hub
* Select Projects
* Select Add
* Locate directory to where this repository was downloaded
* Select Directory
* Select OK

Now you can select the project in Unity Hub and open it

Below are descriptions of major parts of the Source code:

#### For the Truck Driving Scene

* Assets -> Truck_Low_Poly -> Scripts -> TruckMovementControl.cs </br>
  Description: This script is responsible for making the Truck move forward, backward, right, and left. It stores 4 WheelColliders, 1 for each wheel of the truck. It stores 4 tyre meshes, 1 for each wheel of the truck.</br>

  The WheelColliders are rotated when OnRightEvent or OnLeftEvent are invoked. The UpdateMeshesPosition function when invoked takes the WheelCollider rotation into account and rotates the tyremeshes.</br>

  The script registers the following events: OnForwardPressEvent, OnBackwardPressEvent,OnBackwardReleaseEvent, OnForwardReleaseEvent, OnRightPressEvent, OnRightReleaseEvent, OnLeftPressEvent,OnLeftReleaseEvent, through the use of CallbackEventSystem manager.</br>

  Below are descriptions of the functions that are part of this file:</br>

  * FixedUpdate() - Responsible for setting the two front wheel colliders' steering angles of the truck. Also, sets the motor torque of all the wheel colliders of the truck. Also, keeps track of forward, backward, right, left events.

  * UpdateMeshesPositions() - Sets the position and rotation of each tyremesh with respect to the current position and rotation of each WheelCollider.

  * OnRight() - Increments steer variable by 0.05 and esures that the value of the steer is approximately no greater than 1. This steer value is used as a factor to calculate the angle of the front WheelColliders.

  * OnLeft() - Decrements steer variable by 0.05 and esures that the value of the steer is approximately no less than -1. This steer value is used as a factor to calculate the angle of the front WheelColliders.

  * OnSteerReset() - Resets the steer value to 0.

  * OnForward() - Checks if car is moving backward. If so, it sets the accelaration to 0 and applies brakes on each WheelCollider. Otherwise, if brakes were previously applied, it sets isBreaking to false and calls resetBraking(). It also checks to see if accelaration is less than 1, if so, it increments accelaration by 0.089. The accelaration value is used as a factor to calculate the motorTorque of all the WheelColliders.

  * OnBackward() - Checks if car is moving forward. If so, it sets the accelaration to 0 and applies brakes on each WheelCollider. Otherwise, if brakes were previously applied, it sets isBreaking to false and calls resetBraking(). It also checks to see if accelaration is greater than than -1, if so, it decrements accelaration by 0.089. The accelaration value is used as a factor to calculate the motorTorque of all the WheelColliders.

  * ApplyBraking() - sets isBreaking to true. It applies brakeTorque of maxBrakeForce to each WheelCollider.

  * ResetBraking() - sets isBraking to false. It applies brakeTorque of 0 to each WheelCollider.  

  * OnForwardEvent() - invoked when OnForwardPressEvent is fired. Sets goForward to true.

  * OnBackwardEvent() - invoked when OnBackwardPressEvent is fired. Sets goBackward to true.

  * OnForwardREvent() - invoked when OnForwardReleaseEvent is fired. Sets goForward to false.

  * OnBackwardREvent() - invoked when OnBackwardReleaseEvent is fired. Sets goBackward to false.

  * OnRightEvent() - invoked when OnRightPressEvent is fired. Sets goRight to true.

  * OnRightREvent() - invoked when OnRightReleaseEvent is fired. Sets goRight to false.

  * OnLeftEvent() - invoked when OnLeftPressEvent is fired. Sets goLeft to true.

  * OnLeftREvent() - invoked when OnLeftReleaseEvent is fired. Sets goLeft to false. 

  * Sensors() - takes care of all the truck sensors.

* Assets -> Truck_Low_Poly -> Scripts -> CameraFollow.cs</br>
  
  Description: This script is responsible to move the camera so that it follows the truck from the back.</br> 

  Below are descriptions of the functions that are part of this file:</br> 

  * FixedUpdate() - invoked "every fixed frame-rate frame." For the script that has been written, in FixedUpdate(),we position the camera to the amount of posSmoothing value between transform.position and player.position. We also, rotate the camera to the amount of rotSmoothing value angle between transform.rotation and player.rotation. Finally, we set x, z angles to 0 and only leave y angle unchanged.</br> 
  FixedUpdate is part of Unity's MonoBehavior class and is called in a specified order. To learn more about this function, see the following links: </br>
     * [https://docs.unity3d.com/Manual/ExecutionOrder.html](https://docs.unity3d.com/Manual/ExecutionOrder.html)</br>
     * [https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html](https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html)</br>
* Assets -> Scripts -> Manuver -> Backward_Long_Press.cs
  </br>
  Description: This script is responsible for listening to the onLongPress and onPressRelease events from the Backward button on the Truck Driving Scene. The Backward button is intended to move the truck back.</br>

  Below are descriptions of the functions that are part of this file:</br>

  * OnPointerDown() - invoked when backward button is pressed. Fires a onBackwardPressEvent to the callback system.

  * OnPointerUp() - invoked when backward button press is released. Fires a onBackwardReleaseEvent to the callback system. 

* Assets -> Scripts -> Manuver -> Forward_Long_Press.cs
  </br>  
  Description: This script is responsible for listening to the onLongPress and onPressRelease events from the Forward button on the Truck Driving Scene. The Forward button is intended to move the truck forward. </br>

  Below are descriptions of the functions that are part of this file:</br>

  * OnPointerDown() - invoked when forward button is pressed. Fires a onForwardPressEvent to the callback system.

  * OnPointerUp() - invoked when forward button press is released. Fires a onForwardReleaseEvent to the callback system. 

* Assets -> Scripts -> Manuver -> Left_Long_Press.cs
  </br>  
  Description: This script is responsible for listening to the onLongPress and onPressRelease events from the Left button on the Truck Driving Scene. The Left button is intended to turn the truck left. </br>

  Below are descriptions of the functions that are part of this file:</br>

  * OnPointerDown() - invoked when left button is pressed. Fires a onLeftPressEvent to the callback system.

  * OnPointerUp() - invoked when left button press is released. Fires a onLeftReleaseEvent to the callback system. 

* Assets -> Scripts -> Manuver -> Right_Long_Press.cs
  </br>
  Description: This script is responsible for listening to the onLongPress and onPressRelease events from the Right button on the Truck Driving Scene. The Right button is intended to turn the truck right.</br>

  Below are descriptions of the functions that are part of this file:</br>

  * OnPointerDown() - invoked when right button is pressed. Fires a onRightPressEvent to the callback system.

  * OnPointerUp() - invoked when right button press is released. Fires a onRightReleaseEvent to the callback system. 

* Assets -> Scripts -> Callback_System -> CallbackEventSystem.cs
  </br>  
  Description: This script is responsible for managing callback functions for the OnLongPress and OnPressRelease events of the buttons: Forward Button, Backward Button, Right Button, and Left Button of the truck driving scene.</br>

  Below are descriptions of the functions that are part of this file:</br>

  * OnEnable() - sets __Current object to the current instance of the CallbackEventSystem class.

  * Current - returns the __Current object. If __Current object is null, it sets __Current to the GameObject with the CallbackEventSystem script.

  * EventListener(EventInfo ei) - is a delegate function, which means that it is able to hold on to another function as a reference. It is useful when storing listeners using RegisterListener(...) function.

  * RegisterListener<T>(System.Action<T> listener) - stores an event listener so that it can be called when an event of that type is fired.

  * UnregisterListener<T>(System.Action<T> listener) - removes an event listener so that it is no longer called.

  * FireEvent(EventInfo eventInfo) - fires an event by checking to see all eventListeners for a particular EventInfo type and calling them all. If no eventlistener of a particular EventInfo type exists, then it does not call any event listener. 
  

* Assets -> Scripts -> Callback_System -> EventInfo.cs
  </br>  
  Description: This script is responsible for defining each type of OnLongPress and OnPressRelease events for each of the following buttons: Forward, Backward, Right, Left of the truck driving scene.   



