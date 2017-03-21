# connect4
Basic connect-4 game in C#


C# Code for Unity Based Connect4 Game

------------------------------------


Features:

Draws a board and allows pieces to be placed by clicking on columns.
Ability to start new game when game is over.
Play human v human or human v computer. (Computer v Computer untested)
Basic hueristic-driven ai using depth-limited Negamax algorithm with Alpha-Beta pruning. 



Known Issues:

Current player's piece should be drawn before the computer player's move is calculated to avoid the delay seen when placing a piece.
End of game message not displayed for Human wins.
The search algorithm is still fairly slow at high depths, with up to several seconds search time at depths 6 and higher.
If multiple moves exist for computer with the same hueristic value, the first (leftmost) move will always be taken.
Currently, lines that have a gap in them (xx - x) are not counted as a threat correctly.
If the player has forked the computer (wherever it moves it will lose) the computer returns all scores as equally bad instead of blocking one of the player's wins. Although this makes sense, the human may not have seen the fork and may still make a mistake.



------------------------------------




