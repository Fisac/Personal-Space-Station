
How to add audio using the audiomanager:
-	In the audiomanager prefab and under Sounds. Increase the size to add more sound components.

-	Rename the new element to something appropiate like 'shoot'.

-	Add the audio file to 'Clip'.

-	In music group drag in either 'Music' or 'SoundFX' depending on what type of sound it is.

-	The 'Master' group should be in the top 'Mixer Group'.

Functions:
-	'Volume' and 'Pitch' changes the volume and pitch values.

-	'Volume Variance' and 'Pitch Variance' will add a randomized value increasing the 
	slider value will increase the amount of randomness you want to add to the sound clip.

Adding more values:
-	To add more sound effects you simply increase the size value and add the new sound in the new element.

Using sounds in scripts:
-	To play audio use the following code:

-	FindObjectOfType<AudioManager>().Play("name-of-sound-element");

3D Sounds:
-	When using 3D sounds you wanna put 'Spatial Blend' to 1 and in 'Target' you 
	wanna add a gameobject from the scene that is used as source position for the sound.
	
-	Min Distance and Max distance changes the size of the audio source. 
	It's easier to go into playmode then tweak the size values there and then copy them into the editor.

IMPORTANT:
-	Do NOT re-name the audiomanager!