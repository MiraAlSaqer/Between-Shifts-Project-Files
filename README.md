# Between Shifts

Between Shifts is a 3D narrative-driven simulation prototype developed in Unity where the player steps into the role of a single father navigating the exhausting daily routines of parenting, commuting, and occupational burnout. Through focused first-person gameplay, the experience uses atmospheric lighting, dynamic audio styling, and fading UI parameters to simulate the heavy emotional and physical labor of balancing career demands with caregiving responsibilities.

### Core Systems & Scripts

Between Shifts relies on a decoupled, event-driven architecture where player constraints, state changes (fatigue), and scene transitions are managed through centralized controllers rather than hardcoded dependencies.

#### 1. State & Scene Management Controllers

*   **SupermarketManager & StatusEffectsManager**
  
    Coordinates the global 60-second exhaustion lifecycle. It synchronizes physical player constraints (freezing physics translation, locking kinematic properties) with runtime UI frameworks and links directly to post processing color grading profiles to simulate real-time cognitive decline.

*   **SchoolArrival & SupermarketArrival**

    Manages multi-stage narrative state sequences and asset dependency tracking. Handles complex asynchronous scene entry and car to foot exit transitions by managing audio listeners, spatial positioning, UI crossfades, and cinematic camera handoffs.

*   **GameOverManager**

    A controller that manages fatal player burnout states. It drives alpha blended canvas overlays, maps variable typewriter audio pacing based on text punctuation milestones, and cleans up active system timescales during menu returns.

#### 2. Core Player System Loops

*   **PlayerInteract & PlayerInteractUI**

    A modular raycast interaction system that projects centralized viewport rays out to explicit distance tolerances. It dynamically extracts abstract IInteractable interfaces from target layers while running a frame by frame checking system to evaluate crosshair context and toggle UI prompt visibility.

*   **PlayerMovement**

    A physics-driven character translation system. It samples raw input keys to update rigidbody linear damping profiles based on continuous raycast ground-checks.

#### 3. Interactive World Assets

*   **DoorController & DoorExit**

    Implements the IInteractable interface to handle spatial state configurations. It seamlessly bridges gameplay and scene transitions by handling local translation shifts, playing contextual audio profiles, locking player scripts behind prompt windows, and initiating asset scene loading loops.

*   **AlarmClock & LeahInteract**

    Dialogue monitoring controllers designed to freeze character movement parameters dynamically upon interaction. They handle frame by frame checking routines that lock user locomotion until associated dialogue UI panels are fully deactivated.
