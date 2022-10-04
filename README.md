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
  Description: This script is responsible for making the Truck move forward, backward, right, and left.  

* Assets -> Truck_Low_Poly -> Scripts -> CameraFollow.cs
</br>
  Description: This script is responsible to move the camera so that it follows the truck from the back.  

* Assets -> Scripts -> Truck_Scene -> Backward_Long_Press.cs
  </br>
  Description: This script is responsible for listening to the onLongPress and onPressRelease events from the Backward button on the Truck Driving Scene. The Backward button is intended to move the truck back. 

* Assets -> Scripts -> Truck_Scene -> Forward_Long_Press.cs
  </br>  
  Description: This script is responsible for listening to the onLongPress and onPressRelease events from the Forward button on the Truck Driving Scene. The Forward button is intended to move the truck forward.

* Assets -> Scripts -> Truck_Scene -> Left_Long_Press.cs
  </br>  
  Description: This script is responsible for listening to the onLongPress and onPressRelease events from the Left button on the Truck Driving Scene. The Left button is intended to turn the truck left.

* Assets -> Scripts -> Truck_Scene -> Right_Long_Press.cs
  </br>
  Description: This script is responsible for listening to the onLongPress and onPressRelease events from the Right button on the Truck Driving Scene. The Right button is intended to turn the truck right.

* Assets -> Scripts -> Callback_System -> CallbackEventSystem.cs
  </br>  
  Description: This script is responsible for managing callback functions for the OnLongPress and OnPressRelease events of the buttons: Forward Button, Backward Button, Right Button, and Left Button of the truck driving scene. 

* Assets -> Scripts -> Callback_System -> EventInfo.cs
  </br>  
  Description: This script is responsible for defining the OnLongPress and OnPressRelease events for each of the following buttons: Forward, Backward, Right, Left of the truck driving scene.   



