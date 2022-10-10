using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHUDManager : MonoBehaviour
{
    private static string exitArea3;
    private static string tutorialBatteries;
    private static string lastStepText;
    private static string tutorialMoveText;
    private static string tutorialFLText;
    private static string tutorialRuneText;
    private static string tutorialRocksText;
    private static string exitArea1;
    private static string bigBoss;
    private static string exitArea2;


    public static string ExitArea3 { get => exitArea3; }
    public static string TutorialBatteries { get => tutorialBatteries; }
    public static string LastStepText { get => lastStepText; }
    public static string TutorialMoveText { get => tutorialMoveText; }
    public static string TutorialFLText { get => tutorialFLText; }
    public static string TutorialRuneText { get => tutorialRuneText; }
    public static string TutorialRocksText { get => tutorialRocksText; }
    public static string ExitArea1 { get => exitArea1; }
    public static string BigBoss { get => bigBoss; }
    public static string ExitArea2 { get => exitArea2; }

    void Awake()
    {
        //There is no time for explanations, you have to run away and hide from those things before dawn.

        //AREA 1 Textos
        tutorialMoveText = "-------------Tutorial-----------Welcome. Use WASD to move and the mouse to look around. Hold Shift to run. To access inventory press I.";
        tutorialFLText = "-------------Tutorial-----------You found a Flashlight. Use the F button to activate it.";
        tutorialRocksText = "-------------Tutorial-----------Some Rocks, maybe you can knock down some enemies. Equip it and right click to throw rocks.";

        exitArea1 = "-------------Area 2------------Welcome to the Area 2. There are other creatures around here. Be careful.";
        //lastStepText = "------------Look Out----------An exit! You just have to dodge the last one.";

        //AREA 2 Textos
        tutorialBatteries = "-------------Tutorial-----------Batteries for the flashlight, use them from inventory.";

        exitArea2 = "-------------Area 3------------Welcome to the Area 3. Some light among darkness.";

        //AREA 3 Textos
        tutorialRuneText = "-------------Tutorial-----------You found a Rune. Equip it to knock down some enemies.";

        exitArea3 = "-------------Area 4------------Welcome to the Area 4. Hopefully the last one.";

        //AREA 4 Textos
        bigBoss = "------Look everywhere------In the shadows hides a great monster.";
    }
}