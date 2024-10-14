# Game
I did not mock the singleton. If I should mock the singleton as well, please let me know.
And I implemented the command design pattern, the game class is the sender, and the board class is the receiver, so new commands could be introduced without having to modify
the client code, the game class, but having to modify the board class to add new business logic.
