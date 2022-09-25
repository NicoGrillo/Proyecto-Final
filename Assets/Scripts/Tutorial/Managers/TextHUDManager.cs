using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHUDManager : MonoBehaviour
{
    private static string firstStepText;
    private static string secondStepText;
    private static string lastStepText;
    private static string tutorialMoveText;
    private static string tutorialFLText;
    private static string tutorialRocksText;
    private static string exitArea1;
    private static string thirstStepText;
    private static string exitArea2;


    public static string FirstStepText { get => firstStepText; }
    public static string SecondStepText { get => secondStepText; }
    public static string LastStepText { get => lastStepText; }
    public static string TutorialMoveText { get => tutorialMoveText; }
    public static string TutorialFLText { get => tutorialFLText; }
    public static string TutorialRocksText { get => tutorialRocksText; }
    public static string ExitArea1 { get => exitArea1; }
    public static string ThirstStepText { get => thirstStepText; }
    public static string ExitArea2 { get => exitArea2; }

    void Awake()
    {
        //There is no time for explanations, you have to run away and hide from those things before dawn.

        //AREA 1 Textos
        firstStepText = "------------Warning-----------Be careful out there, you don't know what to expect. Run Juan, run.";
        secondStepText = "-------Useless, or not?------It seems that the rocks are useless, at least against those creatures.";
        lastStepText = "------------Look Out----------An exit! You just have to dodge the last one.";
        tutorialMoveText = "-------------Tutorial-----------Use WASD to move and the mouse to look around. Hold Shift to run.";
        tutorialFLText = "-------------Tutorial-----------You found a Flashlight. Use the F button to activate it.";
        tutorialRocksText = "-------------Tutorial-----------Rocks, maybe you can use them. Will there be more in the other stones? Right click to throw rocks.";
        exitArea1 = "-------------Area 2------------Welcome to the second area. There are other creatures around here. Be careful.";

        //AREA 2 Textos
        firstStepText = "---------Reload light---------Batteries for the flashlight, maybe the last ones.";
        secondStepText = "-----Ranged creatures-----More you advance, more dangerous it gets.";
        thirstStepText = "------Look everywhere------Never let your guard down, you don't know what the fog hides.";
        exitArea2 = "-------------Area 3------------Welcome to the third area. Some light among so much darkness.";
    }
}