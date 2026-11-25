\# Enchanted Forest – Playtest Notes



\*\*Tester:\*\* Sandy Boueri (Developer)



During development, the game was tested regularly to evaluate the controls, gameplay mechanics, animations, user interface and overall stability.



Several issues were identified and corrected during testing:



\- \*\*Respawn system:\*\* The Guardian did not respawn correctly. This was fixed by assigning the correct starting and checkpoint positions.

\- \*\*Character animations:\*\* The death animation continued after respawning. This was corrected by resetting the Animator to the Idle state.

\- \*\*Enemy movement:\*\* Mushroom movement was improved by adjusting colliders and ground detection.

\- \*\*User interface:\*\* The Try Again button was unresponsive because another UI element was blocking it. The interfering element was disabled.

\- \*\*Visual effects:\*\* The Guardian turned red when jumping instead of taking damage. The hit flash was corrected and the original character color was restored properly.



After implementing these changes, the game was tested again in both the Unity Editor and the standalone Windows build. The main gameplay mechanics, menus, audio and respawn system worked successfully during the final testing.

