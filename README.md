# Pirate Cove: The Catcher

Unity mini-game challenge report. Author: b.atomic

## 1. The game I chose

I chose option 2, **The Catcher**. A basket moves left and right at the bottom of the screen, good items give points, and bombs cause damage. I turned it into a small story: a pirate holds up a treasure chest and catches falling coins and gems while cannonballs fall from a rival ship. Health is 3 hearts, and the player has 1:30 minutes to play. Catching a good item adds 2 seconds, and missing one costs 1 second.

Beyond the base requirements I added: difficulty that ramps up over time, a countdown timer, saved high scores, a start menu, a game over screen with a restart on Enter, a full pirate theme with art, and sound effects.

## 2. Thought process

My main rule was that **no system should know about any other system**. Each mechanic does one job and announces what happened through a C# event. Other systems subscribe to the events they care about. This is the Observer Pattern, and it gave me three benefits:

- I could build and test one piece at a time and see the result in the Console before any UI existed.
- Adding a feature (timer, high scores, sound) meant adding a new listener, not editing old code.
- The UI is only a listener, so it can be changed or removed without breaking the game logic.

I built the game in this order: basket movement, falling items and spawner, score and health, UI, game over, art and theme, difficulty, timer, high scores, menu, sound.

## 3. How I divided the systems

| Folder | Script | Responsibility |
| --- | --- | --- |
| Player | PlayerController | Moves the pirate with A/D or arrows, clamped to the screen edges |
| Items | ItemData (ScriptableObject) | Data for each item type: sprite, points, damage, bomb or not |
| Items | FallingItem | Falls, detects a catch in the chest's catch zone or a miss at the bottom |
| Items | Spawner | Spawns random items at random positions; speed and rate follow the difficulty |
| Systems | ScoreSystem | Tracks score |
| Systems | HealthSystem | Tracks health, announces death |
| Systems | TimerSystem | Countdown with bonus and penalty |
| Systems | DifficultyManager | Raises the difficulty level every few seconds |
| Systems | HighScoreSystem | Saves the top 5 scores with PlayerPrefs |
| Core | GameStateManager | Ends the game (death or time up), freezes time, restarts on Enter |
| Core | GameSettings | Static settings shared between scenes |
| UI | UIManager | Shows score, health, timer, and the game over screen |
| UI | MainMenu | Start and Quit buttons, shows the top score |
| Audio | Sound effects | Plays sounds for a coin catch, a bomb hit, and losing |

## 4. Events and who listens

| Event | Published by | Listened to by |
| --- | --- | --- |
| OnItemCaught(ItemData) | FallingItem | ScoreSystem, HealthSystem, TimerSystem, audio |
| OnItemMissed(ItemData) | FallingItem | TimerSystem |
| OnScoreChanged(int) | ScoreSystem | UIManager, HighScoreSystem |
| OnHealthChanged(int) | HealthSystem | UIManager |
| OnPlayerDied | HealthSystem | GameStateManager |
| OnTimeChanged(float) | TimerSystem | UIManager |
| OnTimeUp | TimerSystem | GameStateManager |
| OnDifficultyChanged(int) | DifficultyManager | Spawner |
| OnGameOver | GameStateManager | UIManager, HighScoreSystem, audio |
| OnHighScoresUpdated(int\[\]) | HighScoreSystem | UIManager |

Every subscription (`+=` in `OnEnable`) has a matching unsubscription (`-=` in `OnDisable`) to avoid leaks and null references.

## 5. Design decisions

- **ScriptableObjects for items.** Coin, gem and cannonball are data assets. Adding a new item needs no code change.
- **Catch zone.** The pirate sprite is tall, so catches are checked against a small invisible zone on the chest opening, not the whole sprite.
- **Difficulty by events.** A timer raises the level and the Spawner reacts by shortening the spawn interval and increasing the fall speed, up to a cap.
- **Persistent scores.** Scores are kept with PlayerPrefs, so they survive closing the game, and the menu reads them through a static method.
