v1.cs contains the former version before refactoring.

v2.cs contains a helper version that doesn't work.

v3.cs contains the final refactored version that is working basically the same way as the first version (the only difference is the cursor position).

Key Changes and Improvements:

* Code Organization: Methods are organized logically, separating initialization, game loop actions (input processing, game state updates, collision detection), and rendering.
* Readability: Method names clearly indicate their purpose, improving readability and maintainability.
* Use of Properties: The Pixel class uses properties for position and color, adhering to C# conventions.
* Simplification: Removed unnecessary variables and condensed the input processing logic to eliminate redundant checks.
* Game Logic Separation: Separated game logic from rendering logic, making it easier to understand and modify each part of the code independently.

This refactored version aims to make the code cleaner, more maintainable, and easier to understand while preserving the original functionality.