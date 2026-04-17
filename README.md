# Boid Hunting Simulation

Artificial Intelligence simulation developed in Unity, featuring autonomous agents (Boids) and a Hunter NPC with reactive behavior.

This project was built for the **Artificial Intelligence I** course and focuses on implementing core AI techniques such as **Flocking, Decision Trees, Finite State Machines (FSM), and Steering Behaviors**.

---

## Overview

The simulation represents a dynamic environment where:

* Boids behave as autonomous agents
* Each Boid makes decisions based on its surroundings
* A Hunter NPC interacts with them using state-based logic
* Food acts as a resource influencing agent behavior

The system demonstrates how multiple AI techniques can be combined to produce emergent behavior.

---

## AI Systems Implemented

### Boids (Autonomous Agents)

Each Boid is controlled by a **Decision Tree** that evaluates the environment in real time:

* **Food nearby →** Move towards it using *Arrive*
* **Hunter nearby →** Escape using *Evade*
* **Other boids nearby →** Apply *Flocking*
* **No stimuli →** Move randomly

---

### Flocking Behavior

Boids move in groups using three core rules:

* **Separation** → Avoid crowding neighbors
* **Alignment** → Match velocity with nearby boids
* **Cohesion** → Move toward the group's center

This produces realistic group movement and emergent patterns.

---

### Food System

* Food is spawned randomly in the environment
* Boids detect and move towards food using *Arrive*
* Food is consumed and removed from the scene

---

### Hunter NPC (Finite State Machine)

The Hunter behavior is implemented using a **Finite State Machine (FSM)**:

#### Idle / Rest

* Triggered when energy reaches zero
* Hunter stops moving and recovers energy
* After recovery → transitions to Patrol

#### Patrol

* Moves through predefined waypoints
* Can loop or reverse its path
* Detects boids within vision range → transitions to Hunting

#### Hunting

* Pursues boids using:

  * **Pursuit (predictive movement)**
* Consumes energy while active
* If energy depletes → returns to Idle
* If target is lost → returns to Patrol

---

## Steering Behaviors

The simulation uses classic steering behaviors:

* **Arrive** → Smooth movement toward a target
* **Evade** → Move away from threats
* **Pursuit** → Predict and intercept moving targets

---

## Architecture & Design

The project is structured with separation of responsibilities:

* **Boid System**

  * Movement + flocking logic
  * Decision Tree evaluation

* **Hunter System**

  * FSM implementation
  * State-based transitions

* **Environment System**

  * Food spawning
  * Detection and interaction logic
---

## ▶️ How to Run

1. Clone the repository:

   ```bash
   git clone https://github.com/Mon-Zai/BoidHunting.git
   ```

2. Open the project in Unity

3. Load the main scene

4. Press ▶ Play

---

##  Key Learnings

* Combining multiple AI systems in a single simulation
* Designing modular behavior systems (FSM + Decision Tree)
* Implementing real-time decision-making agents
* Understanding emergent behavior in group systems