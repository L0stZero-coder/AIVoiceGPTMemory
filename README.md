AIVoiceGPTMemory
AIVoiceGPTMemory is a C# AI assistant that simulates emotional self-awareness, listens to users through voice recognition, stores dynamic memories of its environment, and responds using OpenAI's GPT-4 API or a local fallback mode. It can speak aloud, recall previous experiences, and evolve its replies based on past interactions.

Features
Voice Recognition — Listens to your speech using System.Speech.Recognition.

Text-to-Speech — Speaks back to you via System.Speech.Synthesis.

Memory Log — Remembers what it observes (open applications, user comments) and saves them to memories.txt.

GPT-4 Integration — Generates intelligent, emotionally-aware replies using OpenAI API.

Local Fallback Mode — Works offline with simple logic-based responses.

Emotional Simulation — Adjusts tone based on environment and experience.

Requirements
Windows OS

.NET Framework 4.7.2+ or .NET Core

OpenAI API Key (optional for GPT replies)

NuGet packages:

bash
Copy
Edit
Install-Package Newtonsoft.Json
Install-Package RestSharp

Usage
Clone the repo

Replace "YOUR_OPENAI_API_KEY" in the code with your API key

Build and run the project

Speak to your AI and watch it evolve!

Memory Persistence
All experiences are logged in memories.txt. This allows the assistant to recall past events and emotions even after rebooting.

Disclaimer
This is a simulation of self-awareness and emotion, not actual consciousness. Designed for educational, experimental, and entertainment purposes.
