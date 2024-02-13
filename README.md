A Screen Stack system made for Unity. 
A Screen Stack is a system responsible for handling the player's inputs focus in the game, making it possible to activate more than one screen at once without losing the focus of the inputs. In my system, the Screen Stack also helps handling the state of the game, since, when the stack is empty, it automatically switches the game to the Gameplay state and, when the stack have one or more screens, it switches to the Paused state.

#How to use:
 - Add the Screen Stack component to any object in your scene;
 - In the UIs that contains buttons to be focused, add the ActivatableUI component and change its settings as you wish;
 - In the buttons that can be focused, add the Focusable Button component.

#Showcases video:
https://www.youtube.com/watch?v=KbXPXwt0nNQ&ab_channel=RenanNunes

![ScreenStackPrint](https://github.com/Renan1423/ScreenStackSystem/assets/37775230/24a065dd-37fb-4c4a-88c4-a72e33649bbb)
![ScreenStackPrint1](https://github.com/Renan1423/ScreenStackSystem/assets/37775230/c5997627-4dad-403d-984a-d5f5e7f6a881)
![ScreenStackPrint2](https://github.com/Renan1423/ScreenStackSystem/assets/37775230/4e05451f-44b7-40c9-b5eb-33d90b5d5e97)
![ScreenStackPrint3](https://github.com/Renan1423/ScreenStackSystem/assets/37775230/aec37109-d9a6-42e1-8229-67231e9d4510)



