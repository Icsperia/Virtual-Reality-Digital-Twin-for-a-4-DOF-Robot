This project is an immersive Virtual Reality (VR) digital twin tailored for industrial environments, focusing on the operation, safety, and real-time monitoring of industrial robots. It bridges the gap between spatial computing and industrial automation by combining a real-time 3D simulation environment with a lightweight MQTT messaging broker.

This system allows operators to train in a risk-free, highly realistic virtual space that remains perfectly synchronized with physical machinery, enabling both simulation-based training and real-time teleoperation.
Key Features

    Real-Time Feedback Loop: Features a continuous, bi-directional feedback system between the physical robotic arm and the Unity simulation. Sensor telemetry updates the virtual twin's state instantly, while VR inputs can be relayed back to control the physical hardware.

    Lightweight IoT Architecture: Utilizes an MQTT broker for efficient, event-driven, low-latency communication, optimized for standard industrial automation hardware.

    High-Fidelity Mechanical Simulation: The 3D model of the robotic arm was designed from scratch using Autodesk Fusion, ensuring accurate mechanical proportions, precise joint articulation, and realistic physics within the Unity engine.

    Immersive Training Scenarios: Provides a safe digital workspace where users can learn machine operation, test kinematics, and practice safety protocols without real-world hazards.

Technologies & Tools

    Simulation & VR: Unity 3D, C#

    CAD & 3D Modeling: Autodesk Fusion

    Communication & IoT: MQTT Protocol, Mosquitto Broker

    Hardware Interfacing: Python (Paho-MQTT, Serial communication)



https://github.com/user-attachments/assets/b277b08d-7292-46dd-83df-eedab1a9c66e



